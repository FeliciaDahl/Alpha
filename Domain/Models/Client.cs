﻿namespace Domain.Models;

public class Client
{
    public int Id { get; set; }
    public string? Image { get; set; }
    public string ClientName { get; set; } = null!;
    public string ContactPerson { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Location { get; set; } = null!;
    public string? Phone { get; set; }

    public bool HasProjects { get; set; }
}
