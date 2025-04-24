using Business.Interfaces;
using Data.Entites;
using Domain.Dto;
using Domain.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;
using WebApp.Services;


namespace WebApp.Controllers;

public class MemberController(IMemberService memberService, IFileService fileService, RoleManager<IdentityRole> roleManager, UserManager<MemberEntity> userManager) : Controller
{

    private readonly IMemberService _memberService = memberService;
    private readonly IFileService _fileService = fileService;
    private readonly RoleManager<IdentityRole> _roleManager = roleManager;
    private readonly UserManager<MemberEntity> _userManager = userManager;

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

    [HttpGet]

    public async Task<IActionResult> EditMember(string id)
    {
        var member = await _memberService.GetMemberAsync(id);

        var result = member.Result;

        if (result == null)
        {
            return NotFound();
        }

        var roles = await LoadRoleListAsync();

        var model = new MemberEditViewModel
        {
            Id = result.Id,
            FirstName = result.FirstName,
            LastName = result.LastName,
            Email = result.Email ?? string.Empty,
            PhoneNumber = result.PhoneNumber,
            MemberImagePath = result.Image,
            Roles = roles
        };

        return Ok(model);
    }

    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> EditMember(string id, [FromForm] MemberEditViewModel model)
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

        var editForm = model.MapTo<MemberEditForm>();

        var result = await _memberService.EditMemberAsync(id, editForm);
        if (result.Succeeded)
        {
            return RedirectToAction("Members", "Admin");
        }

        return BadRequest(new { sucess = false });

    }


    private async Task<List<SelectListItem>> LoadRoleListAsync()
    {
        return await _roleManager.Roles.Select(x => new SelectListItem
        {
            Value = x.Id,
            Text = x.Name
        }).ToListAsync();
    }
}

