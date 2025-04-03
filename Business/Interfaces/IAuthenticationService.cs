
using Business.Models;
using Domain.Dto;

namespace Business.Interfaces
{
    public interface IAuthenticationService
    {
        Task<ServiceResult<bool>> CreateAsync(MemberSignUpForm form);
        Task<bool> ExistAsync(string email);
        Task<ServiceResult<bool>> SignInAsync(MemberSignInForm form);
        Task SignOutAsync();
    }
}