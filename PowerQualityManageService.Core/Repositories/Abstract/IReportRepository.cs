using PowerQualityManageService.Core.PDFGenerator.PageModels;

namespace PowerQualityManageService.Core.Repositories.Abstract;
public interface IReportRepository
{
    string GenerateReport(ReportModel model);
}