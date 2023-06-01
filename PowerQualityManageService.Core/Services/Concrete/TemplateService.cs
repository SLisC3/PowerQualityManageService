using PowerQualityManageService.Core.Repositories.Abstract;
using PowerQualityManageService.Core.Services.Abstract;
using PowerQualityManageService.Model.Models;

namespace PowerQualityManageService.Core.Services.Concrete;

public class TemplateService : ITemplateService
{
    private readonly ITemplateRepository _templateRepository;

    public TemplateService(ITemplateRepository templateRepository)
    {
        _templateRepository = templateRepository;
    }

    public async Task<Template?> ExtendTemplate(int id, ChartDataDefinition chart) 
    {
        var template = await _templateRepository.GetTemplateById(id);
        if (template == null) return null;
        template.Charts.Append(chart);
        return await _templateRepository.UpdateTemplate(id, template);
    }
    public async Task<Template> AddTemplate(Template template)
    {
        return await _templateRepository.AddTemplate(template);
    }
    public async Task<Template?> GetTemplateById(int id)
    {
        return await _templateRepository.GetTemplateById(id);
    }
    public async Task<Template?> EditTemplate(int id, Template updatedTemplate)
    {
        return await _templateRepository.UpdateTemplate(id, updatedTemplate);
    }
    public async Task<bool> DeleteTemplate(int id)
    {
        return await _templateRepository.DeleteTemplate(id);
    }
}
