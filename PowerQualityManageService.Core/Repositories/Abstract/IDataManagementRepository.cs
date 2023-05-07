using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using PowerQualityManageService.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerQualityManageService.Core.Repositories.Abstract;

public interface IDataManagementRepository
{
    public Task<string?> Upload(IFormFile file);
    public Stream? Download(string fileName);
    public Task<List<ColumnHeader>> GetHeaders(Stream stream);
    public Task<DataTable> ReadRows(Stream stream, List<ColumnHeader> headers, int count);
    public Task<DataTable> ReadRowsNoDispose(Stream stream, List<ColumnHeader> headers, int count);
}
