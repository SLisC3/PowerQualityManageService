
using PowerQualityManageService.Model.Models;

namespace PowerQualityManageService.Core.Services.Abstract;

public interface IDataService
{
    Task<GetSamplesModel> GetSamples(DateTime startDate, DateTime endDate, string measuringPoint, List<string> keys);
}