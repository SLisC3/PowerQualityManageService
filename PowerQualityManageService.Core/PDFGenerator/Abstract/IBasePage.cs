using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace PowerQualityManageService.Core.PDFGenerator.Abstract;

public abstract class IBasePage
{
    public TextStyle titleStyle = TextStyle.Default.FontSize(20).SemiBold().FontColor(Colors.Blue.Medium);
    public TextStyle semiTitleStyle = TextStyle.Default.FontSize(14).SemiBold();
    public TextStyle italicsStyle = TextStyle.Default.FontSize(12).Italic();
    public TextStyle normalStyle = TextStyle.Default.FontSize(12);
    public abstract IDocumentContainer Compose(IDocumentContainer container);

}



