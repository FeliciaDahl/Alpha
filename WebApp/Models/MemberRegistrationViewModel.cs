using Business.Models;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;

public class MemberRegistrationViewModel
{

    [Required(ErrorMessage = "You must enter a name")]
    [DataType(DataType.Text)]
    [Display(Name = "First Name", Prompt = "Enter your first name")]
    public string FirstName { get; set; } = null!;

    [Required(ErrorMessage = "You must enter a name")]
    [DataType(DataType.Text)]
    [Display(Name = "Last Name", Prompt = "Enter your last name")]
    public string LastName { get; set; } = null!;

    [Required(ErrorMessage = "You must enter a email")]
    [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Invalid email format")]
    [DataType(DataType.EmailAddress)]
    [Display(Name = "Email", Prompt = "Enter your email")]
    public string Email { get; set; } = null!;

    [DataType(DataType.PhoneNumber)]
    [Display(Name = "Phone", Prompt = "Enter your phone number")]
    public string? Phone { get; set; }

    public static implicit operator MemberSignUpForm(MemberRegistrationViewModel model)
    {
        return model == null ? null! : new MemberSignUpForm
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            Phone = model?.Phone,

        };
    }

}
