
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Entites;
using Data.Interfaces;
using Data.Repositories;
using Domain.Dto;
using Domain.Extensions;
using Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace Business.Services;

public class MemberService(IMemberRepository memberRepository, UserManager<MemberEntity> userManager) : IMemberService
{
    private readonly IMemberRepository _memberRepository = memberRepository;
    private readonly UserManager<MemberEntity> _userManager = userManager;

  
    public async Task<ServiceResult<Member>> AddMember(MemberSignUpForm form)
    {

        var memberExist = await _memberRepository.ExistsAsync(m => m.Email == form.Email);

        if (memberExist.Succeeded && memberExist.Result)
            return ServiceResult<Member>.Failed(400, "Member with this email already exist");

        await _memberRepository.BeginTransactionAsync();

        try
        {

            var memberEntity = MemberFactory.ToEntity(form);
          
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

    public async Task<ServiceResult<IEnumerable<Member>>> GetAllMembersAsync()
    {
        var result = await _memberRepository.GetAllAsync();
        var members = result.Result?.Select(c => c.MapTo<Member>());

        return ServiceResult<IEnumerable<Member>>.Success(members!);
    }

    public async Task<ServiceResult<Member>> GetMemberAsync(string id)
    {
        var memberResult = await _memberRepository.GetEntityAsync(m => m.Id == id, m => m.MemberAdress!);

        var memberEntity = memberResult.Result;

        if (memberEntity == null)
            return ServiceResult<Member>.Failed(404, "Member not found");

        var member = MemberFactory.ToModel(memberEntity);

        var roles = await _userManager.GetRolesAsync(memberEntity);
        member.Role = roles.FirstOrDefault();


        return ServiceResult<Member>.Success(member);
    }


    public async Task<ServiceResult<bool>> EditMemberAsync(string id, MemberEditForm form)
    {
        var existingMemberResult = await _memberRepository.GetEntityAsync(m => m.Id == id, m => m.MemberAdress!);

        if (!existingMemberResult.Succeeded || existingMemberResult.Result == null)
            return ServiceResult<bool>.Failed(404, "Member not found");

        var memberEntity = existingMemberResult.Result;

        memberEntity.Image = string.IsNullOrWhiteSpace(form.MemberImagePath) ? memberEntity.Image : form.MemberImagePath;
        memberEntity.FirstName = string.IsNullOrWhiteSpace(form.FirstName) ? memberEntity.FirstName : form.FirstName;
        memberEntity.LastName = string.IsNullOrWhiteSpace(form.LastName) ? memberEntity.LastName : form.LastName;
        memberEntity.PhoneNumber = string.IsNullOrWhiteSpace(form.PhoneNumber) ? memberEntity.PhoneNumber : form.PhoneNumber;
        memberEntity.Email = string.IsNullOrWhiteSpace(form.Email) ? memberEntity.Email : form.Email;
        memberEntity.JobTitle = string.IsNullOrWhiteSpace(form.JobTitle) ? memberEntity.JobTitle : form.JobTitle;

        if (memberEntity.MemberAdress == null)
        {
            memberEntity.MemberAdress = new MemberAdressEntity
            {
                UserId = memberEntity.Id, 
                Street = form.Street,
                PostalCode = form.PostalCode,
                City = form.City,
                Country = form.Country
            };
        }
        else
        {
            memberEntity.MemberAdress.Street = string.IsNullOrWhiteSpace(form.Street) ? memberEntity.MemberAdress.Street : form.Street;
            memberEntity.MemberAdress.PostalCode = string.IsNullOrWhiteSpace(form.PostalCode) ? memberEntity.MemberAdress.PostalCode : form.PostalCode;
            memberEntity.MemberAdress.City = string.IsNullOrWhiteSpace(form.City) ? memberEntity.MemberAdress.City : form.City;
            memberEntity.MemberAdress.Country = string.IsNullOrWhiteSpace(form.Country) ? memberEntity.MemberAdress.Country : form.Country;
        }

        if (!string.IsNullOrWhiteSpace(form.Role))
        {
            var currentRole = await _userManager.GetRolesAsync(memberEntity);

            var removeResult = await _userManager.RemoveFromRolesAsync(memberEntity, currentRole);
            if (!removeResult.Succeeded)
                return ServiceResult<bool>.Failed(500, "Failed to remove existing role");

            var addRole = await _userManager.AddToRoleAsync(memberEntity, form.Role);
            if (!addRole.Succeeded)
                return ServiceResult<bool>.Failed(500, "Failed to assign new role");
        }

        await _memberRepository.BeginTransactionAsync();

        try
        {
            var result = await _userManager.UpdateAsync(memberEntity);

          
            await _memberRepository.SaveAsync();

            await _memberRepository.CommitTransactionAsync();
            return ServiceResult<bool>.Success(true);
        }
        catch (Exception e)
        {
            await _memberRepository.RollbackTransactionAsync();
            return ServiceResult<bool>.Failed(500, e.Message);
        }
    }

    public async Task<ServiceResult<bool>> DeleteMemberAsync(string id)
    {
        var existingMemberResult = await _memberRepository.GetEntityAsync(c => c.Id == id);

        if (!existingMemberResult.Succeeded || existingMemberResult.Result == null)
            return ServiceResult<bool>.Failed(404, "Member not found");

        var existingMember = existingMemberResult.Result;

        await _memberRepository.BeginTransactionAsync();

        try
        {
            var result = await _userManager.DeleteAsync(existingMember);
            await _memberRepository.SaveAsync();

            await _memberRepository.CommitTransactionAsync();
            return ServiceResult<bool>.Success(true);
        }
        catch (Exception e)
        {
            await _memberRepository.RollbackTransactionAsync();
            return ServiceResult<bool>.Failed(500, e.Message);
        }
    }
}
