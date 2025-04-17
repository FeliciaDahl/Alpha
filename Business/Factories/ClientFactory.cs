
using Data.Entites;
using Domain.Dto;

namespace Business.Factories;

class ClientFactory
{
    public static ClientEntity Create(ClientRegistrationForm form)
    {
        return new ClientEntity
        {  
            Image = form.ClientImagePath,
            ClientName = form.ClientName,
            ContactPerson = form.ContactPerson,
            Email = form.Email,
            Location = form.Location,
            Phone = form.Phone,
        };

    }
}
