using Microcharts;
using PowerQualityManageService.Core.Helpers;
using PowerQualityManageService.Core.PDFGenerator;
using PowerQualityManageService.Core.PDFGenerator.PageModels;
using PowerQualityManageService.Core.Repositories.Abstract;
using PowerQualityManageService.Core.Services.Abstract;
using PowerQualityManageService.Core.Utils.Extensions;
using PowerQualityManageService.Model.Models;
using QuestPDF.Fluent;
using System.Diagnostics;

namespace PowerQualityManageService.Core.Services.Concrete;

public class ReportService : IReportService
{
    private readonly ITemplateService _templateService;
    private readonly IDataService _dataService;
    public ReportService(ITemplateService templateService, IDataService dataService)
    {
        _templateService = templateService;
        _dataService = dataService;
    }

    public async Task<string?> GenerateReport(int templateId, ResultDefinition resultDefinition)
    {
#if DEBUG
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
#endif
        Template? template = await _templateService.GetTemplateById(templateId);
        if (template == null) return null;
#if DEBUG
        stopwatch.Stop();
        Console.WriteLine("Czas Wyciągania template'u: " + stopwatch.Elapsed);

        stopwatch.Restart();
        stopwatch.Start();
#endif
        List<string> keys = NormResultReportHelper.GetKeys(template.Charts.SelectMany(x => x.SamplesName));
        GetSamplesModel samplesWithDatalabels = await _dataService.GetSamples(resultDefinition.DateFrom, resultDefinition.DateTo, resultDefinition.MeasuringPoint, keys);
#if DEBUG
        stopwatch.Stop();
        Console.WriteLine("Czas Wyciągania Sampli: " + stopwatch.Elapsed);

        stopwatch.Restart();
        stopwatch.Start();
#endif
        ReportModel reportModel = new ReportModel()
        {
            FromDate = resultDefinition.DateFrom,
            ToDate = resultDefinition.DateTo,
            StationName = resultDefinition.MeasuringPoint,
            Results = PrepareNorms(NormResultReportHelper.Norms, samplesWithDatalabels),
            ResultCharts = PrepareCharts(NormResultReportHelper.Charts, samplesWithDatalabels),
            AdditionalCharts = PrepareCharts(template.Charts, samplesWithDatalabels)
        };

#if DEBUG
        stopwatch.Stop();
        Console.WriteLine("Czas tworzenia ReportModel: " + stopwatch.Elapsed);

        stopwatch.Restart();
        stopwatch.Start();
#endif
        string fileName = Guid.NewGuid().ToString() + ".pdf";
        string? filePath = Path.Combine(Directory.GetCurrentDirectory(), "Reports", fileName);
        ReportDocument document = new ReportDocument(reportModel);
        document.GeneratePdf(filePath);
#if DEBUG
        stopwatch.Stop();
        Console.WriteLine("Czas Generowania PDF: " + stopwatch.Elapsed);
#endif
        return fileName;
    }

    private List<ChartData> PrepareCharts(IEnumerable<ChartDataDefinition> charts, GetSamplesModel samplesWithDatalabels)
    {
        List<ChartData> results = new List<ChartData>();
        foreach (ChartDataDefinition chart in charts)
        {
            var signals = chart.SamplesName.ToDictionary(l => l, l => samplesWithDatalabels.Samples[l].ToDoubleArray());
            results.Add(new ChartData() { Name = chart.Name, DateLabels = samplesWithDatalabels.DataLabels, Data = signals });
        }
        return results;
    }

    private List<SingleNormResult> PrepareNorms(IEnumerable<SingleNormDefinition> norms, GetSamplesModel samplesWithDatalabels)
    {
        List<SingleNormResult> results = new List<SingleNormResult>();
        foreach (SingleNormDefinition norm in norms)
        {
            norm.Samples =norm.SamplesName.ToDictionary(l => l, l => samplesWithDatalabels.Samples[l].Cast<decimal>());
            var result = NormResultCalculator.Calculate(norm);
            results.Add(new SingleNormResult() { Name = norm.Name, Success = result.Item1, Message = result.Item2 });
        }
        return results;
    }
}