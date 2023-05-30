using Microsoft.AspNetCore.Mvc;
using PowerQualityManageService.Core.Repositories.Abstract;
using PowerQualityManageService.Core.Services.Abstract;
using System.Data;

namespace PowerQualityManageService.Controllers;

[ApiController]
[Route("[controller]")]
public class DataManagementController : Controller
{
    private readonly IDataService _dataManagementService;
    private readonly IDataManagementDbRepository _rep;

    public DataManagementController(IDataService dataManagementService, IDataManagementDbRepository rep)
    {
        _dataManagementService = dataManagementService;
        _rep = rep;
    }
    [HttpGet]
    [Route("Data")]
    public async Task<ActionResult> GetData(DateTime startDate, DateTime endDate, string measuringPoint)
    {
        var keys = new List<string>() { "THD_PhaseToPhase31", "Frequency" };
        var res = _dataManagementService.GetSamples(startDate, endDate, measuringPoint, keys);
        return Ok();
    }
    [HttpGet]
    [Route("Test")]
    public async Task<ActionResult<DataTable>> Test (DateTime startDate, DateTime endDate)
    {
        return Ok(await _rep.GetDataSamplesDT(startDate, endDate));
    }
}
