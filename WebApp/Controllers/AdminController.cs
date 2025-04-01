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

        //[HttpPost]
        //public async Task<IActionResult> AddMember(MemberRegistrationViewModel model)
        //{

        //    if (!ModelState.IsValid)
        //        return View(model);

        //    MemberSignUpForm memberSignUpForm = model;


        //    if (await _authenticationService.ExistAsync(model.Email))
        //    {
        //        ModelState.AddModelError("Email", "Email already exists");
        //        return View(model);
        //    }

        //    var result = _authenticationService.CreateAsync(memberSignUpForm);

        //    if (result.Result)
        //        return RedirectToAction("SignIn", "Auth");


        //    ModelState.AddModelError("NotCreated", "Something went wrong, User not created.");
        //    return View(model);

        //}



        //[Authorize(Roles = "admin")]
        public IActionResult Clients()
        {
            return View();
        }


        public IActionResult Clientss()
        {
            return View();
        }
    }
}
