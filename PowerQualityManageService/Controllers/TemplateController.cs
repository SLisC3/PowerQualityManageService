using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PowerQualityManageService.Core.Helpers;
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
    private readonly IMapper _mapper;

    public TemplateController(ITemplateService templateService,IMapper mapper)
    {
        _templateService = templateService;
        _mapper = mapper;
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
        var res = await _templateService.AddTemplate(
            new Template() 
            {
                Name = template.Name, 
                Description = template.Description, 
                Charts = template.Charts.ToListOfAdvancedCharts()
            }
            );
        if(res==null) return View("Error");
        ViewBag.Templates = await _templateService.GetTemplates();
        return View("Index");
        
    }

    [HttpGet]
    [Route("Delete")]
    public async Task<ActionResult> Delete(string name)
    {
        bool res =  await _templateService.DeleteTemplate(name);
        if (res == false) return View("Error");
        ViewBag.Templates = await _templateService.GetTemplates();
        return View("Index");
    }

    [HttpGet]
    [Route("Template")]
    public async Task<ActionResult<Template>> TemplateByName(string name)
    {
        Template? res = await _templateService.GetTemplateByName(name);
        return  res != null ? Ok(res) : NotFound("Nie znaleziono");
    }


    [HttpGet]
    [Route("Edit")]
    public async Task<ActionResult> Edit(string name)
    {
        var res = await _templateService.GetTemplateByName(name);
        if(res==null) return View("Error");
        return View(new TemplateEditModel() {PreviousName=res.Name, Name=res.Name,Description=res.Description,Charts = res.Charts.ToListOfSimpleCharts()});
    }

    [HttpPost]
    [Route("Edit")]
    public async Task<ActionResult> Edit([FromForm]TemplateEditModel template)
    {

        var result = await _templateService.EditTemplate(template.PreviousName,
            new Template()
            {
                Name = template.Name,
                Description = template.Description,
                Charts = template.Charts.ToListOfAdvancedCharts()
            });
        if(result==null) return View("Error");
        ViewBag.Templates = await _templateService.GetTemplates();
        return View("Index");
    }
}
