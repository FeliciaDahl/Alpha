using Business.Models;
using Domain.Dto;
using Domain.Models;

namespace Business.Interfaces;

public interface IProjectService
{
    Task<ServiceResult<Project>> CreateProjectAsync(ProjectRegistrationForm form);
    Task<ServiceResult<bool>> EditProjectAsync(int id, ProjectEditForm form);
    Task<ServiceResult<IEnumerable<Project>>> GetAllProjectsAsync();
    Task<ServiceResult<Project>> GetProjectAsync(int id);
}