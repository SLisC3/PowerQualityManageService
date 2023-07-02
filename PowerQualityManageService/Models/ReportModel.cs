namespace PowerQualityManageService.Models;

public class ReportDTOModel
{
    public string Name { get; set; }
    public string Template { get; set; }
    public string DateRange { get;set; }
    public string MeasuringPoint { get;set; }
}

public class AddReportModel
{
    public string Name { get; set; }
    public string Template { get; set; }
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
    public string MeasuringPoint { get; set; }
}
