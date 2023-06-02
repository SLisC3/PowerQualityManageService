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

        string? fileName = await _reportService.GenerateReport(templateId, resultDefinition);
        
        stopwatch.Stop();
        TimeSpan czasWykonania = stopwatch.Elapsed;
        Console.WriteLine("Czas wykonania akcji w controllerze: " + czasWykonania);
        
        return fileName != null ? Ok(fileName) : BadRequest() ;
    }

    [HttpGet]
    [Route("Preview")]
    public async Task<ActionResult> Preview(string fileName)
    {
        _reportService.PreviewReport(fileName);
        return Ok();
    }

    [HttpPost]
    [Route("SendMail")]
    public async Task<ActionResult> SendMail(string fileName, MailModel model)
    {
        bool result = await _reportService.SendMail(fileName,model);
        return result == true ? Ok() : BadRequest();
    }

    [HttpGet]
    [Route("Download")]
    public async Task<ActionResult> Download(string fileName)
    {
        byte[] file = await _reportService.Download(fileName);
        Response.Headers.Clear();
        Response.Headers.Add("Content-Disposition", $"attachment; filename=\"{fileName}\"");
        return File(file, "application/octet-stream");
    }

    [HttpGet]
    [Route("Delete")]
    public async Task<ActionResult> Delete(string fileName)
    {
        bool result = _reportService.Delete(fileName);
        return result == true ? Ok() : NotFound();
    }
}
