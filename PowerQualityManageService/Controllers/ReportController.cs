using Microsoft.AspNetCore.Mvc;
using PowerQualityManageService.Core.Services.Abstract;

namespace PowerQualityManageService.Controllers;

[ApiController]
[Route("[controller]")]
public class ReportController : Controller
{
    private readonly ITemplateService _templateService;
    private readonly IDataService _dataService;

    public ReportController(ITemplateService templateService, IDataService dataService)
    {
        _templateService = templateService;
        _dataService = dataService;
    }

}
