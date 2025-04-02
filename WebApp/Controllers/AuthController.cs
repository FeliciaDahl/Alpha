using Business.Interfaces;
using Business.Models;
using Business.Services;
using Data.Entites;
using Domain.Dto;
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

        MemberSignUpForm memberSignUpForm = model;


        if (await _authenticationService.ExistAsync(model.Email))
        {
            ModelState.AddModelError("Email", "Email already exists");
            return View(model);
        }

        var result = _authenticationService.CreateAsync(memberSignUpForm);

        if(result.Result)
            return RedirectToAction("SignIn", "Auth");
        
        
            ModelState.AddModelError("NotCreated", "Something went wrong, User not created.");
            return View(model);
      
    }

    public IActionResult SignIn()
    {
        ViewBag.ErrorMessage = null!;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> SignIn(MemberSignInViewModel model)
    {
        ViewBag.ErrorMessage = null!;

        if (string.IsNullOrEmpty(model.Email) || (string.IsNullOrEmpty(model.Password)))
        {
            ViewBag.ErrorMessage = "Enter email and password to log in";
            return View(model);
        }

        if(!await _authenticationService.ExistAsync(model.Email))
        {
            ViewBag.ErrorMessage = "Email does not exist.";
            return View(model);
        }

        if (ModelState.IsValid)
        {
            MemberSignInForm memberSignInForm = model;

            if (await _authenticationService.SignInAsync(model))
            {
                return RedirectToAction("Index", "Admin");
            }

        }
            
            ViewData["ErrorMessage"] = "Invalid email or password";
            return View(model);

    }

    public async new Task<IActionResult> SignOut()
    {
        await _authenticationService.SignOutAsync();
        return RedirectToAction("Auth", "SignIn");
    }

}
