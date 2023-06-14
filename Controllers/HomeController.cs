#pragma warning disable CS8618
﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Session_Workshop.Models;


namespace SessionWorkshop.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [HttpGet("")]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost("login")]
    public IActionResult Login(string name)
    {
        if(name == null)
        {
            return RedirectToAction("Index");
        }
        HttpContext.Session.SetString("Name", name);
        HttpContext.Session.SetInt32("Count", 22);
        return RedirectToAction("Dashboard");
    }

    [HttpGet("dashboard")]
    public IActionResult Dashboard()
    {
        if(HttpContext.Session.GetString("Name") == null)
        {
            return RedirectToAction("Index");
        }
        string? Name = HttpContext.Session.GetString("Name");
        return View();
    }

    [HttpPost("addone")]
    public IActionResult AddOne()
    {
        int? newCount = HttpContext.Session.GetInt32("Count")+1;
        HttpContext.Session.SetInt32("Count", (Int32)newCount);
        return RedirectToAction("Dashboard");
    }

    [HttpPost("subtractone")]
    public IActionResult SubtractOne()
    {
        int? newCount = HttpContext.Session.GetInt32("Count")-1;
        HttpContext.Session.SetInt32("Count", (Int32)newCount);
        return RedirectToAction("Dashboard");
    }

    [HttpPost("timestwo")]
    public IActionResult TimesTwo()
    {
        int? newCount = HttpContext.Session.GetInt32("Count")*2;
        HttpContext.Session.SetInt32("Count", (Int32)newCount);
        return RedirectToAction("Dashboard");
    }

    [HttpPost("addrandom")]
    public IActionResult AddRandom()
    {
        Random rand = new Random();
        int? newCount = HttpContext.Session.GetInt32("Count")+ rand.Next(1,11);
        HttpContext.Session.SetInt32("Count", (Int32)newCount);
        return RedirectToAction("Dashboard");
    }

    [HttpPost("logout")]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}