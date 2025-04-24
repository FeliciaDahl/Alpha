using System.ComponentModel.DataAnnotations;

namespace Domain.Dto;

public class MemberSignInForm
{
    [Required(ErrorMessage = "You must enter a email")]
    [DataType(DataType.EmailAddress)]
    [Display(Name = "Email", Prompt = "Enter your email")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "You must enter a password")]
    [DataType(DataType.Password)]
    [Display(Name = "Password", Prompt = "Enter your password")]
    public string Password { get; set; } = null!;
}
