
using Business.Factories;
using Business.Models;
using Data.Entites;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;

namespace Business.Services;

public class MemberService(UserManager<MemberEntity> userManager)
{
    private readonly UserManager<MemberEntity> _userManager = userManager;


    public async Task<IEnumerable<Member>> GetMembersAsync()
    {
        var users = await _userManager.Users.ToListAsync();
        return users.Select(MemberFactory.ToModel);
    }

  

}
