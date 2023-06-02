using System.Diagnostics;
using PowerQualityManageService.Core.Repositories.Abstract;

namespace PowerQualityManageService.Core.Repositories.Concrete;

public class ReportRepository : IReportRepository
{
    public bool Delete(string fileName)
    {
        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Reports", fileName);
        try
        {
            File.Delete(filePath);
            return true;
        }
        catch (Exception ex) { return false; }
    }

    public async Task<byte[]> Get(string fileName)
    {
        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Reports", fileName);
        return await File.ReadAllBytesAsync(filePath);
    }
    public void Preview(string fileName)
    {
        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Reports", fileName);
        Process.Start("explorer.exe", filePath);
    }
}