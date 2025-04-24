namespace Domain.Dto;

public class MemberSignUpForm
   
{
    public string? MemberImagePath { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? JobTitle { get; set; }
    public string? Role { get; set; } 
    public string? PhoneNumber { get; set; }
    
    public string? Password { get; set; } 

}
