
using Business.Models;
using Data.Entites;
using Domain.Dto;

namespace Business.Interfaces
{
    public interface IAuthenticationService
    {
        Task<ServiceResult<bool>> CreateAsync(MemberSignUpForm form);
        Task<bool> ExistAsync(string email);
        Task<MemberEntity?> GetLoggedInUserAsync();
        Task<ServiceResult<bool>> SignInAsync(MemberSignInForm form);
        Task SignOutAsync();
    }
}