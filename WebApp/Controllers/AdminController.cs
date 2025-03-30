using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        //[Authorize(Roles = "admin")]
        public IActionResult Clients()
        {
            return View();
        }

    }
}
