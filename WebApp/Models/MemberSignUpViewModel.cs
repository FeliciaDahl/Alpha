using Business.Models;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;

public class MemberSignUpViewModel
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

    [Required(ErrorMessage = "You must enter a password")]
    [DataType(DataType.Password)]
    [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[^A-Za-z\d]).{8,}$", ErrorMessage = "Password not vaild")]
    [Display(Name = "Password", Prompt = "Enter your password")]
    public string Password { get; set; } = null!;

    [Required(ErrorMessage = "You must confrim your password")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Passwords do not match")]
    [Display(Name = "Confirm Password", Prompt = "Confirm your password")]
    public string ConfirmPassword { get; set; } = null!;


    [Display(Name = "Terms and Conditions", Prompt = "I accept terms and conditions")]
    [Range(typeof(bool), "true", "true", ErrorMessage = "You must accept terms and conditions")]
    public bool TermsAndConditions { get; set; }

    public static implicit operator MemberSignUpForm(MemberSignUpViewModel model)
    {
        return model == null ? null! : new MemberSignUpForm
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            Password = model.Password
        };
    }
}
