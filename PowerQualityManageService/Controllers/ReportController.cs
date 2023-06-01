using Microsoft.AspNetCore.Mvc;
using PowerQualityManageService.Core.Services.Abstract;
using PowerQualityManageService.Model.Models;

namespace PowerQualityManageService.Controllers;

[ApiController]
[Route("[controller]")]
public class ReportController : Controller
{
    private readonly IReportService _reportService;
    public ReportController(IReportService reportService)
    {
        _reportService = reportService;
    }

    [HttpGet]
    [Route("Report")]
    public async Task<ActionResult> Report(int templateId, [FromQuery]ResultDefinition resultDefinition)
    {
        var test = await _reportService.GenerateReport(templateId, resultDefinition);
        return Ok();
    }

}
