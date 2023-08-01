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

    public PreviewController(IDataService dataManagementService)
    {
        _dataManagementService = dataManagementService;
    }

    public async Task<IActionResult> Index()
    {
        ViewBag.CurrentArea = "Podgląd";
        var points = await _dataManagementService.GetMeasuringPoints();
        var res = points.Select(x => new SelectListItem { Text = x, Value = x }).ToList();
        ViewBag.MeasuringPoints = res;
        var startEndDate = await _dataManagementService.GetStartEndDate();
        ViewBag.StartDate = startEndDate.Item1;
        ViewBag.EndDate = startEndDate.Item2;
        return View();
    }


    [HttpGet]
    [Route("Data")]
    public async Task<ActionResult<DataTable>> Data(string measuringPoint, DateTime startDate, DateTime endDate)
    {
        var res = await _dataManagementService.GetSamplesDt(startDate, endDate, measuringPoint, null);
        return res != null ? Ok(res) : NotFound();
    }
}
