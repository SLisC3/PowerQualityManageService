using PowerQualityManageService.Core.PDFGenerator.PageModels;
using PowerQualityManageService.Core.Repositories.Abstract;

namespace PowerQualityManageService.Core.Repositories.Concrete;

public class ReportRepository : IReportRepository
{
    public ReportRepository()
    {

    }

    public string GenerateReport(ReportModel model)
    {
        return string.Empty;
    }
}
