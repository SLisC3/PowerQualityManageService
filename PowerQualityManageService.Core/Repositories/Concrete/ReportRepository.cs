using System.Diagnostics;
using MongoDB.Driver;
using PowerQualityManageService.Core.Helpers;
using PowerQualityManageService.Core.Repositories.Abstract;
using PowerQualityManageService.Infrastructure.MongoDBInfrastructure.Abstract;
using PowerQualityManageService.Model.Models;

namespace PowerQualityManageService.Core.Repositories.Concrete;

public class ReportRepository : IReportRepository
{
    private readonly IMongoCollection<Report> _reports;
    public ReportRepository(IMongoDbContext context)
    {
        _reports = context.Reports;
    }

    public async Task Delete(string fileName)
    {
        await _reports.DeleteOneAsync(x => x.FileName == fileName);
    }

    public async Task<Report?> GetByName(string fileName)
    {
        return await _reports.Find(x => x.FileName == fileName).FirstAsync();
    }
    public async Task<List<Report>?> GetAll()
    {
        return await _reports.Find(_ => true).ToListAsync();
    }
    public async Task Add(Report model)
    {
        await _reports.InsertOneAsync(model);
    }

    public async Task<string> GetFileNameFromName(string name)
    {
        var result = await _reports.Find(x => x.Name == name).FirstAsync();
        if(result == null) { return string.Empty; }
        return result.FileName;
    }
}