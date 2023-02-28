using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        
        var dt = CSVHelper.ConvertCSVtoDataTable(stream);

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


