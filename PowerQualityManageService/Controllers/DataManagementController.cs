using Microsoft.AspNetCore.Mvc;
using PowerQualityManageService.Core.Repositories.Abstract;
using PowerQualityManageService.Core.Services.Abstract;
using System.Data;

namespace PowerQualityManageService.Controllers;

[ApiController]
[Route("[controller]")]
public class DataManagementController : Controller
{
    private readonly IDataService _dataManagementService;
    private readonly IDataManagementDbRepository _rep;

    public DataManagementController(IDataService dataManagementService, IDataManagementDbRepository rep)
    {
        _dataManagementService = dataManagementService;
        _rep = rep;
    }

    public IActionResult Index()
    {
        ViewBag.CurrentArea = "Podgląd";
        return View();
    }


    [HttpGet]
    [Route("Data")]
    public async Task<ActionResult<DataTable>> Data(DateTime startDate, DateTime endDate, string measuringPoint)
    {
        var res = _dataManagementService.GetSamplesDt(startDate, endDate, measuringPoint, null);
        return Ok();
    }

    [HttpGet]
    [Route("MeasuringPoints")]
    public async Task<ActionResult<List>

