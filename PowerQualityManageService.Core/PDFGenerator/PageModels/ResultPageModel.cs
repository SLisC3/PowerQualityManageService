using Microcharts;
using SkiaSharp;
using System.Linq;

namespace PowerQualityManageService.Core.PDFGenerator.PageModels;

public class ResultPageModel
{
    public IEnumerable<SingleResult> Results { get; set; }
    public List<ChartData>? ResultCharts { get;set; }
}
public class SingleResult
{
    public string Name { get; set; } = null!;
    public bool? Success { get; set; }
    public string? Message { get; set; }
}

public class ChartData
{
    public string Name { get; set; } = null!;
    public Dictionary<string, double[]> Data { get; set; } = null!;
    public IEnumerable<DateTime> DateLabels { get; set; } = null!;
}