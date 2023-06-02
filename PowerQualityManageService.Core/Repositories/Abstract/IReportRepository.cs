namespace PowerQualityManageService.Core.Repositories.Abstract;

public interface IReportRepository
{
    public bool Delete(string fileName);
    public Task<byte[]> Get(string fileName);
    public void Preview(string fileName);
}
