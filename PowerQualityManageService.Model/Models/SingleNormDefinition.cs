namespace PowerQualityManageService.Model.Models;
public class SingleNormDefinition
{
    public string Name { get; set; } = null!;
    public IEnumerable<string> SamplesName { get; set; } = null!;
    public INormCalculationMethod CalculationMethod { get; set; }
    public Dictionary<string, IEnumerable<decimal?>> Samples { get; set; } = null!;
    public SingleNormDefinition(string name, string samplesName, INormCalculationMethod calculationMethod)
    {
        Name = name;
        SamplesName = new List<string>() { samplesName };
        CalculationMethod = calculationMethod;
    }
    public SingleNormDefinition(string name, IEnumerable<string> samplesName, INormCalculationMethod calculationMethod)
    {
        Name = name;
        SamplesName = samplesName; 
        CalculationMethod = calculationMethod;
    }
}
