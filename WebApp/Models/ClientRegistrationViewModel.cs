
using Business.Models;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;

public class ClientRegistrationViewModel
{
    [Required(ErrorMessage = "You must enter a name")]
    [DataType(DataType.Text)]
    [Display(Name = "Name", Prompt = "Enter name")]
    public string ClientName { get; set; } = null!;
    [Required(ErrorMessage = "You must enter a name")]
    [DataType(DataType.Text)]
    [Display(Name = "Contact Person", Prompt = "Enter contact person")]
    public string ContactPerson { get; set; } = null!;
    [Required(ErrorMessage = "You must enter a email")]
    [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Invalid email format")]
    [DataType(DataType.EmailAddress)]
    [Display(Name = "Email", Prompt = "Enter email")]
    public string Email { get; set; } = null!;
    [Required(ErrorMessage = "You must enter a location")]
    [DataType(DataType.Text)]
    [Display(Name = "Location", Prompt = "Enter location")]
    public string Location { get; set; } = null!;
    [DataType(DataType.PhoneNumber)]
    [Display(Name = "Phone", Prompt = "Enter phone number")]
    public string? Phone { get; set; }
    public static implicit operator ClientRegistrationForm(ClientRegistrationViewModel model)
    {
        return model == null ? null! : new ClientRegistrationForm
        {
            ClientName = model.ClientName,
            ContactPerson = model.ContactPerson,
            Email = model.Email,
            Location = model.Location,
            Phone = model?.Phone,
        };
    }
}
