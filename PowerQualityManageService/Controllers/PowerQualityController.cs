using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PowerQualityManageService.Core.Helpers;
using System.Data;
using System.Globalization;
using System.IO;

namespace PowerQualityManageService.Controllers;

[ApiController]
[Route("[controller]")]
public class PowerQualityController : Controller
{

    [HttpGet]
    public ActionResult Index()
    {
        return View();
    }

    [HttpPost]
    [Route("Upload")]
    public ActionResult Upload(IFormFile file)
    {
        var stream = file.OpenReadStream();
        
        var headers = CSVHelper.ReadHeaders(stream);
        var trimmedHeaders = ColumnHeaderRegexHelper.TrimQuotes(headers);
        List<ColumnHeader> columns = new List<ColumnHeader>();
        foreach(var h in trimmedHeaders)
        {
            columns.Add(new ColumnHeader(h));
        }
        //var dt = CSVHelper.ConvertCSVtoDataTable(stream);

        return Ok();

        //try
        //{
        //    return RedirectToAction(nameof(Index));
        //}
        //catch
        //{
        //    return View();
        //}
    }
}


