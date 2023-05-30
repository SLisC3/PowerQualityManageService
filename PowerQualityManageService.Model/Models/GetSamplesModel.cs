namespace PowerQualityManageService.Model.Models;


public class GetSamplesModel
{
    public IEnumerable<DateTime> DataLabels { get; set; }
    public Dictionary<string, IEnumerable<object?>> Samples { get; set; }
}

