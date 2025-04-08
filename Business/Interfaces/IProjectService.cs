using Business.Models;

namespace Business.Interfaces
{
    public interface IProjectService
    {
        Task<ProjectResult> GetAllProjectsAsync();
    }
}