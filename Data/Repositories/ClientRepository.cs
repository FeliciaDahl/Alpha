using Data.Contexts;
using Data.Entites;
using Data.Interfaces;

namespace Data.Repositories;

public class ClientRepository(DataContext context) : BaseRepository<ClientEntity>(context), IClientRepository
{
}
