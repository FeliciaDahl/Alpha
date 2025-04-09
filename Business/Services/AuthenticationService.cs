using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Entites;
using Domain.Dto;
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
  
    public async Task<ServiceResult<bool>> CreateAsync(MemberSignUpForm form)
    {
        if (form == null)
        {
            return ServiceResult<bool>.Failed(400, "Form can not be empty");
        }
        var memberEntity = MemberFactory.ToEntity(form);
        var result = await _userManager.CreateAsync(memberEntity, form.Password!);
        return ServiceResult<bool>.Success(true);

    }
    public async Task<ServiceResult<bool>> SignInAsync(MemberSignInForm form)
    {
        if (form == null)
        {
           return ServiceResult<bool>.Failed(400, "Client name can not be empty");
        }
        
        var result = await _signInManager.PasswordSignInAsync(form.Email, form.Password, false, false);
        return ServiceResult<bool>.Success(true);
    }
    public async Task SignOutAsync()
    {
        await _signInManager.SignOutAsync();
    }

    public async Task<bool> ExistAsync(string email)
    {
        return await _userManager.Users.AnyAsync(x => x.Email == email);
    }

}
