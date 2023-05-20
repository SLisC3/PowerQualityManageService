using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using PowerQualityManageService.Core.Repositories.Abstract;
using PowerQualityManageService.Infrastructure.Models;
using PowerQualityManageService.Infrastructure.SQLServerInfrastructure.Concrete;


namespace PowerQualityManageService.Core.Repositories.Concrete;

public class TemplateRepository : ITemplateRepository
{
    private readonly SqlDbContext _context;
    public TemplateRepository(SqlDbContext context)
    {
        _context = context;
    }


    public async Task<Template> AddTemplate(Template template)
    {
        _context.Templates.Add(template);
        await _context.SaveChangesAsync();
        return template;
    }
    public async Task<Template?> GetTemplateById(int id)
    {
        return await _context.Templates.FirstOrDefaultAsync(x=> x.Id == id);
    }
    public async Task<Template?> UpdateTemplate(int id, Template template)
    {
        var entity = await _context.Templates.FirstOrDefaultAsync(x => x.Id == id);
        if (entity == null) { return null; }
        entity.Name = template.Name;
        entity.Description = template.Description;
        entity.Content = template.Content;  
        _context.Templates.Update(entity);
        return entity;
    }
    public async Task<bool> DeleteTemplate(int id)
    {
        var entity = await _context.Templates.FindAsync(id);
        if (entity == null) { return false; }
        _context.Templates.Remove(entity);
        return true;
    }
}
