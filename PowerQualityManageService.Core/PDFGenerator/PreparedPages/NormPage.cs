using PowerQualityManageService.Core.Helpers;
using PowerQualityManageService.Core.PDFGenerator.Abstract;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace PowerQualityManageService.Core.PDFGenerator.PreparedPages;

public class NormPage : IBasePage
{
    public override IDocumentContainer Compose(IDocumentContainer container)
    {
        return container.Page(page =>
        {
            page.Margin(50);
            page.Header().Element(ComposeHeader);
            page.Content().Element(ComposeContent);
            page.Footer().Element(ComposeFooter);
        });
    }
    void ComposeHeader(IContainer container)
    {
        container.Height(50).Row(row =>
        {
            row.RelativeItem().Column(column =>
            {
                column.Item().AlignCenter().Text(text =>
                {
                    text.Span(ResourceHelper.Instance.GetString("Norms")).Style(titleStyle);
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
                column.Spacing(10);
                column.Item().Text(ResourceHelper.Instance.GetString("Norm1")).Style(normalStyle);
                column.Item().Text(ResourceHelper.Instance.GetString("Norm1a")).Style(normalStyle);
                column.Item().Text(ResourceHelper.Instance.GetString("Norm1b")).Style(normalStyle);
                column.Item().Text(ResourceHelper.Instance.GetString("Norm2")).Style(normalStyle);
                column.Item().Text(ResourceHelper.Instance.GetString("Norm3")).Style(normalStyle);
                column.Item().Text(ResourceHelper.Instance.GetString("Norm4")).Style(normalStyle);
                column.Item().Text(ResourceHelper.Instance.GetString("Norm4a")).Style(normalStyle);
                column.Item().Text(ResourceHelper.Instance.GetString("Norm4b")).Style(normalStyle);
                byte[] imageData = File.ReadAllBytes(Path.Combine(Directory.GetCurrentDirectory(), "Resources\\Imgs\\harmoniczne3-5.png"));
                column.Item().Image(Path.Combine(Directory.GetCurrentDirectory(), "Resources\\Imgs\\harmoniczne3-5.png"));
                column.Item().Text(ResourceHelper.Instance.GetString("Norm5")).Style(normalStyle);
                column.Item().Text(ResourceHelper.Instance.GetString("Norm6")).Style(normalStyle);
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



