
using System.Collections.Specialized;

namespace Business.Models;

public class Member
{
    public string Id { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;
    public string Email { get; set; }   = null!;

    public string? PhoneNumber { get; set; }

}
