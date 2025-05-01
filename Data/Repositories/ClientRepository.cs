using Data.Contexts;
using Data.Entites;
using Data.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class ClientRepository(DataContext context) : BaseRepository<ClientEntity, Client>(context), IClientRepository
{
    public async Task<List<Client>> GetAllClientsWithProjectStatusAsync()
    {
        return await _context.Clients
            .Select(c => new Client
            {
                Id = c.Id,
                ClientName = c.ClientName,
                ContactPerson = c.ContactPerson,
                Email = c.Email,
                Location = c.Location,
                Phone = c.Phone,
                Image = c.Image,
                HasProjects = c.Projects.Any()
            })
            .ToListAsync();
    }

}
