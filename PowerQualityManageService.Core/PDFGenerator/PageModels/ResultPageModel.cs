using PowerQualityManageService.Model.Models;

namespace PowerQualityManageService.Core.PDFGenerator.PageModels;

public class ResultPageModel
{
    public IEnumerable<SingleNormResult> Results { get; set; } = null!;
    public IEnumerable<ChartData> ResultCharts { get; set; } = null!;
    public int NormValue { get; set; }
}

public enum normType
{
    AllValues = 1,
    PartialValues = 2
}
public enum actionType
{
    LargerThan = 1,
    SmallerThan = 2,
    Equal = 3
}
