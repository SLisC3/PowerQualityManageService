using Microsoft.AspNetCore.Http;
using PowerQualityManageService.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerQualityManageService.Core.Services.Abstract;
public interface IDataAcquisitionService
{
    public Task<string?> Upload(IFormFile file);
    public Task<DataTable?> LoadParseHeaders(string fileName);
    public Task<int> Save(string fileName, string measuringPoint);
}
