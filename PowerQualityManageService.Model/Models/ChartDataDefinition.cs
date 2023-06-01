namespace PowerQualityManageService.Model.Models;

public class ChartDataDefinition
{
    public string Name { get; set; } = null!;
    public IEnumerable<string> SamplesName { get; set; } = null!;
    public ChartDataDefinition()
    {

    }
    public ChartDataDefinition(string name, IEnumerable<string> samplesName)
    {
        Name = name;
        SamplesName = samplesName;
    }
    public ChartDataDefinition(string name, string sampleName)
    {
        Name = name;
        SamplesName = new List<string>() { sampleName };
    }
}
