
using Business.Factories;
using Business.Models;
using Data.Entites;
using Data.Interfaces;
using Domain.Extensions;
using Microsoft.AspNetCore.Identity;

namespace Business.Services;

public class MemberService(IMemberRepository memberRepository, UserManager<MemberEntity> userManager)
{
    private readonly IMemberRepository _memberRepository = memberRepository;
    private readonly UserManager<MemberEntity> _userManager = userManager;

    public async Task<MemberResult> GetMembersAsync()
    {
        var result = await _memberRepository.GetAllAsync();
        return result.MapTo<MemberResult>();
    }
}
