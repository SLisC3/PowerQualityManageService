using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerQualityManageService.Core.Repositories.Abstract;
public interface IDataMongoDbRepository
{
    Task<bool> InsertDataFromDataTable(DataTable dt);
    Task<bool> InsertSingleData(BsonDocument document);


}
