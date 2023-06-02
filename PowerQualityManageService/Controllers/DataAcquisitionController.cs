using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PowerQualityManageService.Core.Helpers;
using System.Data;
using System.Globalization;
using System.IO;
using PowerQualityManageService.Core.Services.Abstract;
using PowerQualityManageService.Pages;
using Microsoft.Extensions.Caching.Memory;


namespace PowerQualityManageService.Controllers;

[ApiController]
[Route("[controller]")]
public class DataAcquisitionController : Controller
{
    private readonly IDataAcquisitionService _dataManagementService;
    private readonly IMemoryCache _cache;
    public DataAcquisitionController(IDataAcquisitionService service, IMemoryCache cache)
    {
        _dataManagementService = service;
        _cache = cache;
    }

    [HttpGet]
    public ActionResult Index()
    {
        return View();
    }

    [HttpPost]
    [Route("Upload")]
    public async Task<ActionResult<string?>> Upload(IFormFile file)
    {
        string? res = await _dataManagementService.Upload(file);
        return res == null ? NotFound() : Ok(res);
    }

    [HttpGet]
    [Route("Headers")]
    public async Task<ActionResult<DataTable?>> Headers(string fileName)
    {
        var dt = await _dataManagementService.LoadParseHeaders(fileName);
        return dt == null ? NotFound() : Ok(dt);
    }

    [HttpPost]
    [Route("Save")]
    public async Task<ActionResult<int>> Save(string fileName, string measuringPoint)
    {
        int res = await _dataManagementService.Save(fileName, measuringPoint);
        return Ok(res);
    }

    //    [HttpPut]
    //    public ActionResult ModifyHeader(ColumnHeader header)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    [HttpPost]
    //    [Route("Test")]
    //    public ActionResult Test()
    //    {

    //        return Ok();
    //    }
}


