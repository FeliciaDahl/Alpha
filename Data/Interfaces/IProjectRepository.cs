using Data.Entites;
using Domain.Models;
using System.Linq.Expressions;

namespace Data.Interfaces;

public interface IProjectRepository : IBaseRepository<ProjectEntity, Project>
{
    
}