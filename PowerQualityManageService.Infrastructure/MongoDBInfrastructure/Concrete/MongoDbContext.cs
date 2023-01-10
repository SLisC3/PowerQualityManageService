using Microsoft.Extensions.Options;
using MongoDB.Driver;
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
    // private readonly IMongoCollection<Type> _collectionName;
    public MongoDbContext(IOptions<MongoDbConfig> mongoDbConfig)
    {
        _mongoClient = new MongoClient(mongoDbConfig.Value.ConnectionString);
        _mongoDatabase = _mongoClient.GetDatabase(mongoDbConfig.Value.Database);
        //_collectionName = _mongoDatabase.GetCollection<Type>(mongoDbConfig.Value.Collection)
    }

    //public IMongoCollection<Type> CollectionName => _collectionName;
    public bool IsMongoActive()
    {
        throw new NotImplementedException();
    }
}
