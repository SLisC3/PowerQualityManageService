using PowerQualityManageService.Model.Models;

namespace PowerQualityManageService.Core.Services.Abstract;
public interface IReportService
{
    Task<string?> GenerateReport(string templateName, ResultDefinition resultDefinition, string reportName);
    Task PreviewReport(string fileName);
    Task<bool> SendMail(MailModel model);
    Task<byte[]> Download(string fileName);
    Task<bool> Delete(string fileName);
    Task<string> GetFileNameFromReportName(string name);
    Task<List<Report>?> GetReports();
}