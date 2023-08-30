using PowerQualityManageService.Core.Helpers;
using PowerQualityManageService.Core.PDFGenerator.Abstract;
using PowerQualityManageService.Core.PDFGenerator.PageModels;
using PowerQualityManageService.Model.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace PowerQualityManageService.Core.PDFGenerator.PreparedPages;

public class ResultPage : IBasePage
{
    private readonly ResultPageModel _model;

    public ResultPage(ResultPageModel model)
    {
        _model = model;
     }
    public override IDocumentContainer Compose(IDocumentContainer container)
    {
        return container.Page(page =>
        {
            page
                .Margin(50);
            page
                .Header()
                .Element(ComposeHeader);
            page
                .Content()
                .Element(ComposeContent);
            page
                .Footer()
                .Element(ComposeFooter);
        });
    }
    void ComposeHeader(IContainer container)
    {
        container.Height(30).Row(row =>
        {
            row.RelativeItem().Column(column =>
            {
                column.Item().AlignCenter().Text(text =>
                {
                    text
                        .Span(ResourceHelper.Instance.GetString("Results"))
                        .Style(titleStyle);
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
                //column
                //    .Item()
                //    .AlignCenter()
                //    .Text(ResourceHelper.Instance.GetString("Compliance"))
                //    .Style(semiTitleStyle);
                column
                    .Spacing(5);
                column
                    .Item()
                    .Element(ComposeTable);
                column
                    .Spacing(10);
                column
                    .Item()
                    .AlignCenter()
                    .Text(ResourceHelper.Instance.GetString("Graphs")) 
                    .Style(semiTitleStyle);
                column
                    .Spacing(5);
                column
                    .Item()
                    .Element(ComposeChart);

            });
        });

    }
    void ComposeTable(IContainer container)
    {
        container.Table(table =>
        {
            table.ColumnsDefinition(columns =>
            {
                columns.ConstantColumn(80);
                columns.ConstantColumn(80);
                columns.RelativeColumn();
            });
            table.Header(header =>
            {
                header
                    .Cell()
                    .Element(CellStyle)
                    .Text(ResourceHelper.Instance.GetString("Norm"));
                header
                    .Cell()
                    .Element(CellStyle)
                    .Text(ResourceHelper.Instance.GetString("Result"));
                header
                    .Cell()
                    .Element(CellStyle)
                    .AlignCenter().Text(ResourceHelper.Instance.GetString("Reason"));

                static IContainer CellStyle(IContainer container)
                {
                    return container.DefaultTextStyle(x => x.SemiBold()).PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Black);
                }
            });

            foreach (SingleNormResult res in _model.Results)
            {
                table
                    .Cell()
                    .Element(CellStyle)
                    .Text(res.Name);
                if (res.Success == null)
                {
                    table
                        .Cell()
                        .Element(CellStyle)
                        .Text(ResourceHelper.Instance.GetString("NoData"));
                }
                else
                {
                    table
                        .Cell()
                        .Element((bool)res.Success ? PassedStyle : FailedStyle)
                        .Text((bool)res.Success ? ResourceHelper.Instance.GetString("Yes") : ResourceHelper.Instance.GetString("No"));
                }
                table
                    .Cell()
                    .Element(CellStyle)
                    .Text(res.Message);

            }
            static IContainer CellStyle(IContainer container)
            {
                return container
                    .BorderBottom(1)
                    .BorderColor(Colors.Grey.Lighten2)
                    .PaddingLeft(10)
                    .PaddingVertical(5);
            }
            static IContainer PassedStyle(IContainer container)
            {
                return container
                    .BorderBottom(1)
                    .BorderColor(Colors.Grey.Lighten2)
                    .Background(Colors.Green.Lighten1)
                    .PaddingLeft(10)
                    .PaddingVertical(5);
            }
            static IContainer FailedStyle(IContainer container)
            {
                return container
                    .BorderBottom(1)
                    .BorderColor(Colors.Grey.Lighten2)
                    .Background(Colors.Red.Lighten1)
                    .PaddingLeft(10)
                    .PaddingVertical(5);
            }

        });
    }

    void ComposeChart(IContainer container)
    {
        if (_model.ResultCharts == null || _model.ResultCharts.Count() == 0) return;
        container.Row(row =>
        {
            row.RelativeItem().AlignCenter().Column(column =>
            {
                foreach (ChartData chartData in _model.ResultCharts)
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

    void ComposeFooter(IContainer container)
    {
        container.AlignCenter().Text(x =>
        {
            x.CurrentPageNumber();
        });
    }
}
