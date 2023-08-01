using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PowerQualityManageService.Core.Helpers;
using PowerQualityManageService.Core.Services.Abstract;
using PowerQualityManageService.Model.Models;
using PowerQualityManageService.Models;
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

    public async Task<IActionResult> Index()
    {
        ViewBag.CurrentArea = "Raporty";
        ViewBag.Reports = await _reportService.GetReports();
        return View();
    }

    [HttpGet]
    [Route("Generate")]
    public async Task<ActionResult> Generate()
    {
        var tmpls = await _reportService.GetTemplatesNames();
        var resTmpls = tmpls.Select(x => new SelectListItem { Text = x, Value = x }).ToList();
        ViewBag.Templates = resTmpls;

        var points = await _reportService.GetMeasuringPoints();
        var resPoints = points.Select(x=> new SelectListItem { Text = x, Value = x }).ToList();
        ViewBag.MeasuringPoints = resPoints;

        var startEndDate = await _reportService.GetStartEndDate();
        ViewBag.StartDate = startEndDate.Item1;
        ViewBag.EndDate = startEndDate.Item2;
        return View();
    }

    [HttpPost]
    [Route("Generate")]
    public async Task<ActionResult<string>> Generate([FromForm]AddReportModel model)
    {

        string templateName = model.Template;
        ResultDefinition resultDefinition = new ResultDefinition()
        {
            DateFrom = model.DateFrom,
            DateTo = model.DateTo, 
            MeasuringPoint = model.MeasuringPoint           
        };
        string reportName = model.Name;
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        await _reportService.GenerateReport(templateName, resultDefinition,reportName);
        
        stopwatch.Stop();
        TimeSpan czasWykonania = stopwatch.Elapsed;
        Console.WriteLine("Czas wykonania akcji w controllerze: " + czasWykonania);

        
        ViewBag.Reports = await _reportService.GetReports();
        return View("Index");
    }

    [HttpGet]
    [Route("Preview")]
    public async Task<ActionResult> Preview(string fileName)
    {
        string filepath = fileName.ToFilePath();
        _reportService.PreviewReport(filepath);
        ViewBag.Reports = await _reportService.GetReports();
        return View("Index");
    }

    [HttpGet]
    [Route("SendMail")]
    public ActionResult SendMail(string fileName)
    {
        ViewBag.FileName = fileName;
        return View();
    }

    [HttpPost]
    [Route("SendMail")]
    public async Task<ActionResult> SendMail([FromForm]MailModel model)
    {
        bool result = await _reportService.SendMail(model);
        ViewBag.Reports = await _reportService.GetReports();// MockModels();
        return View("Index");
    }

    [HttpGet]
    [Route("Download")]
    public async Task<ActionResult> Download(string fileName)
    {
        string filepath = fileName.ToFilePath();
        byte[] file = await _reportService.Download(filepath);
        Response.Headers.Clear();
        Response.Headers.Add("Content-Disposition", $"attachment; filename=\"{filepath}\"");
        return File(file, "application/octet-stream");
    }

    [HttpGet]
    [Route("Delete")]
    public async Task<ActionResult> Delete(string fileName)
    {
        bool result = await _reportService.Delete(fileName);
        if (result == false) return View("Error");
        ViewBag.Reports = await _reportService.GetReports();
        return View("Index");
    }

    private List<ReportDTOModel> MockModels()
    {
        return new List<ReportDTOModel>() {
            new ReportDTOModel() { Name = "a", Template = "b", DateRange = "c", MeasuringPoint = "d" },
            new ReportDTOModel() { Name = "a2", Template = "b2", DateRange = "c2", MeasuringPoint = "d2" }};
    }
}
