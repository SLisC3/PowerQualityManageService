using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerQualityManageService.Core.Repositories.Abstract;
public interface IDataManagementDbRepository
{
    Task<int> InsertDataFromDataTable(DataTable dt);
}

