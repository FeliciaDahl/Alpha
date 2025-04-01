
using static System.Net.Mime.MediaTypeNames;

namespace Business.Models;

public class ClientRegistrationForm
{
    public string ClientName { get; set; } = null!;
    public string ContactPerson { get; set; } = null!;
    public string Email { get; set; } = null!;
   
    public string Location { get; set; } = null!;
    public string? Phone { get; set; }

}
    