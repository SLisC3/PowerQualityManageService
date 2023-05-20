namespace PowerQualityManageService.Core.Services.Abstract;

public interface IDataService
{
    Task<Dictionary<string, IEnumerable<object>>?> GetResults(DateTime startDate, DateTime endDate, List<string> keys);
}