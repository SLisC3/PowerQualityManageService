
using PowerQualityManageService.Model.Models;
using System.Data;

namespace PowerQualityManageService.Core.Services.Abstract;

public interface IDataService
{
    Task<GetSamplesModel> GetSamples(DateTime startDate, DateTime endDate, string measuringPoint, List<string> keys);
    Task<DataTable?> GetSamplesDt(DateTime startDate, DateTime endDate, string measuringPoint, List<string>? keys);
    Task<List<string>> GetMeasuringPoints();
    Task<(DateTime, DateTime)> GetStartEndDate();
}