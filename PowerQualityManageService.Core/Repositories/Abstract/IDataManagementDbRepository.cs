using MongoDB.Bson;
using PowerQualityManageService.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerQualityManageService.Core.Repositories.Abstract;
public interface IDataManagementDbRepository
{
    Task<IEnumerable<DataSample>?> GetDataSamples(DateTime startDate, DateTime endDate, string measuringPoint);
    Task<DataTable?> GetDataSamplesDT(DateTime startDate, DateTime endDate);
    Task<int> InsertDataFromDataTable(DataTable dt, string measuringPoint);
}

