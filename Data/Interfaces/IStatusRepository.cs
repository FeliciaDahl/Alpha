using Data.Entites;
using Domain.Models;

namespace Data.Interfaces;

public interface IStatusRepository : IBaseRepository<StatusEntity, Status>
{
}