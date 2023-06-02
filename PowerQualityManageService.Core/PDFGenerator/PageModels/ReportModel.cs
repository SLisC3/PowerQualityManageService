using PowerQualityManageService.Model.Models;

namespace PowerQualityManageService.Core.PDFGenerator.PageModels;

public class ReportModel
{
    public string StationName { get; set; } = null!;
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
    public IEnumerable<SingleNormResult> Results { get; set; } = null!;
    public List<ChartData>? ResultCharts { get; set; }
    public bool ContainsAdditionalCharts { get => AdditionalCharts != null && AdditionalCharts.Count != 0; }
    public List<ChartData>? AdditionalCharts { get; set; }
}



