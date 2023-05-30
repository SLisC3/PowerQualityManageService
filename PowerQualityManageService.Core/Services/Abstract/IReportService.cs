using PowerQualityManageService.Model.Models;

namespace PowerQualityManageService.Core.Services.Abstract;
public interface IReportService
{
    Task<string?> GenerateReport(int templateId, ResultDefinition resultDefinition);
}