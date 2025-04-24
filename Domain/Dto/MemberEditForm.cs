
namespace Domain.Dto;

public class MemberEditForm
{
    public string Id { get; set; } = null!;
    public string? MemberImagePath { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? JobTitle { get; set; }
    public string Email { get; set; } = null!;
    public string? PhoneNumber { get; set; }
    public string? Role { get; set; }
    public string? Street { get; set; } = null!;

    public string? City { get; set; } = null!;

    public string? PostalCode { get; set; } = null!;

    public string? Country { get; set; } = null!;

}
