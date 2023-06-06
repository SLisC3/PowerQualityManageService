﻿using Microsoft.AspNetCore.Mvc;
using PowerQualityManageService.Models;
using System.Diagnostics;

namespace PowerQualityManageService.Controllers;
public class HomeController : Controller
{
    public IActionResult Index()
    {
        ViewBag.CurrentArea = "Strona Główna";
        return View();
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
