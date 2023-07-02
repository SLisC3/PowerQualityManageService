namespace PowerQualityManageService.Core.Repositories.Abstract;

public interface ILocalFilesRepository
{
    public bool DeleteFile(string fileName);
    public Task<byte[]> GetFile(string fileName);
    public void PreviewFile(string fileName);
}
