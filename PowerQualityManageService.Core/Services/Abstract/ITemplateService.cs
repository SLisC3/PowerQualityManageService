using PowerQualityManageService.Infrastructure.Models;


namespace PowerQualityManageService.Core.Services.Abstract;

public interface ITemplateService
{
    Task<Template> AddTemplate(Template template);
    Task<bool> DeleteTemplate(int id);
    Task<Template?> EditTemplate(int id, Template template);
    Task<Template?> GetTemplateById(int id);
}
