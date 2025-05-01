using Data.Entites;
using Domain.Models;

namespace Data.Interfaces;

public interface IClientRepository : IBaseRepository<ClientEntity, Client>
{
    Task<List<Client>> GetAllClientsWithProjectStatusAsync();
}
