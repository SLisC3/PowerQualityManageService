using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using PowerQualityManageService.Core.Repositories.Abstract;
using PowerQualityManageService.Infrastructure.MongoDBInfrastructure.Abstract;
using PowerQualityManageService.Model.Models;

namespace PowerQualityManageService.Core.Repositories.Concrete;
public class TemplateRepository : ITemplateRepository
{
    private readonly IMongoCollection<Template> _templates;
    public TemplateRepository(IMongoDbContext context)
    {
        _templates = context.Templates;
    }


    public async Task<Template?> AddTemplate(Template template)
    {
        if(await _templates.Find(x => x.Name == template.Name).CountDocumentsAsync() > 0) return null;
        try
        {
            await _templates.InsertOneAsync(template);
            return template;
        }
        catch(Exception ex) { return null; }
    }
    public async Task<Template?> GetTemplateByName(string name)
    {
        return await _templates.Find(x=> x.Name == name).FirstOrDefaultAsync();
    }
    public async Task<Template?> UpdateTemplate(string name, Template template)
    {
        var entity = await _templates.Find(x => x.Name == name).FirstOrDefaultAsync();
        if (entity == null) { return null; }
        entity.Name = template.Name;
        entity.Description = template.Description;
        entity.Charts = template.Charts;  
        await _templates.ReplaceOneAsync(x=>x.Name == name, entity);
        return entity;
    }
    public async Task<bool> DeleteTemplate(string name)
    {
        var res = await _templates.DeleteOneAsync(x=>x.Name == name);    
        if (res.DeletedCount == 0) { return false; }
        return true;
    }

    public async Task<List<Template>> GetTemplates()
    {
        return await _templates.Find(x => true).ToListAsync();
    }
}
