﻿namespace WebApp.Models
{
    public class ClientListItemViewModel
    {
        public string ClientName { get; set; } = null!;
        public string ContactPerson { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Location { get; set; } = null!;
        public string? Phone { get; set; }
    }
}
