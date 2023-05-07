﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using MongoDB.Driver;
using PowerQualityManageService.Core.Helpers;
using PowerQualityManageService.Core.Repositories.Abstract;
using PowerQualityManageService.Core.Services.Abstract;
using PowerQualityManageService.Core.Utils.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace PowerQualityManageService.Core.Services.Concrete;
public class DataManagementService : IDataManagementService
{
    private readonly IDataManagementRepository _localRepository;
    private readonly IDataManagementDbRepository _dbRepository;
    private readonly IMemoryCache _memoryCache;

    public DataManagementService(
        IDataManagementRepository localRepository,
        IDataManagementDbRepository dbRepository,
        IMemoryCache memoryCache)
    {
        _localRepository = localRepository;
        _dbRepository = dbRepository;
        _memoryCache = memoryCache;
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
        DataTable data = await _localRepository.ReadRows(stream,headers,5); // TODO take from config number of rows
        return data;
    }

    public async Task<int> Save(string fileName)
    {
        MemoryCacheEntryOptions? cacheEntryOptions = new MemoryCacheEntryOptions()
            .SetSlidingExpiration(TimeSpan.FromMinutes(30));
        _memoryCache.GetOrCreate(fileName + "Status", entry =>
        {
            entry.SetOptions(cacheEntryOptions);
            return LoadStatusEnum.InProgress;
        });

        try
        {
            Stream? stream = _localRepository.Download(fileName);
            if (stream == null || stream.Length == 0) { return 0; }
            List<ColumnHeader> headers = await _localRepository.GetHeaders(stream);
            int insertedRows = 0;
            while (stream.Position < stream.Length)
            {
                DataTable dt = await _localRepository.ReadRowsNoDispose(stream, headers, 1000);
                insertedRows += await _dbRepository.InsertDataFromDataTable(dt);
                dt.Dispose();
            }
            stream.Dispose();
            return insertedRows;

        }
        catch (Exception)
        {
            _memoryCache.GetOrCreate(fileName + "Status", entry =>
            {
                entry.SetOptions(cacheEntryOptions);
                return LoadStatusEnum.ErrorWhileLoading;
            });
            return 0;
        }


    }
}
