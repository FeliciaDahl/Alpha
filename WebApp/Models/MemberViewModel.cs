using Domain.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Models;

public class MemberViewModel
{
    public List<Member> Members { get; set; } = new();
    public MemberRegistrationViewModel MemberRegistration { get; set; } = new();
    public MemberEditViewModel MemberEdit { get; set; } = new();

    public List<SelectListItem> Roles { get; set; } = new();

}
