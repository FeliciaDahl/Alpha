
using System.ComponentModel.DataAnnotations;

namespace Business.DTO;

public class UserSignUpForm
   
{
    [Required (ErrorMessage ="You must enter a name")]
    [DataType(DataType.Text)]
    [Display(Name = "First Name", Prompt ="Enter your first name")]
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
    
    [Required(ErrorMessage = "You must confrim your passoword")]
    [DataType(DataType.Password)]
    [Display(Name = "Confirm Password", Prompt = "Confirm your password")]
    public string ConfirmPassword { get; set; } = null!;

    [Required(ErrorMessage = "You must accept terms")]
    [Display(Name = "Accept Terms", Prompt = "I accept terms and conditions")]
    public bool Terms { get; set; }
}
