namespace PowerQualityManageService.Model.Models;

public class SingleNormDefinition
{
    public string Name { get; set; } = null!;
    public IEnumerable<string> SamplesName { get; set; } = null!;
    public int NormValue { get; set; }
    public Dictionary<string, IEnumerable<decimal>> Samples { get; set; } = null!;
    public SingleNormDefinition(string name, string samplesName, int normValue)
    {
        Name = name;
        SamplesName = new List<string>() { samplesName };
        NormValue = normValue;
    }
    public SingleNormDefinition(string name, IEnumerable<string> samplesName, int normValue)
    {
        Name = name;
        SamplesName = samplesName; 
        NormValue = normValue;
    }
}
