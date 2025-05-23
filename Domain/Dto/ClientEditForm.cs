﻿
using Microsoft.AspNetCore.Http;

namespace Domain.Dto;

public class ClientEditForm
{
    public int Id { get; set; }
    public string? ImagePath { get; set; }
    public string ClientName { get; set; } = null!;
    public string ContactPerson { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Location { get; set; } = null!;
    public string? Phone { get; set; }
}
