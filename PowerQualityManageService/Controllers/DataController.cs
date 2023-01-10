using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PowerQualityManageService.Controllers;
public class DataController : Controller
{
    // GET: DataController
    public ActionResult Index()
    {
        return View();
    }

    // POST: DataController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(IFormCollection collection)
    {
        try
        {
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }
}
