using Business.Models;
using Domain.Dto;
using Domain.Models;

namespace Business.Interfaces
{
    public interface IMemberService
    {
        Task<ServiceResult<Member>> AddMember(MemberSignUpForm form);
        Task<MemberResult> GetAllMembersAsync();
    }
}