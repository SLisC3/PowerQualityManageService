using PowerQualityManageService.Model.Models;

namespace PowerQualityManageService.Core.Repositories.Abstract;

public interface ITemplateRepository
{
    Task<List<Template>> GetTemplates();
    Task<Template?> AddTemplate(Template template);
    Task<bool> DeleteTemplate(string name);
    Task<Template?> UpdateTemplate(string name, Template template);
    Task<Template?> GetTemplateByName(string name);
}
