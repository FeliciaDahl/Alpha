using Business.Models;
using Domain.Models;

namespace Business.Interfaces
{
    public interface IProjectService
    {
        Task<ServiceResult<IEnumerable<Project>>> GetAllProjectsAsync();
  
    }
}