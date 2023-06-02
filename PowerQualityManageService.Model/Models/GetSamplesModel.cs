namespace PowerQualityManageService.Model.Models;


public class GetSamplesModel
{
    public IEnumerable<DateTime> DataLabels { get; set; } = null!;
    public Dictionary<string, IEnumerable<object?>> Samples { get; set; } = null!;
}

