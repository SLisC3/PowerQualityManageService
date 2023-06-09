using PowerQualityManageService.Model.Models;

namespace PowerQualityManageService.Core.Services.Abstract;

public interface ITemplateService
{
    Task<List<Template>> GetTemplates();
    Task<Template?> AddTemplate(Template template);
    Task<bool> DeleteTemplate(string name);
    Task<Template?> EditTemplate(string name, Template template);
    Task<Template?> GetTemplateByName(string name);
}
