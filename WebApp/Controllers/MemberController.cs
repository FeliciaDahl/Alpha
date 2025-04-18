using Business.Interfaces;
using Business.Services;
using Domain.Dto;
using Domain.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Controllers;

public class MemberController(IMemberService memberService, IFileService fileService) : Controller
{

    private readonly IMemberService _memberService = memberService;
    private readonly IFileService _fileService = fileService;

    public IActionResult Index()
    {
        return View();
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

}

