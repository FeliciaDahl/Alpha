namespace Domain.Dto;

public class MemberSignUpForm
   
{
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? PhoneNumber { get; set; }
    public string? JobTitle { get; set; }
    public string? Password { get; set; } 

}
