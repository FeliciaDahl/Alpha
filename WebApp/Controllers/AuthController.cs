using Business.Interfaces;

using Data.Entites;
using Domain.Dto;
using Domain.Extensions;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApp.Hubs;
using WebApp.Models;
using WebApp.Services;


namespace WebApp.Controllers;

public class AuthController(IAuthenticationService authenticationService, SignInManager<MemberEntity> signInManager, UserManager<MemberEntity> userManager,  INotificationDispatcherService notificationDispatch) : Controller
{

    private readonly IAuthenticationService _authenticationService = authenticationService;
    private readonly SignInManager<MemberEntity> _signInManager = signInManager;
    private readonly UserManager<MemberEntity> _userManager = userManager;
    private readonly INotificationDispatcherService _notificationDispatch = notificationDispatch;
    //private readonly INotificationService _notificationService = notificationService;
    //private readonly IHubContext<NotificationHub> _notificationHub = notificationHub;

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

        if (!result.Succeeded)
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

            var notificationEntity = new NotificationEntity
            {
                Message = $"{user.FirstName} {user.LastName} signed in",
                NotificationTypeId = 3,
                Icon = user.Image!,
                TargetGroupId = 1
            };

            await _notificationDispatch.DispatchAsync(notificationEntity, user.Id);

        }



        return RedirectToAction("Index", "Admin");
    }

    public async new Task<IActionResult> SignOut()
    {
        await _authenticationService.SignOutAsync();
        return RedirectToAction("SignIn", "Auth");
    }

    [HttpPost]
    public IActionResult ExternalSignIn(string provider, string returnUrl = null!)
    {
        if (string.IsNullOrEmpty(provider))
        {
            ModelState.AddModelError("", "Provider is not valid");
            return View("SignIn");
        }

        var redirectUrl = Url.Action("ExternalLoginCallback", "Auth", new { returnUrl });
        var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
        return Challenge(properties, provider);
    }

    public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null!, string remoteError = null!)
    {
        returnUrl ??= Url.Content("~/");

        if (!string.IsNullOrEmpty(remoteError))
        {
            ModelState.AddModelError("", $"Error from external provider: {remoteError}");
            return View("SignIn");
        }

        var externalInfo = await _signInManager.GetExternalLoginInfoAsync();
        if (externalInfo == null)
        {
            ModelState.AddModelError("", "Error loading external login information");
            return View("SignIn");
        }
        var signInResult = await _signInManager.ExternalLoginSignInAsync(externalInfo.LoginProvider, externalInfo.ProviderKey, isPersistent: false, bypassTwoFactor: true);
        if (signInResult.Succeeded)
        {
            return LocalRedirect(returnUrl);
        }
        else
        {
            string firstName = externalInfo.Principal.FindFirstValue(ClaimTypes.GivenName) ?? string.Empty;
            string lastName = externalInfo.Principal.FindFirstValue(ClaimTypes.Surname) ?? string.Empty;
            string email = externalInfo.Principal.FindFirstValue(ClaimTypes.Email)!;
            string username = $"ext_{externalInfo.LoginProvider.ToLower()}_{email}";

            var user = new MemberEntity
            {
                UserName = username,
                Email = email,
                FirstName = firstName,
                LastName = lastName
            };

            var identityResult = await _userManager.CreateAsync(user);
            if (identityResult.Succeeded)
            {
                await _userManager.AddLoginAsync(user, externalInfo);
                await _signInManager.SignInAsync(user, isPersistent: false);
                return LocalRedirect(returnUrl);
            }
            foreach (var error in identityResult.Errors)
            {
                ModelState.AddModelError("", error.Description);

            }

            return View("SignIn");


        }
    }
}
