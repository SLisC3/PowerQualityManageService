using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PowerQualityManageService.Core.Repositories.Abstract;
using PowerQualityManageService.Core.Services.Abstract;
using PowerQualityManageService.Core.Services.Concrete;
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

    public async Task<IActionResult> Index()
    {
        ViewBag.CurrentArea = "Podgląd";
        var points = await _rep.GetMeasuringPoints();
        var res = points.Select(x => new SelectListItem { Text = x, Value = x }).ToList();
        ViewBag.MeasuringPoints = res;
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
