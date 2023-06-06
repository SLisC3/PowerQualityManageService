using Microsoft.AspNetCore.Mvc;
using System.Data;
using PowerQualityManageService.Core.Services.Abstract;


namespace PowerQualityManageService.Controllers;

[ApiController]
[Route("[controller]")]
public class DataAcquisitionController : Controller
{
    private readonly IDataAcquisitionService _dataManagementService;
    public DataAcquisitionController(IDataAcquisitionService service)
    {
        _dataManagementService = service;
    }

    [HttpGet]
    public ActionResult Index()
    {
        ViewBag.CurrentArea = "Import danych";
        return View();
    }

    [HttpPost]
    [Route("Upload")]
    public async Task<ActionResult<string?>> Upload([FromForm] IFormFile fileInput)
    {
        string? res = await _dataManagementService.Upload(fileInput);
        if (res != null) { ViewBag.fileName = res; }
        return res == null ? NotFound() : Ok(res);
    }

    [HttpGet]
    [Route("Headers")]
    public async Task<ActionResult<DataTable?>> Headers([FromQuery] string fileName)
    {
        var dt = await _dataManagementService.LoadParseHeaders(fileName);
        if (dt != null) { ViewBag.headers = dt; }
        return dt == null ? NotFound() : Ok(dt);
    }

    [HttpPost]
    [Route("Save")]
    public async Task<ActionResult<int>> Save([FromForm] string fileName, [FromForm] string measuringPoint)
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


