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
using System.Runtime.CompilerServices;

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
        List<string> keys = NormResultReportHelper.GetKeys(template.Charts!.SelectMany(x => x.SamplesName));
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

    public void PreviewReport(string fileName)
    {
        _filesRepository.PreviewFile(fileName);
    }

    public async Task<bool> SendMail(MailModel model)
    {
        model.Attachment = model.Attachment!.ToFilePath();
        return await MailSender.SendMail(model);
    }

    private GetSamplesModel FilterSamples(GetSamplesModel samplesWithDataLabels, IEnumerable<string> samplesName)
    {
        var filteredSamples = new Dictionary<string, IEnumerable<object?>>();
        IEnumerable<int> idx= new List<int>();

        foreach (var sampleName in samplesName)
            idx = idx.Concat(samplesWithDataLabels.Samples[sampleName].IndexesOf(x => x == null));

        idx = idx.Distinct();
        foreach(var sampleName in samplesName)
        {
            filteredSamples.Add(sampleName, samplesWithDataLabels.Samples[sampleName].ExceptByIndexes(idx));
        }

        return new GetSamplesModel() { DataLabels = samplesWithDataLabels.DataLabels.ExceptByIndexes(idx), Samples = filteredSamples! };
    }
    private List<ChartData> PrepareCharts(IEnumerable<ChartDataDefinition> charts, GetSamplesModel samplesWithDatalabels)
    {
        List<ChartData> results = new List<ChartData>();
        foreach (ChartDataDefinition chart in charts)
        {
            var filteredSamples = FilterSamples(samplesWithDatalabels, chart.SamplesName);
            var signals = chart.SamplesName.ToDictionary(l => l, l => filteredSamples.Samples[l].ToDoubleArray());
            results.Add(new ChartData() { Name = chart.Name, DateLabels = filteredSamples.DataLabels, Data = signals });
        }
        return results;
    }

    private List<SingleNormResult> PrepareNorms(IEnumerable<SingleNormDefinition> norms, GetSamplesModel samplesWithDatalabels)
    {
        List<SingleNormResult> results = new List<SingleNormResult>();
        foreach (SingleNormDefinition norm in norms)
        {
            norm.Samples =norm.SamplesName.ToDictionary(l => l, l => samplesWithDatalabels.Samples[l].Cast<decimal?>());
            var result = NormResultCalculator.Calculate(norm).ToList();
            
            if (norm.Name.Equals("4b"))
            {
                var it = 1;
                bool s1 = true; var s2 = true; var s3 = true;
                for (int i = 0; i < result.Count(); i++)
                {
                    if(i % 3 == 0 && s1)
                    {
                        s1 = s1 && result[i].Result;
                        if(!s1) results.Add(new SingleNormResult() { Name = norm.Name + $" Faza 1", Success = false, Message = $"Norma niespełniona dla harmonicznej nr {(int) it/3 + 1}. {result.ElementAt(i).ErrorMessage}" });
                    }
                    if (i % 3 == 1 && s2)
                    {
                        s2 = s2 && result[i].Result;
                        if (!s2) results.Add(new SingleNormResult() { Name = norm.Name + $" Faza 2", Success = false, Message = $"Norma niespełniona dla harmonicznej nr {(int)it / 3 + 1}. {result.ElementAt(i).ErrorMessage}" });
                    }
                    if (i % 3 == 2 && s3)
                    {
                        s3 = s3 && result[i].Result;
                        if (!s3) results.Add(new SingleNormResult() { Name = norm.Name + $" Faza 3", Success = false, Message = $"Norma niespełniona dla harmonicznej nr{(int)it / 3 + 1}. {result.ElementAt(i).ErrorMessage}" });
                    }
                    it++;
                }
                if (s1) results.Add(new SingleNormResult() { Name = norm.Name + $" Faza 1", Success = true });
                if (s2) results.Add(new SingleNormResult() { Name = norm.Name + $" Faza 2", Success = true });
                if (s3) results.Add(new SingleNormResult() { Name = norm.Name + $" Faza 3", Success = true });
            }
            else
            {
                var it = 1;
                foreach (var res in result)
                {
                    if(result.Count()==1)
                    {
                        results.Add(new SingleNormResult() { Name = norm.Name , Success = res.Result, Message = res.ErrorMessage });
                        continue;
                    }
                    results.Add(new SingleNormResult() { Name = norm.Name + $" Faza {it}", Success = res.Result, Message = res.ErrorMessage});
                    it++;
                }
            }
        }
        return results;
    }

    public async Task<(DateTime, DateTime)> GetStartEndDate()
    {
        return await _dataService.GetStartEndDate();
    }
}