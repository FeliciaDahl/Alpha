
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;


public class AdminController() : Controller
{
    [Authorize]
    public IActionResult Index()
    {
        return View();
    }

    [Authorize]
    [Route("Admin/Denied")]
    public IActionResult Denied()
    {
       
        return View();
    }














}

    
