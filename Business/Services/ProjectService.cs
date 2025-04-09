
using Business.Interfaces;
using Business.Models;
using Data.Entites;
using Data.Interfaces;
using Data.Repositories;
using Domain.Dto;
using Domain.Extensions;
using Domain.Models;

namespace Business.Services;

public class ProjectService(IProjectRepository projectRepository, IStatusService statusService) : IProjectService
{
    private readonly IProjectRepository _projectRepository = projectRepository;
    private readonly IStatusService _statusService = statusService;


    public async Task<ServiceResult<Project>> CreateProjectAsync(ProjectRegistrationForm form)
    {

        if (form == null)
           return ServiceResult<Project>.Failed(400, "Form can not be empty");
      
        await _projectRepository.BeginTransactionAsync();

        try
        {
            var projectEntity = form.MapTo<ProjectEntity>();
            var statusId = await _statusService.GetStatusByIdAsync(1);
            var status = statusId.Result;

            projectEntity.StatusId = status!.Id;

            await _projectRepository.AddAsync(projectEntity);
            await _projectRepository.SaveAsync();

            await _projectRepository.CommitTransactionAsync();

            var client = projectEntity.MapTo<Project>();
            return ServiceResult<Project>.Success(client);
        }
        catch (Exception e)
        {
            await _projectRepository.RollbackTransactionAsync();
            return ServiceResult<Project>.Failed(500, e.Message);
        }

    }


    public async Task<ServiceResult<IEnumerable<Project>>> GetAllProjectsAsync()
    {
        var result = await _projectRepository.GetAllAsync(orderByDescending: true, sortBy: s => s.Created, where: null,
          x => x.ProjectMembers,
          x => x.Client,
          x => x.Status
          );

        var projects = result.Result?.Select(p => p.MapTo<Project>());

        return ServiceResult<IEnumerable<Project>>.Success(projects!);
    }

    public async Task<ServiceResult<Project>> GetProjectAsync(int id)
    {
        var result = await _projectRepository.GetAsync( where: x => x.Id == id,
          x => x.ProjectMembers,
          x => x.Client,
          x => x.Status
          );

        return result.Succeeded
            ? ServiceResult<Project>.Success(result.Result!.MapTo<Project>())
            : ServiceResult<Project>.Failed(result.StatusCode, "Project not found");

    }


}
