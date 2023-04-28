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
public class DataMongoDbRepository : IDataMongoDbRepository
{
    private readonly IMongoCollection<BsonDocument> _dataSamples;
    public DataMongoDbRepository(IMongoDbContext mongoDbContext)
    {
        _dataSamples = mongoDbContext.DataSamples;
    }

    public async Task<bool> InsertData(DataTable dt)
    {
        if (dt == null)
        {
            return false;
        }
        var batch = new List<BsonDocument>();
        foreach(DataRow dr in dt.Rows)
        {
            var dictionary = dr.Table.Columns.Cast<DataColumn>().ToDictionary(col => col.ColumnName, col => dr[col.ColumnName]);
            batch.Add(new BsonDocument(dictionary));
        }
        await _dataSamples.InsertManyAsync(batch.AsEnumerable());

        return true;
    }
}
