﻿using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerQualityManageService.Infrastructure.MongoDBInfrastructure.Abstract;

public interface IMongoDbContext
{
    public bool IsMongoActive();
    IMongoCollection<BsonDocument> DataSamples {get;}
}

