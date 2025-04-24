using Business.Interfaces;
using Business.Services;
using Data.Entites;
using Domain.Dto;
using Domain.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Controllers;

public class MemberController(IMemberService memberService, IFileService fileService, RoleManager<MemberEntity> roleManager) : Controller
{

    private readonly IMemberService _memberService = memberService;
    private readonly IFileService _fileService = fileService;
    private readonly RoleManager<MemberEntity> _roleManager = roleManager;

    public async Task<IActionResult> Members()
    {
        var membersResult = await _memberService.GetAllMembersAsync();
        var roles = await LoadRoleListAsync();
        var viewModel = new MemberViewModel
        {
            Roles = roles,
            Members = membersResult.Result!.ToList(),

            MemberRegistration = new MemberRegistrationViewModel()
            {
                Roles = roles
            },

            MemberEdit = new MemberEditViewModel()
            {
                Roles = roles

            }
        };

        viewModel.MemberRegistration.Roles = viewModel.Roles;
        viewModel.MemberEdit.Roles = viewModel.Roles;

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> AddMember(MemberRegistrationViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState
                .Where(x => x.Value?.Errors.Count > 0)
                .ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value?.Errors.Select(e => e.ErrorMessage).ToArray()
                );
            return BadRequest(new { sucess = false, errors });
        }

        if (model.MemberImage != null)
        {
            var filePath = await _fileService.SaveFileAsync(model.MemberImage, "members");
            model.MemberImagePath = filePath;
        }

        var registrationForm = model.MapTo<MemberSignUpForm>();

        var result = await _memberService.AddMember(registrationForm);

        if (result.Succeeded)
        {
            return RedirectToAction("Members", "Admin");
        }

        return BadRequest(new { sucess = false });
    }


    private async Task<List<SelectListItem>> LoadRoleListAsync()
    {
        var rolesResult = await _roleManager.Roles.Select(x => new SelectListItem
        {
            Value = x.Id.ToString(),
            Text = x.Name
        }).ToListAsync();

        return rolesResult;

    }
}

