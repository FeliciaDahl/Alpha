using Data.Contexts;
using Data.Entites;
using Data.Interfaces;
using Domain.Models;

namespace Data.Repositories;

public class StatusRepository(DataContext context) : BaseRepository<StatusEntity, Status>(context), IStatusRepository
{
}
