using Business.Models;
using Domain.Dto;
using Domain.Models;

namespace Business.Interfaces;

public interface IMemberService
{
    Task<ServiceResult<Member>> AddMember(MemberSignUpForm form);
    Task<ServiceResult<bool>> DeleteMember(string id);
    Task<ServiceResult<bool>> EditMember(string id, MemberEditForm form);
    Task<ServiceResult<IEnumerable<Member>>> GetAllMembersAsync();
    Task<ServiceResult<Member>> GetMemberAsync(string id);
}