using PowerQualityManageService.Core.Repositories.Abstract;
using PowerQualityManageService.Core.Services.Abstract;
using PowerQualityManageService.Model.Models;
using System.ComponentModel;

namespace PowerQualityManageService.Core.Services.Concrete;

public class TemplateService : ITemplateService
{
    private readonly ITemplateRepository _templateRepository;

    public TemplateService(ITemplateRepository templateRepository)
    {
        _templateRepository = templateRepository;
    }

    public async Task<List<Template>> GetTemplates()
    {
        return await _templateRepository.GetTemplates();
    }

    public async Task<Template?> AddTemplate(Template template)
    {
        return await _templateRepository.AddTemplate(template);
    }
    public async Task<Template?> GetTemplateByName(string name)
    {
        return await _templateRepository.GetTemplateByName(name);
    }
    public async Task<Template?> EditTemplate(string name, Template updatedTemplate)
    {
        return await _templateRepository.UpdateTemplate(name, updatedTemplate);
    }
    public async Task<bool> DeleteTemplate(string name)
    {
        return await _templateRepository.DeleteTemplate(name);
    }

    public async Task<List<string>> GetTemplatesNames()
    {
        return await _templateRepository.GetTemplatesNames();
    }
}
