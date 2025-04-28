using Business.Interfaces;
using Domain.Extensions;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Models;

[ViewComponent]
public class HeaderViewComponent(IAuthenticationService authService) : ViewComponent
{

    public readonly IAuthenticationService _authService = authService;


    public async Task<IViewComponentResult> InvokeAsync()
    {
        var memberEnitiy = await _authService.GetLoggedInUserAsync();

        if (memberEnitiy != null)
        {
            var loggedInMember = memberEnitiy.MapTo<Member>();
            return View("_Header", loggedInMember);
        }

        return View("_Header" );
    }

}
