using Domain.Models;

namespace WebApp.Models;

public class MemberViewModel
{

    public List<Member> Members { get; set; } = new();
    public MemberRegistrationViewModel MemberRegistration { get; set; } = new();

}
