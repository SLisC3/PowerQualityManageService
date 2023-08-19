using MongoDB.Driver;
using PowerQualityManageService.Core.Repositories.Abstract;
using PowerQualityManageService.Model.Models;
using PowerQualityManageService.Infrastructure.MongoDBInfrastructure.Abstract;
using System.Data;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using MongoDB.Bson;

namespace PowerQualityManageService.Core.Repositories.Concrete;

public class DataManagementMongoDbWithIdsRepository : IDataManagementDbRepository
{
    private readonly IMongoCollection<DataSampleId> _dataSamples;
    public DataManagementMongoDbWithIdsRepository(IMongoDbContext mongoDbContext)
    {
        _dataSamples = mongoDbContext.DataSamplesId;
    }

    public async Task<int> InsertDataFromDataTable(DataTable dt, string measuringPoint)
    {
        if (dt == null)
        {
            return 0;
        }
        //-------------------------------------------------------------------------------
#if DEBUG
        var stopwatch = new Stopwatch();
        stopwatch.Start();
#endif
        //-------------------------------------------------------------------------------
        var samples = new List<DataSampleId>();
        foreach (DataRow dr in dt.Rows)
        {
            var dictionary = dr.Table.Columns.Cast<DataColumn>().ToDictionary(col => col.ColumnName, col => dr[col.ColumnName]);
            DateTime date = (DateTime)dictionary["Date"];
            dictionary.Remove("Date");
            bool flagging = !string.IsNullOrEmpty((string?)dictionary["Flagging"]);
            dictionary.Remove("Flagging");

            var sample = new DataSampleId
            {
                Id = ObjectId.GenerateNewId(),
                MeasuringPoint = measuringPoint,
                Date = date,
                Flagging = flagging,
                Data = dictionary
            };

            samples.Add(sample);
        }
        //-------------------------------------------------------------------------------
#if DEBUG
        stopwatch.Stop();
        Console.WriteLine("[MONGO_ID] Czas Tworzenia Sampli:  " + stopwatch.Elapsed);
        stopwatch.Restart();
        stopwatch.Start();
#endif
        //-------------------------------------------------------------------------------
        await _dataSamples.InsertManyAsync(samples.AsEnumerable());
        //-------------------------------------------------------------------------------
#if DEBUG
        stopwatch.Stop();
        Console.WriteLine("[MONGO_ID] Czas Zapisu w bazie:  " + stopwatch.Elapsed);
        stopwatch.Restart();
        stopwatch.Start();
#endif
        //-------------------------------------------------------------------------------
        return samples.Count;
    }

    public async Task<IEnumerable<DataSample>?> GetDataSamples(DateTime startDate, DateTime endDate, string measuringPoint)
    {

        var result = await _dataSamples
            .FindAsync(x => /*x.Flagging == false &&*/ x.Date >= startDate && x.Date <= endDate && x.MeasuringPoint == measuringPoint);

        if (result == null) { return null; }

        return await result.ToListAsync();
    }

    public async Task<DataTable?> GetDataSamplesDT(DateTime startDate, DateTime endDate, string measuringPoint)
    {

//-------------------------------------------------------------------------------
#if DEBUG
        var stopwatch = new Stopwatch();
        stopwatch.Start();
#endif
//-------------------------------------------------------------------------------

        var samples = await _dataSamples.FindAsync(x => x.Date >= startDate && x.Date <= endDate && x.MeasuringPoint == measuringPoint);
        var groupedSamples = samples.ToEnumerable().GroupBy(x => x.Date).Select(g => new { Date = g.Key, Data = g.SelectMany(x => x.Data!).Distinct().ToDictionary(kv => kv.Key, kv => kv.Value) }).ToList();

//-------------------------------------------------------------------------------
#if DEBUG
        stopwatch.Stop();
        Console.WriteLine("[MONGO_ID] Czas wyciągania Sampli:  " + stopwatch.Elapsed);
        stopwatch.Restart();
        stopwatch.Start();
#endif
//-------------------------------------------------------------------------------

        if (groupedSamples.Count == 0) { return null; }
        Dictionary<string, object> columns = groupedSamples.First().Data;

        DataTable dt = new DataTable();

        dt.Columns.Add("Date");
        foreach (KeyValuePair<string, object> kv in columns)
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

//-------------------------------------------------------------------------------
#if DEBUG
        stopwatch.Stop();
        Console.WriteLine("[MONGO_ID] Czas Tworzenia DataTable:  " + stopwatch.Elapsed);
        stopwatch.Restart();
        stopwatch.Start();
#endif
//-------------------------------------------------------------------------------

        return dt;
    }

    public async Task<List<string>> GetMeasuringPoints()
    {
        return _dataSamples.AsQueryable().Select(x => x.MeasuringPoint).Distinct().ToList();
    }

    public async Task<(DateTime, DateTime)> GetStartEndDate()
    {
        var min = _dataSamples.AsQueryable().Select(x => x.Date).Min();
        var max = _dataSamples.AsQueryable().Select(x => x.Date).Max();
        return (min, max);
    }
}
