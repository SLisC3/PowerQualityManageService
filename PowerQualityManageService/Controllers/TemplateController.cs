using Microsoft.AspNetCore.Mvc;
using PowerQualityManageService.Core.Repositories.Abstract;
using PowerQualityManageService.Core.Services.Abstract;
using PowerQualityManageService.Model.Models;

namespace PowerQualityManageService.Controllers;

[ApiController]
[Route("[controller]")]
public class TemplateController : Controller
{
    private readonly ITemplateService _templateService;
    private readonly ITemplateRepository _templateRepository;

    public TemplateController(ITemplateService templateService, ITemplateRepository templateRepository)
    {
        _templateService = templateService;
        _templateRepository = templateRepository;
    }
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    [Route("Template")]
    public async Task<ActionResult<Template>> Template(Template template)
    {
        return Ok(await _templateRepository.AddTemplate(template));
    }

}
