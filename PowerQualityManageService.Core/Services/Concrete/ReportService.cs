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
    private readonly ILocalFilesRepository _filesRepository;
    private readonly IReportRepository _reportRepository;

    public ReportService(ITemplateService templateService, IDataService dataService, IReportRepository reportRepository, ILocalFilesRepository filesRepository)
    {
        _templateService = templateService;
        _dataService = dataService;
        _reportRepository = reportRepository;
        _filesRepository = filesRepository;
    }

    public async Task<bool> Delete(string fileName)
    {
        await _reportRepository.Delete(fileName);
        string filepath = fileName.ToFilePath();
        return _filesRepository.DeleteFile(filepath);
    }

    public async Task<byte[]> Download(string fileName)
    {
        return await _filesRepository.GetFile(fileName);
    }

    public async Task<string?> GenerateReport(string templateName, ResultDefinition resultDefinition, string reportName)
    {
#if DEBUG
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
#endif
        Template? template = await _templateService.GetTemplateByName(templateName);
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
        await _reportRepository.Add(new Report() 
            { 
                FileName = fileName.ToBaseFileName(),
                Name = reportName,
                DateFrom= resultDefinition.DateFrom,
                DateTo= resultDefinition.DateTo,
                MeasuringPoint= resultDefinition.MeasuringPoint,
                Template = templateName
            }
        );;
#if DEBUG
        stopwatch.Stop();
        Console.WriteLine("Czas Generowania PDF: " + stopwatch.Elapsed);
#endif
        return fileName;
    }

    public Task<string> GetFileNameFromReportName(string name)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Report>?> GetReports()
    {
        return await _reportRepository.GetAll();
    }

    public async Task<List<string>> GetTemplatesNames()
    {
        return await _templateService.GetTemplatesNames();
    }

    public async Task<List<string>> GetMeasuringPoints()
    {
        return await _dataService.GetMeasuringPoints();
    }

    public Task PreviewReport(string fileName)
    {
        _filesRepository.PreviewFile(fileName);
        return null;
    }

    public async Task<bool> SendMail(MailModel model)
    {
        model.Attachment = model.Attachment.ToFilePath();
        return await MailSender.SendMail(model);
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