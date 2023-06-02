using Microsoft.AspNetCore.Mvc;
using PowerQualityManageService.Core.Services.Abstract;
using PowerQualityManageService.Model.Models;
using SharpCompress.Common;
using System.Diagnostics;

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

    [HttpPost]
    [Route("Generate")]
    public async Task<ActionResult<string>> Generate(int templateId, ResultDefinition resultDefinition)
    {

        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        var fileName = await _reportService.GenerateReport(templateId, resultDefinition);
        
        stopwatch.Stop();
        TimeSpan czasWykonania = stopwatch.Elapsed;
        Console.WriteLine("Czas wykonania akcji w controllerze: " + czasWykonania);
        
        return Ok(fileName);
    }

    [HttpGet]
    [Route("Preview")]
    public async Task<ActionResult<string>> Preview(string fileName)
    {
        //Process.Start("explorer.exe", filePath);
        throw new NotImplementedException();
    }

    [HttpPost]
    [Route("SendMail")]
    public async Task<ActionResult> SendMail(string fileName)
    {
        throw new NotImplementedException();
    }

    [HttpGet]
    [Route("Download")]
    public async Task<ActionResult> Download(string fileName)
    {
        throw new NotImplementedException();
    }
}
