using PowerQualityManageService.Model.Models;
using System.Data;

namespace PowerQualityManageService.Core.Repositories.Abstract;
public interface IDataManagementDbRepository
{
    Task<IEnumerable<DataSample>?> GetDataSamples(DateTime startDate, DateTime endDate, string measuringPoint);
    Task<DataTable?> GetDataSamplesDT(DateTime startDate, DateTime endDate, string measuringPoint);
    Task<int> InsertDataFromDataTable(DataTable dt, string measuringPoint);
    Task<List<string>> GetMeasuringPoints();
}

