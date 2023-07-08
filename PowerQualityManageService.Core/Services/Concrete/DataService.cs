using PowerQualityManageService.Core.Repositories.Abstract;
using PowerQualityManageService.Core.Services.Abstract;
using PowerQualityManageService.Model.Models;
using System.Data;

namespace PowerQualityManageService.Core.Services.Concrete;

public partial class DataService : IDataService
{
    private readonly IDataManagementDbRepository _dataManagementRepository;

    public DataService(IDataManagementDbRepository dataManagementRepository)
    {
        _dataManagementRepository = dataManagementRepository;
    }

    public async Task<List<string>> GetMeasuringPoints()
    {
        return await _dataManagementRepository.GetMeasuringPoints();
    }

    public async Task<GetSamplesModel> GetSamples(DateTime startDate, DateTime endDate, string measuringPoint, List<string> keys)
    {
        IEnumerable<DataSample>? samples = await _dataManagementRepository.GetDataSamples(startDate, endDate, measuringPoint);
        if (samples == null) { return null; }
        var groupedSamples = samples.GroupBy(x => x.Date).Select(g => new { Date = g.Key, Data = g.SelectMany(x => x.Data!).Distinct().ToDictionary(kv => kv.Key, kv => kv.Value) });
        Dictionary<string, IEnumerable<object?>> chosenValues = new Dictionary<string, IEnumerable<object?>>();
        foreach (string key in keys)
        {
            chosenValues.Add(key, groupedSamples.Select(x => x.Data.TryGetValue(key, out object? value) ? value : null).ToList());
        }
        return new GetSamplesModel() { DataLabels = groupedSamples.Select(x=>x.Date), Samples = chosenValues};
    }

    public async Task<DataTable?> GetSamplesDt(DateTime startDate, DateTime endDate, string measuringPoint, List<string>? keys)
    {
        return await _dataManagementRepository.GetDataSamplesDT(startDate,endDate,measuringPoint);
    }
}
