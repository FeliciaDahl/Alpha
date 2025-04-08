
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Entites;
using Data.Interfaces;
using Domain.Dto;
using Domain.Extensions;
using Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace Business.Services;

public class MemberService(IMemberRepository memberRepository, UserManager<MemberEntity> userManager) : IMemberService
{
    private readonly IMemberRepository _memberRepository = memberRepository;
    private readonly UserManager<MemberEntity> _userManager = userManager;

    public async Task<MemberResult> GetMembersAsync()
    {
        var result = await _memberRepository.GetAllAsync();
        return result.MapTo<MemberResult>();
    }

    public async Task<ServiceResult<Member>> AddMember(MemberRegistrationForm form)
    {

        var memberExist = await _memberRepository.ExistsAsync(m => m.Email == form.Email);

        if (memberExist.Succeeded && memberExist.Result)
            return ServiceResult<Member>.Failed(400, "Member with this email already exist");

        await _memberRepository.BeginTransactionAsync();

        try
        {
            var memberEntity = form.MapTo<MemberEntity>();
            var result = await _userManager.CreateAsync(memberEntity);
            await _memberRepository.SaveAsync();
            await _memberRepository.CommitTransactionAsync();

            var member = memberEntity.MapTo<Member>();
            return ServiceResult<Member>.Success(member);

        }
        catch
        {
            await _memberRepository.RollbackTransactionAsync();
            return ServiceResult<Member>.Failed(500, "Error while adding member");
        }

    }
}
