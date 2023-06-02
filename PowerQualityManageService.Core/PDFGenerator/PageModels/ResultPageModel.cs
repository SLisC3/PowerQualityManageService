using PowerQualityManageService.Model.Models;

namespace PowerQualityManageService.Core.PDFGenerator.PageModels;

public class ResultPageModel
{
    public IEnumerable<SingleNormResult> Results { get; set; } = null!;
    public IEnumerable<ChartData> ResultCharts { get; set; } = null!;
    public int NormValue { get; set; }
}
