using PowerQualityManageService.Model.Models;

namespace PowerQualityManageService.Core.Services.Abstract;
public interface IReportService
{
    Task<string?> GenerateReport(int templateId, ResultDefinition resultDefinition);
    void PreviewReport(string fileName);
    Task<bool> SendMail(string fileName, MailModel model);
    Task<byte[]> Download(string fileName);
    bool Delete(string fileName); 
}