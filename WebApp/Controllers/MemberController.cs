using Business.Interfaces;
using Business.Services;
using Domain.Dto;
using Domain.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Controllers;

public class MemberController(IMemberService memberService) : Controller
{

    private readonly IMemberService _memberService = memberService;

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

        var registrationForm = model.MapTo<MemberSignUpForm>();

        var result = await _memberService.AddMember(registrationForm);

        if (result.Succeeded)
        {
            return RedirectToAction("Members", "Admin");
        }

        return BadRequest(new { sucess = false });
    }

}

