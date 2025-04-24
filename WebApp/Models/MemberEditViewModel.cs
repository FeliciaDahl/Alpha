using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;

public class MemberEditViewModel
{

    public string Id { get; set; } = null!;

    [Display(Name = "Member Image", Prompt = "Enter image")]
    [DataType(DataType.Upload)]
    public IFormFile? MemberImage { get; set; }

    public string? MemberImagePath { get; set; } = null!;

    [Required(ErrorMessage = "You must enter a firstname")]
    [DataType(DataType.Text)]
    [Display(Name = "First Name", Prompt = "Enter a first name")]
    public string FirstName { get; set; } = null!;

    [Required(ErrorMessage = "You must enter a last name")]
    [DataType(DataType.Text)]
    [Display(Name = "Last Name", Prompt = "Enter a last name")]
    public string LastName { get; set; } = null!;

    [Required(ErrorMessage = "You must enter a jobtitle")]
    [DataType(DataType.Text)]
    [Display(Name = "Job Title", Prompt = "Select Title")]
    public string JobTitle { get; set; } = null!;

    public string? Role { get; set; } = null!;

    [Required(ErrorMessage = "You must enter a email")]
    [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Invalid email format")]
    [DataType(DataType.EmailAddress)]
    [Display(Name = "Email", Prompt = "Enter a email")]
    public string Email { get; set; } = null!;

    [DataType(DataType.PhoneNumber)]
    [Display(Name = "PhoneNumber", Prompt = "Enter phone number")]
    public string? PhoneNumber { get; set; }

    public List<SelectListItem> Roles { get; set; } = new();
}
