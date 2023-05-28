using PowerQualityManageService.Core.Helpers;
using PowerQualityManageService.Core.PDFGenerator.Abstract;
using PowerQualityManageService.Core.PDFGenerator.PageModels;
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