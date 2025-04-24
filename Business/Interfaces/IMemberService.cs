using Business.Models;
using Domain.Dto;
using Domain.Models;

namespace Business.Interfaces;

public interface IMemberService
{
    Task<ServiceResult<Member>> AddMember(MemberSignUpForm form);
    Task<ServiceResult<bool>> DeleteMemberAsync(string id);
    Task<ServiceResult<bool>> EditMemberAsync(string id, MemberEditForm form);
    Task<ServiceResult<IEnumerable<Member>>> GetAllMembersAsync();
    Task<ServiceResult<Member>> GetMemberAsync(string id);
}