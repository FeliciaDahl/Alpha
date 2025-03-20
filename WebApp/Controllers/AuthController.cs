using Business.DTO;
using Business.Services;
using Data.Entites;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace WebApp.Controllers
{
    public class AuthController(UserService userService, SignInManager<AppUser> signInManager) : Controller
    {
       

        private readonly UserService _userService = userService;
        private readonly SignInManager<AppUser> _signInManager = signInManager;

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(UserSignUpForm form)
        {

            if (!ModelState.IsValid)
                return View(form);
           

            if (await _userService.ExistAsync(form.Email))
            {
                ModelState.AddModelError("Email", "Email already exists");
                return View(form);
            }

            var result = _userService.CreateAsync(form);
            if(result.Result)
                return RedirectToAction("SignIn", "Auth");
            
            
                ModelState.AddModelError("NotCreated", "Something went wrong, User not created.");
                return View(form);
          

        }

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(UserSignInForm form)
        {

            if (ModelState.IsValid)
            {  
                var result = await _signInManager.PasswordSignInAsync(form.Email, form.Password, false, false);
                if(result.Succeeded)
                return RedirectToAction("Index", "Home");
            }
                
                ViewData["ErrorMessage"] = "Invalid email or password";
                return View(form);


        }

    }
}
