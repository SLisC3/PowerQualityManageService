using PowerQualityManageService.Model.Models;

namespace PowerQualityManageService.Core.Repositories.Abstract;

public interface ITemplateRepository
{
    Task<Template> AddTemplate(Template template);
    Task<bool> DeleteTemplate(int id);
    Task<Template?> UpdateTemplate(int id, Template template);
    Task<Template?> GetTemplateById(int id);
}
