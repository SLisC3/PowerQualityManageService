using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using PowerQualityManageService.Core.Helpers;
using PowerQualityManageService.Core.Repositories.Abstract;
using PowerQualityManageService.Core.Services.Abstract;
using PowerQualityManageService.Core.Utils.Enums;
using System.Data;

namespace PowerQualityManageService.Core.Services.Concrete;
public class DataAcquisitionService : IDataAcquisitionService
{
    private readonly IDataAcquisitionRepository _localRepository;
    private readonly IDataManagementDbRepository _dbRepository;
    private readonly CacheHelper _cache;

    public DataAcquisitionService(
        IDataAcquisitionRepository localRepository,
        IDataManagementDbRepository dbRepository,
        IMemoryCache memoryCache)
    {
        _localRepository = localRepository;
        _dbRepository = dbRepository;
        _cache = new CacheHelper(memoryCache);
    }
    public async Task<string?> Upload(IFormFile file)
    {
        return await _localRepository.Upload(file);
    }

    public async Task<DataTable?> LoadParseHeaders(string fileName)
    {
        Stream? stream = _localRepository.Download(fileName);
        if (stream == null) { return null; }
        List<ColumnHeader> headers = await _localRepository.GetHeaders(stream);
        _cache.Set(fileName + "Headers", headers);
        DataTable data = await _localRepository.ReadRows(stream,headers,5); // TODO take from config number of rows
        return data;
    }

    public async Task<int> Save(string fileName, string measuringPoint)
    {
        _cache.Set(fileName + "Status", SaveStatus.InProgress);

        try
        {
            Stream? stream = _localRepository.Download(fileName);
            if (stream == null || stream.Length == 0) { return 0; }
            List<ColumnHeader> headers;
            if (!_cache.TryGetValue(fileName+"Headers",out headers))
            {
                headers = await _localRepository.GetHeaders(stream);
            }

            int insertedRows = 0;
            while (stream.Position < stream.Length)
            {
                DataTable dt = await _localRepository.ReadRowsNoDispose(stream, headers, 50000); // TODO take from config number of rows 
                insertedRows += await _dbRepository.InsertDataFromDataTable(dt, measuringPoint);
                dt.Dispose();
            }
            stream.Dispose();
            return insertedRows;

        }
        catch (Exception)
        {
            _cache.Set(fileName + "Status", SaveStatus.ErrorWhileLoading);
            return 0;
        }


    }

    public async Task<List<string>> GetMeasuringPoints()
    {
        return await _dbRepository.GetMeasuringPoints();
    }
}
