using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Entites;
using Domain.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Business.Services;

public class AuthenticationService(UserManager<MemberEntity> userManager, SignInManager<MemberEntity> signInManager, IHttpContextAccessor httpContextAccessor) : IAuthenticationService
{

    private readonly UserManager<MemberEntity> _userManager = userManager;
    private readonly SignInManager<MemberEntity> _signInManager = signInManager;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task<ServiceResult<bool>> CreateAsync(MemberSignUpForm form)
    {
        if (form == null)
        {
            return ServiceResult<bool>.Failed(400, "Required fields can not be empty");
        }

        var memberEntity = MemberFactory.ToEntity(form);

        var member = await _userManager.CreateAsync(memberEntity, form.Password!);
        if (!member.Succeeded)
        {
            return ServiceResult<bool>.Failed(400, "Something went wrong when trying to create user");
        }

        var roleResult = await _userManager.AddToRoleAsync(memberEntity, "User");
        if (!roleResult.Succeeded)
        {
            return ServiceResult<bool>.Failed(400, "Something went wrong when trying to add role to user");
        }

        return ServiceResult<bool>.Success(true);

    }
    public async Task<ServiceResult<bool>> SignInAsync(MemberSignInForm form)
    {
        if (form == null)
        {
            return ServiceResult<bool>.Failed(400, "Required fields can not be empty");
        }

        var result = await _signInManager.PasswordSignInAsync(form.Email, form.Password, false, false);

        if (!result.Succeeded)
        {
            return ServiceResult<bool>.Failed(400, "Invalid email or password");
        }

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


    public async Task<MemberEntity?> GetLoggedInUserAsync()
    {
        var userId = _userManager.GetUserId(_httpContextAccessor.HttpContext.User);

        if (userId == null)
            return null;

        var member = await _userManager.FindByIdAsync(userId);

        return member;
    }

}
