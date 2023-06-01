namespace PowerQualityManageService.Model.Models;

public class ChartData
{
    public string Name { get; set; } = null!;
    public Dictionary<string, double[]> Data { get; set; } = null!;
    public IEnumerable<DateTime> DateLabels { get; set; } = null!;
}