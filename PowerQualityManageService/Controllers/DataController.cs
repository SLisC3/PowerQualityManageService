﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PowerQualityManageService.Core.Helpers;
using System.Data;
using System.Globalization;
using System.IO;
using PowerQualityManageService.Core.Services.Abstract;
using PowerQualityManageService.Pages;

namespace PowerQualityManageService.Controllers;

[ApiController]
[Route("[controller]")]
public class DataController : Controller
{
    private readonly IDataService _service;
    public DataController(IDataService service)
    {
        _service = service;
    }

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
        var dt =_service.LoadData(stream);
        _service.Test(dt);
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


