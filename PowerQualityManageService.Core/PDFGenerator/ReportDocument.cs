using AutoMapper;
using PowerQualityManageService.Core.PDFGenerator.PageModels;
using PowerQualityManageService.Core.PDFGenerator.PreparedPages;
using QuestPDF.Infrastructure;
using System.Diagnostics;

namespace PowerQualityManageService.Core.PDFGenerator;

public class ReportDocument : IDocument
{
    private readonly IMapper _mapper;
    public ReportModel _model;
    public ReportDocument(ReportModel model)
    {
        _model = model;
        var configExpression = new MapperConfigurationExpression();
        configExpression.AddProfile<PageModelsProfile>();
        var config = new MapperConfiguration(configExpression);
        _mapper = new Mapper(config);

    }
    public DocumentMetadata GetMetadata() => DocumentMetadata.Default;
    public DocumentSettings GetSettings() => DocumentSettings.Default;

    public void Compose(IDocumentContainer container)
    {
#if DEBUG
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
#endif
        container = container.InsertPage(new OpeningPage(_mapper.Map<OpeningPageModel>(_model)));
#if DEBUG
        stopwatch.Stop();
        Console.WriteLine("Czas tworzenia OpeningPage: " + stopwatch.Elapsed);
        stopwatch.Restart();
        stopwatch.Start();
#endif
        container = container.InsertPage(new NormPage());
#if DEBUG
        stopwatch.Stop();
        Console.WriteLine("Czas tworzenia NormPage: " + stopwatch.Elapsed);
        stopwatch.Restart();
        stopwatch.Start();
#endif
        container = container.InsertPage(new ResultPage(_mapper.Map<ResultPageModel>(_model)));
#if DEBUG
        stopwatch.Stop();
        Console.WriteLine("Czas tworzenia ResultPage: " + stopwatch.Elapsed);
        stopwatch.Restart();
        stopwatch.Start();
#endif

        if (_model.ContainsAdditionalCharts)
        {
            container = container.InsertPage(new CustomChartPage(_mapper.Map<CustomChartPageModel>(_model)));
        }
#if DEBUG
        stopwatch.Stop();
        Console.WriteLine("Czas tworzenia CustomChartPage: " + stopwatch.Elapsed);
#endif
    }
}
