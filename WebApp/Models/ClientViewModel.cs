using Domain.Models;

namespace WebApp.Models;

public class ClientViewModel
{
    public List<Client> Clients { get; set; } = new();
    public ClientRegistrationViewModel ClientRegistration { get; set; } = new();
    public ClientEditViewModel ClientEdit { get; set; } = new();


}
