using PowerQualityManageService.Model.Models;

namespace PowerQualityManageService.Core.Repositories.Abstract;

public interface IReportRepository
{
    Task Add(Report model);
    Task Delete(string fileName);
    Task<List<Report>?> GetAll();
    Task<Report?> GetByName(string fileName);
    Task<string> GetFileNameFromName(string name);
}
