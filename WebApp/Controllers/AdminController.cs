using Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    //[Authorize]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Projects()
        {
            return View();
        }

        //[Authorize(Roles ="admin")]
        public IActionResult Members()
        {
            return View();
        }
        public IActionResult Clients()
        {
            return View();
        }

        public IActionResult AddClients()
        {
            return View();
        }

    }
}
