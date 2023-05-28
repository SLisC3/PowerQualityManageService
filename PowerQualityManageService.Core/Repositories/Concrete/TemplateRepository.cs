using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using MongoDB.Driver;
using PowerQualityManageService.Core.Repositories.Abstract;
using PowerQualityManageService.Infrastructure.Models;
using PowerQualityManageService.Infrastructure.MongoDBInfrastructure.Concrete;
using PowerQualityManageService.Infrastructure.SQLServerInfrastructure.Concrete;


namespace PowerQualityManageService.Core.Repositories.Concrete;

public class TemplateRepository : ITemplateRepository
{
    private readonly IMongoCollection<Template> _templates;
    public TemplateRepository(MongoDbContext context)
    {
        _templates = context.Templates;
    }


    public async Task<Template> AddTemplate(Template template)
    {
        try
        {
            await _templates.InsertOneAsync(template);
            return template;
        }
        catch(Exception ex) { return null; }
    }
    public async Task<Template?> GetTemplateById(int id)
    {
        return await _templates.Find(x=> x.Id == id).FirstOrDefaultAsync();
    }
    public async Task<Template?> UpdateTemplate(int id, Template template)
    {
        var entity = await _templates.Find(x => x.Id == id).FirstOrDefaultAsync();
        if (entity == null) { return null; }
        entity.Name = template.Name;
        entity.Description = template.Description;
        entity.Charts = template.Charts;  
        await _templates.ReplaceOneAsync(x=>x.Id == id, entity);
        return entity;
    }
    public async Task<bool> DeleteTemplate(int id)
    {
        var res = await _templates.DeleteOneAsync(x=>x.Id == id);    
        if (res.DeletedCount == 0) { return false; }
        return true;
    }
}
