using Business.Models;

namespace Business.Interfaces
{
    public interface IAuthenticationService
    {
        Task<bool> CreateAsync(MemberSignUpForm form);
        Task<bool> ExistAsync(string email);
        Task<bool> SignInAsync(MemberSignInForm form);
        Task SignOutAsync();
    }
}