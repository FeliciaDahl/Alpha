﻿namespace Domain.Models;

public class Member
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; }   = null!;
    public string? PhoneNumber { get; set; }

}
