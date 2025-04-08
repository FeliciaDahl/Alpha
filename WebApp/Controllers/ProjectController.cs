using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

public class ProjectController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult AddProject()
    {
        return View();
    }
}
