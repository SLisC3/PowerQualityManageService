using MongoDB.Driver;
using PowerQualityManageService.Model.Models;

namespace PowerQualityManageService.Infrastructure.MongoDBInfrastructure.Abstract;

public interface IMongoDbContext
{
    public bool IsMongoActive();
    IMongoCollection<DataSample> DataSamples {get;}
    IMongoCollection<DataSampleId> DataSamplesId { get; }
    IMongoCollection<DataSampleId> DataSamplesIdHybrid { get; }
    IMongoCollection<Template> Templates{ get; }
    IMongoCollection<Report> Reports { get; }
}

