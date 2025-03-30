using System.ComponentModel.DataAnnotations;

namespace Business.Models;

public class MemberSignInForm
{
    public string Email { get; set; } = null!;
  
    public string Password { get; set; } = null!;
}
