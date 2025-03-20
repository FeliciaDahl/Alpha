
using Business.DTO;
using Data.Entites;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Business.Services;

public class UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
{
    private readonly UserManager<AppUser> _userManager = userManager;
    private readonly SignInManager<AppUser> _signInManager = signInManager;

    public async Task<bool> ExistAsync(string email)
    {
        return await _userManager.Users.AnyAsync(x => x.Email == email);
    }

    public async Task<bool> CreateAsync(UserSignUpForm form)
    {
        if( form != null)
        {
            var newUser = new AppUser
            {
                UserName = form.Email,
                FirstName = form.FirstName,
                LastName = form.LastName,
                Email = form.Email,
                PhoneNumber = form.Phone,
             
            };

            var result = await _userManager.CreateAsync(newUser, form.Password);
            return result.Succeeded;
        }
            return false;
        }
    }

