using PowerQualityManageService.Core.Helpers;
using PowerQualityManageService.Core.PDFGenerator.Abstract;
using PowerQualityManageService.Core.PDFGenerator.PageModels;
using PowerQualityManageService.Model.Models;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace PowerQualityManageService.Core.PDFGenerator.PreparedPages;

public class CustomChartPage : IBasePage
{
    private readonly CustomChartPageModel _model;

    public CustomChartPage(CustomChartPageModel model)
    {
        _model = model;
    }

    public override IDocumentContainer Compose(IDocumentContainer container)
    {
        {
            return container.Page(page =>
            {
                page.Margin(50);
                page.Header().Element(ComposeHeader);
                page.Content().Element(ComposeContent);
                page.Footer().Element(ComposeFooter);
            });
        }
    }
    void ComposeHeader(IContainer container)
    {
        container.Row(row =>
        {
            row.RelativeItem().AlignCenter().Height(100).Column(column =>
            {
                column.Item().Text(text =>
                {
                    text.Span(ResourceHelper.Instance.GetString("Charts")).Style(titleStyle);

                });
            });
        });

    }
    void ComposeContent(IContainer container)
    {
        container.Row(row =>
        {
            row.RelativeItem().Column(column =>
            {
                column
                    .Item()
                    .Element(ComposeChart);
            });
        });
    }

    void ComposeChart(IContainer container)
    {
        if (_model.AdditionalCharts == null || _model.AdditionalCharts.Count == 0) return;
        container.Row(row =>
        {
            row.RelativeItem().Column(column =>
            {
                foreach (ChartData chartData in _model.AdditionalCharts)
                {
                    column
                        .Item()
                        .PaddingBottom(10)
                        .Text(chartData.Name)
                        .Style(semiTitleStyle);
                    column
                        .Item()
                        .Image(ChartGenerator.GenerateChart(chartData));
                }

            });
        });
    }
    void ComposeFooter(IContainer container)
    {
        container.AlignCenter().Text(x =>
        {
            x.CurrentPageNumber();
        });
    }
}