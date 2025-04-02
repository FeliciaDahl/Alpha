using System.ComponentModel.DataAnnotations;

namespace Domain.Dto;

public class MemberSignInForm
{
    public string Email { get; set; } = null!;
  
    public string Password { get; set; } = null!;
}
