using Business.Interfaces;
using Business.Models;
using Business.Services;
using Data.Entites;
using Domain.Dto;
using Domain.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApp.Models;


namespace WebApp.Controllers;

public class AuthController(IAuthenticationService authenticationService,  SignInManager<MemberEntity> signInManager) : Controller
{
   
    private readonly IAuthenticationService _authenticationService = authenticationService;
    private readonly SignInManager<MemberEntity> _signInManager = signInManager;

    public IActionResult SignUp()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> SignUp(MemberSignUpViewModel model)
    {

        if (!ModelState.IsValid)
            return View(model);

         var signUpForm = model.MapTo<MemberSignUpForm>();


        if (await _authenticationService.ExistAsync(model.Email))
        {
            ModelState.AddModelError("Email", "Email already exists");
            return View(model);
        }

        var result = await _authenticationService.CreateAsync(signUpForm);

        if(!result.Succeeded)
        {
            ModelState.AddModelError("NotCreated", "Something went wrong, User not created.");
            return View(model);
        }
        return RedirectToAction("SignIn", "Auth");


    }

    [HttpGet]
    public IActionResult SignIn()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> SignIn(MemberSignInViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.ErrorMessage = "Enter email and password to log in";
            return View(model);
        }
           

        var signInForm = model.MapTo<MemberSignInForm>();

        ViewBag.ErrorMessage = null!;

        if (!await _authenticationService.ExistAsync(signInForm.Email))
        {
            ViewBag.ErrorMessage = "Email does not exist.";
            return View(model);
        }

        var result = await _authenticationService.SignInAsync(signInForm);

        if (!result.Succeeded)
        {
            ViewData["ErrorMessage"] = "Invalid email or password";
            return View(model);
        }

        var user = await _signInManager.UserManager.FindByEmailAsync(signInForm.Email);
        if (user != null)
        {
            HttpContext.Session.SetString("UserId", user.Id);
            HttpContext.Session.SetString("FirstName", user.FirstName);
            HttpContext.Session.SetString("LastName", user.LastName);
        }



        return RedirectToAction("Index", "Admin");
    }

    public async new Task<IActionResult> SignOut()
    {
        await _authenticationService.SignOutAsync();
        return RedirectToAction("SignIn", "Auth");
    }


    //public IActionResult ExternalSignIn(string provider, string returnUrl = null!)
    //{
    //   if(string.IsNullOrEmpty(provider))
    //    {
    //        ModelState.AddModelError("", "Provider is not valid");
    //        return View("SignIn");
    //    }

    //   string redirectUrl = Url.Action("ExternalLoginCallback", "Auth", new { ReturnUrl = returnUrl });
    //}
}
