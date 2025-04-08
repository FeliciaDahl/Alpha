using Domain.Dto;
using Domain.Extensions;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers;

public class MemberController : Controller
{

    private

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult AddMember(MemberRegistrationViewModel model)
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

        var registrationForm = model.MapTo<MemberRegistrationForm>();

        var result = 
        if (result.Succeeded)
        {
            return RedirectToAction("Clients", "Admin");
        }

        return BadRequest(new { sucess = false });
    }
}
}
