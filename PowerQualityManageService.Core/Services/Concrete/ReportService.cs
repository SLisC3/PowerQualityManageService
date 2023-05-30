using PowerQualityManageService.Core.PDFGenerator;
using PowerQualityManageService.Core.PDFGenerator.PageModels;
using PowerQualityManageService.Core.Repositories.Abstract;
using PowerQualityManageService.Core.Services.Abstract;
using PowerQualityManageService.Infrastructure.Models;
using PowerQualityManageService.Model.Models;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using QuestPDF.Previewer;
using System.Diagnostics;

namespace PowerQualityManageService.Core.Services.Concrete;

public class ReportService : IReportService
{
    private readonly ITemplateService _templateService;
    private readonly IDataService _dataService;
    private readonly IReportRepository _reportRepository;
    public ReportService(ITemplateService templateService, IDataService dataService)
    {
        _templateService = templateService;
        _dataService = dataService;
    }

    public async Task<string?> GenerateReport(int templateId, ResultDefinition resultDefinition)
    {
        Template? template = await _templateService.GetTemplateById(templateId);
        if (template == null) return null;
        if (template.Charts == null || !template.Charts.Any()) return null;
        List<string> keys = template.Charts.SelectMany(x => x.Signals.Select(x => x.Value)).Distinct().ToList();
        GetSamplesModel results = await _dataService.GetSamples(resultDefinition.DateFrom, resultDefinition.DateTo, resultDefinition.MeasuringPoint, keys);
        List<ChartData> additionalCharts = new List<ChartData>();
        foreach(Chart chart in template.Charts)
        {
            var temp = chart.Signals.ToDictionary(kvp => kvp.Key, kvp => results.Samples[kvp.Value].Cast<decimal>().Select(x=>Decimal.ToDouble(x)) .ToArray());
            additionalCharts.Add(new ChartData() { Name = chart.Name, DateLabels = results.DataLabels, Data = temp });
        }
        ReportModel reportModel = new ReportModel()
        {
            FromDate = resultDefinition.DateFrom,
            ToDate = resultDefinition.DateTo,
            StationName = resultDefinition.MeasuringPoint,
            Results = new List<SingleResult>() { new SingleResult() { Name = "1a", Success = true }, new SingleResult() { Name = "1b", Success = false, Message = "Bład" }, new SingleResult() { Name = "2" } },
            ResultCharts = new List<ChartData>()
            {
                new ChartData()
                {
                    Name = "ResultCharts",
                    Data = new Dictionary<string, double[]>() {
                        { "V1", new double[] { 1, 7, -2 } },
                        { "V2", new double[] { 3, 4, 5 } },
                        { "V3", new double[] { 0, 5, 10 } },
                    },
                    DateLabels = new List<DateTime>() {DateTime.Now.AddDays(-1), DateTime.Now, DateTime.Now.AddDays(1)}
                }
            },
            AdditionalCharts = additionalCharts
        };

        QuestPDF.Settings.License = LicenseType.Community;
        string filePath = "test.pdf";
        ReportDocument document = new ReportDocument(reportModel);
        document.GeneratePdf(filePath);

        Process.Start("explorer.exe", filePath);
        return filePath;
    }
}