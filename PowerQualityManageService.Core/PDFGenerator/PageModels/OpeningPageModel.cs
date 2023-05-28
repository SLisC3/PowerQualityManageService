namespace PowerQualityManageService.Core.PDFGenerator.PageModels;

public class OpeningPageModel
{
    public string StationName { get; set; } = null!;
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
}