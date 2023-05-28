using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PowerQualityManageService.Core.Helpers;
using PowerQualityManageService.Core.PDFGenerator.Abstract;
using PowerQualityManageService.Core.PDFGenerator.PageModels;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Resources;

namespace PowerQualityManageService.Core.PDFGenerator.PreparedPages;

public class OpeningPage : IBasePage
{
    private readonly OpeningPageModel _model;

    public OpeningPage(OpeningPageModel model)
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
            row.RelativeItem().AlignRight().Height(100).Column(column =>
            {
                column.Item().Text(text =>
                {
                    text.Span(ResourceHelper.Instance.GetString("Date")).Style(italicsStyle);
                    text.Span($"{DateTime.Now}").Style(italicsStyle);
                });
                column.Item().Text(text =>
                {
                    text.Span(ResourceHelper.Instance.GetString("CreatedBy")).Style(italicsStyle);

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
                column.Item().AlignCenter().Text(ResourceHelper.Instance.GetString("Title")).Style(titleStyle);
                column.Item().Height(30);
                column.Item().Text(ResourceHelper.Instance.GetString("Signature")).Style(normalStyle);
                column.Item().Height(40);
                column.Item().Text(ResourceHelper.Instance.GetString("Ordinance")).Style(normalStyle);
                column.Item().Height(80);
                column.Item().Text(text =>
                {
                    text.Span(ResourceHelper.Instance.GetString("GeneratedFor")).Style(normalStyle);
                    text.Span($"{_model.StationName}").Style(italicsStyle);
                });
                column.Item().Text(ResourceHelper.Instance.GetString("DateRange")).Style(normalStyle);
                column.Item().Text(text =>
                {
                    text.Span(ResourceHelper.Instance.GetString("From")).Style(normalStyle);
                    text.Span($"{_model.FromDate}").Style(italicsStyle);
                });
                column.Item().Text(text =>
                {
                    text.Span(ResourceHelper.Instance.GetString("To")).Style(normalStyle);
                    text.Span($"{_model.ToDate}").Style(italicsStyle);
                });

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
