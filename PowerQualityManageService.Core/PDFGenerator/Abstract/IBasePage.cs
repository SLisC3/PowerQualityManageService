using PowerQualityManageService.Core.Helpers;
using PowerQualityManageService.Model.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace PowerQualityManageService.Core.PDFGenerator.Abstract;

public abstract class IBasePage
{
    public TextStyle titleStyle = TextStyle.Default.FontSize(20).SemiBold().FontColor(Colors.Blue.Medium);
    public TextStyle semiTitleStyle = TextStyle.Default.FontSize(14).SemiBold();
    public TextStyle boldStyle = TextStyle.Default.FontSize(12).SemiBold();
    public TextStyle italicsStyle = TextStyle.Default.FontSize(12).Italic();
    public TextStyle normalStyle = TextStyle.Default.FontSize(12);
    public abstract IDocumentContainer Compose(IDocumentContainer container);

    protected void ComposeChart(IContainer container, IEnumerable<ChartData> charts)
    {
        if (charts == null || charts.Count() == 0) return;
        container.Row(row =>
        {
            row.RelativeItem().AlignCenter().Column(column =>
            {
                foreach (ChartData chartData in charts)
                {
                    //column
                    //    .Item()
                    //    .PaddingBottom(10)
                    //    .Text(chartData.Name)
                    //    .Style(semiTitleStyle);
                    column
                        .Item()
                        .Image(ChartGenerator.GenerateChart(chartData));
                }
            });
        });
    }
}



