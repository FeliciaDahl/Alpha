
using Business.Interfaces;
using Business.Models;
using Data.Interfaces;
using Data.Repositories;
using Domain.Extensions;

namespace Business.Services;

public class ProjectService(IProjectRepository projectRepository) : IProjectService
{
    private readonly IProjectRepository _projectRepository = projectRepository;

    public async Task<ProjectResult> GetAllProjectsAsync()
    {
        var result = await _projectRepository.GetAllAsync();
        return result.MapTo<ProjectResult>();
    }


}
