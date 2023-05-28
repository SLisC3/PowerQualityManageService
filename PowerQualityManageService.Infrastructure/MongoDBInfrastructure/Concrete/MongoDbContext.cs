using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using PowerQualityManageService.Infrastructure.Models;
using PowerQualityManageService.Infrastructure.MongoDBInfrastructure.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PowerQualityManageService.Infrastructure.MongoDBInfrastructure.Concrete;

public class MongoDbContext : IMongoDbContext
{
    private readonly MongoClient _mongoClient;
    private readonly IMongoDatabase _mongoDatabase;
    private readonly IMongoCollection<DataSample> _dataSamples;
    private readonly IMongoCollection<Template> _templates;
    public MongoDbContext(IOptions<MongoDbConfig> mongoDbConfig)
    {
        _mongoClient = new MongoClient(mongoDbConfig.Value.ConnectionString);
        _mongoDatabase = _mongoClient.GetDatabase(mongoDbConfig.Value.Database);
        _dataSamples = _mongoDatabase.GetCollection<DataSample>(mongoDbConfig.Value.DataSamples);
        _templates = _mongoDatabase.GetCollection<Template>(mongoDbConfig.Value.Templates);
    }

    public IMongoCollection<DataSample> DataSamples => _dataSamples;
    public IMongoCollection<Template> Templates => _templates;
    public bool IsMongoActive()
    {
        throw new NotImplementedException();
    }
}
