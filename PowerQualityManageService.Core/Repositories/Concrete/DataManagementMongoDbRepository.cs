using MongoDB.Bson;
using MongoDB.Driver;
using PowerQualityManageService.Core.Helpers;
using PowerQualityManageService.Core.Repositories.Abstract;
using PowerQualityManageService.Core.Services.Abstract;
using PowerQualityManageService.Infrastructure.MongoDBInfrastructure.Abstract;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerQualityManageService.Core.Repositories.Concrete;
public class DataManagementMongoDbRepository : IDataManagementDbRepository
{
    private readonly IMongoCollection<BsonDocument> _dataSamples;
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
        var batch = new List<BsonDocument>();
        foreach(DataRow dr in dt.Rows)
        {
            var dictionary = dr.Table.Columns.Cast<DataColumn>().ToDictionary(col => col.ColumnName, col => dr[col.ColumnName]);
            batch.Add(new BsonDocument(dictionary));
        }
        await _dataSamples.InsertManyAsync(batch.AsEnumerable());
        
        return batch.Count;
    }
}
