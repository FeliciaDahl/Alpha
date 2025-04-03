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

        if(result.Succeeded)
            return RedirectToAction("SignIn", "Auth");
        
        
            ModelState.AddModelError("NotCreated", "Something went wrong, User not created.");
            return View(model);
      
    }

    public IActionResult SignIn()
    {
        ViewBag.ErrorMessage = null!;
        return View();
    }

    //[HttpPost]
    //public async Task<IActionResult> SignIn(MemberSignInViewModel model)
    //{
    //    ViewBag.ErrorMessage = null!;

    //    if (string.IsNullOrEmpty(model.Email) || (string.IsNullOrEmpty(model.Password)))
    //    {
    //        ViewBag.ErrorMessage = "Enter email and password to log in";
    //        return View(model);
    //    }

    //    if(!await _authenticationService.ExistAsync(model.Email))
    //    {
    //        ViewBag.ErrorMessage = "Email does not exist.";
    //        return View(model);
    //    }

      
    //        signInForm = model.MapTo<MemberSignInForm>();

    //    result = await _authenticationService.SignInAsync(signInForm)
          
    //            return RedirectToAction("Index", "Admin");
          

        
            
    //        ViewData["ErrorMessage"] = "Invalid email or password";
    //        return View(model);

    //}

    public async new Task<IActionResult> SignOut()
    {
        await _authenticationService.SignOutAsync();
        return RedirectToAction("Auth", "SignIn");
    }

}
