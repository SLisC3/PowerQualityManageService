using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PowerQualityManageService.Infrastructure.MongoDBInfrastructure.Abstract;
using PowerQualityManageService.Model.Models;

namespace PowerQualityManageService.Infrastructure.MongoDBInfrastructure.Concrete;

public class MongoDbContext : IMongoDbContext
{
    private readonly MongoClient _mongoClient;
    private readonly IMongoDatabase _mongoDatabase;
    private readonly IMongoCollection<DataSample> _dataSamples;
    private readonly IMongoCollection<DataSampleId> _dataSamplesId;
    private readonly IMongoCollection<DataSampleId> _dataSamplesIdHybrid;
    private readonly IMongoCollection<Template> _templates;
    private readonly IMongoCollection<Report> _reports;
    public MongoDbContext(IOptions<MongoDbConfig> mongoDbConfig)
    {
        _mongoClient = new MongoClient(mongoDbConfig.Value.ConnectionString);
        _mongoDatabase = _mongoClient.GetDatabase(mongoDbConfig.Value.Database);
        _dataSamples = _mongoDatabase.GetCollection<DataSample>(mongoDbConfig.Value.DataSamples);
        _dataSamplesId = _mongoDatabase.GetCollection<DataSampleId>(mongoDbConfig.Value.DataSamplesId);
        _dataSamplesIdHybrid = _mongoDatabase.GetCollection<DataSampleId>(mongoDbConfig.Value.DataSamplesIdHybrid);
        _templates = _mongoDatabase.GetCollection<Template>(mongoDbConfig.Value.Templates);
        _reports = _mongoDatabase.GetCollection<Report>(mongoDbConfig.Value.Reports);
    }

    public IMongoCollection<DataSample> DataSamples => _dataSamples;
    public IMongoCollection<DataSampleId> DataSamplesId => _dataSamplesId;
    public IMongoCollection<DataSampleId> DataSamplesIdHybrid => _dataSamplesIdHybrid;
    public IMongoCollection<Template> Templates => _templates;
    public IMongoCollection<Report> Reports=> _reports;
    public bool IsMongoActive()
    {
        throw new NotImplementedException();
    }
}
