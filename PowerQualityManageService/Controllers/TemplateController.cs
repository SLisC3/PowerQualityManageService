using Microsoft.AspNetCore.Mvc;
using PowerQualityManageService.Core.Repositories.Abstract;
using PowerQualityManageService.Core.Services.Abstract;
using PowerQualityManageService.Model.Models;
using PowerQualityManageService.Models;

namespace PowerQualityManageService.Controllers;

[ApiController]
[Route("[controller]")]
public class TemplateController : Controller
{
    private readonly ITemplateService _templateService;

    public TemplateController(ITemplateService templateService)
    {
        _templateService = templateService;
    }
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        ViewBag.CurrentArea = "Szablony";
        ViewBag.Templates = await _templateService.GetTemplates();
        return View();
    }

    [HttpGet]
    [Route("Create")]
    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [Route("Create")]
    public async Task<ActionResult> Create([FromForm] TemplateModel template /*FormCollection collection*/)
    {
        //var res = await _templateService.AddTemplate(template);
        return View("Index");
        
    }

    [HttpDelete]
    [Route("Template")]
    public async Task<ActionResult> Template(string name)
    {
        return await _templateService.DeleteTemplate(name) == true ? Ok() : NotFound("Nie znaleziono");
    }

    [HttpGet]
    [Route("Template")]
    public async Task<ActionResult<Template>> TemplateByName(string name)
    {
        Template? res = await _templateService.GetTemplateByName(name);
        return  res != null ? Ok(res) : NotFound("Nie znaleziono");
    }

    [HttpPut]
    [Route("Template")]
    public async Task<ActionResult> Template(string name, Template template)
    {
        return await _templateService.EditTemplate(name, template) != null? Ok() : BadRequest("Nie udało się edytować");
    }
}
