using System.Diagnostics;
using MongoDB.Driver;
using PowerQualityManageService.Core.Repositories.Abstract;
using PowerQualityManageService.Infrastructure.MongoDBInfrastructure.Abstract;
using PowerQualityManageService.Model.Models;

namespace PowerQualityManageService.Core.Repositories.Concrete;

public class LocalFilesRepository : ILocalFilesRepository
{
    public bool DeleteFile(string fileName)
    {
        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Reports", fileName);
        try
        {
            File.Delete(filePath);
            return true;
        }
        catch (Exception ex) { return false; }
    }

    public async Task<byte[]> GetFile(string fileName)
    {
        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Reports", fileName);
        return await File.ReadAllBytesAsync(filePath);
    }
    public void PreviewFile(string fileName)
    {
        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Reports", fileName);
        Process.Start("explorer.exe", filePath);
    }
}