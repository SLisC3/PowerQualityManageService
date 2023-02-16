using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PowerQualityManageService.Controllers;

[ApiController]
[Route("[controller]")]
public class DataController : Controller
{
    
    [HttpGet]
    public ActionResult Index()
    {
        return View();
    }

    // POST: DataController/Create
    [HttpPost]
    [Route("Upload")]
    public ActionResult Create(IFormFile file)
    {

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
