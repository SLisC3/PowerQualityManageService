﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MongoDB.Bson;
using MongoDB.Driver;
using PowerQualityManageService.Core.Helpers;
using PowerQualityManageService.Core.Repositories.Abstract;
using PowerQualityManageService.Core.Services.Abstract;
using PowerQualityManageService.Infrastructure.Models;
using PowerQualityManageService.Infrastructure.MongoDBInfrastructure.Abstract;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace PowerQualityManageService.Core.Repositories.Concrete;
public class DataManagementMongoDbRepository : IDataManagementDbRepository
{
    private readonly IMongoCollection<DataSample> _dataSamples;
    public DataManagementMongoDbRepository(IMongoDbContext mongoDbContext)
    {
        _dataSamples = mongoDbContext.DataSamples;
    }

    public async Task<int> InsertDataFromDataTable(DataTable dt)
    {
        if (dt == null)
        {
            return 0;
        }
        var samples = new List<DataSample>();
        foreach(DataRow dr in dt.Rows)
        {
            var dictionary = dr.Table.Columns.Cast<DataColumn>().ToDictionary(col => col.ColumnName, col => dr[col.ColumnName]);
            DateTime date = (DateTime)dictionary["Date"];
            dictionary.Remove("Date");
            bool flagging = !string.IsNullOrEmpty((string?)dictionary["Flagging"]);
            dictionary.Remove("Flagging");

            var sample = new DataSample { 
                Date = date, 
                Flagging = flagging,
                Data = dictionary };

            samples.Add(sample);
        }
        await _dataSamples.InsertManyAsync(samples.AsEnumerable());
        
        return samples.Count;
    }

    public async Task<IEnumerable<DataSample>?> GetDataSamples(DateTime startDate, DateTime endDate)
    {
        var result = await _dataSamples
            .FindAsync(x => x.Flagging == false && x.Date >= startDate && x.Date <= endDate);
            
        if (result == null) { return null; }
        
        return await result.ToListAsync();
    }

    public async Task<DataTable?> GetDataSamplesDT(DateTime startDate, DateTime endDate)
    {
        var samples = await  _dataSamples.FindAsync(x => x.Flagging == false && x.Date >= startDate && x.Date <= endDate);
        var groupedSamples = samples.ToEnumerable().GroupBy(x => x.Date).Select(g => new { Date = g.Key, Data = g.SelectMany(x => x.Data!).Distinct().ToDictionary(kv => kv.Key, kv => kv.Value) }).ToList();

        if(groupedSamples.Count == 0) { return null; }
        Dictionary<string, object> columns = groupedSamples.First().Data;

        DataTable dt = new DataTable();

        dt.Columns.Add("Date");
        foreach(KeyValuePair<string,object> kv in columns)
        {
            dt.Columns.Add(kv.Key);
        }
        
        foreach (var item in groupedSamples)
        {
            item.Data.Add("Date", item.Date);
            DataRow row = dt.NewRow();
            foreach (var entry in item.Data)
            {
                row[entry.Key] = entry.Value;
            }
            dt.Rows.Add(row);
        }

        return dt;
    }
}
