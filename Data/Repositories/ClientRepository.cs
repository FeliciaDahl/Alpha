using Data.Contexts;
using Data.Entites;
using Data.Interfaces;
using Domain.Models;

namespace Data.Repositories;

public class ClientRepository(DataContext context) : BaseRepository<ClientEntity, Client>(context), IClientRepository
{
}
