namespace PowerQualityManageService.Model.Models;
public class Template 
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public IEnumerable<ChartDataDefinition>? Charts { get; set; } = null!;
}

public class Chart
{
    public string Name { get; set; } = null!;
    public Dictionary<string, string> Signals { get; set; } = null!;
}
