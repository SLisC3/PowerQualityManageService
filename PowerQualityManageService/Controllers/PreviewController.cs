using Microsoft.AspNetCore.Mvc;
using PowerQualityManageService.Core.Repositories.Abstract;
using PowerQualityManageService.Core.Services.Abstract;
using System.Data;

namespace PowerQualityManageService.Controllers;

[ApiController]
[Route("[controller]")]
public class PreviewController : Controller
{
    private readonly IDataService _dataManagementService;
    private readonly IDataManagementDbRepository _rep;

    public PreviewController(IDataService dataManagementService, IDataManagementDbRepository rep)
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
    public async Task<ActionResult<DataTable>> Data(string measuringPoint, DateTime startDate, DateTime endDate)
    {
        var res = await _dataManagementService.GetSamplesDt(startDate, endDate, measuringPoint, null);
        return res != null ? Ok(res) : NotFound();
    }

    [HttpGet]
    [Route("MeasuringPoints")]
    public async Task<ActionResult<List<string>>> MeasuringPoints()
    {
        throw new NotImplementedException();
    }
}
