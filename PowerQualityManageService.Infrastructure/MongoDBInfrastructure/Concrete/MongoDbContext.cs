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
    private readonly MongoClient
    public MongoDbContext(IOptions<MongoDbConfig> mongoDbConfig)
    {

    }
    public bool IsMongoActive()
    {
        throw new NotImplementedException();
    }
}
