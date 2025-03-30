using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Entites;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Business.Services;

public class AuthenticationService : IAuthenticationService
{

    private readonly UserManager<MemberEntity> _userManager;
    private readonly SignInManager<MemberEntity> _signInManager;
    public AuthenticationService(UserManager<MemberEntity> userManager, SignInManager<MemberEntity> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }
    public async Task<bool> ExistAsync(string email)
    {
        return await _userManager.Users.AnyAsync(x => x.Email == email);
    }
    public async Task<bool> CreateAsync(MemberSignUpForm form)
    {
        if (form != null)
        {
            var newMember = MemberFactory.ToEntity(form);
            var result = await _userManager.CreateAsync(newMember, form.Password);
            return result.Succeeded;
        }
        return false;
    }
    public async Task<bool> SignInAsync(MemberSignInForm form)
    {
        if (form != null)
        {
            var result = await _signInManager.PasswordSignInAsync(form.Email, form.Password, false, false);
            return result.Succeeded;
        }
        return false;
    }
    public async Task SignOutAsync()
    {
        await _signInManager.SignOutAsync();
    }

}
