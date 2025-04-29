
using Business.Factories;
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
           return ServiceResult<Project>.Failed(400, "All required filed has to be filled in.");
      
        await _projectRepository.BeginTransactionAsync();

        try

        {  
            var statusId = await _statusService.GetStatusByIdAsync(1);
            var status = statusId.Result;

            form.StatusId = status!.Id;

            var projectEntity = ProjectFactory.Create(form);

            await _projectRepository.AddAsync(projectEntity);
            await _projectRepository.SaveAsync();

            await _projectRepository.CommitTransactionAsync();

            var project = projectEntity.MapTo<Project>();
            return ServiceResult<Project>.Success(project);
        }
        catch (Exception e)
        {
            await _projectRepository.RollbackTransactionAsync();
            return ServiceResult<Project>.Failed(500, e.Message);
        }

    }


    public async Task<ServiceResult<IEnumerable<Project>>> GetAllProjectsAsync()
    {
        var result = await _projectRepository.GetAllEntitiesAsync(orderByDescending: true, sortBy: s => s.Created, where: null,
          x => x.ProjectMembers,
          x => x.Client,
          x => x.Status
          );

        var projects = result.Result?.Select(ProjectFactory.ToModel);

        return ServiceResult<IEnumerable<Project>>.Success(projects!);
    }

    public async Task<ServiceResult<Project>> GetProjectAsync(int id)
    {
        var entity = await _projectRepository.GetEntityAsync( where: x => x.Id == id,
          x => x.ProjectMembers,
          x => x.Client,
          x => x.Status
          );

        if (entity.Succeeded && entity.Result != null)
        {
            var result = ProjectFactory.ToModel(entity.Result!);
            return ServiceResult<Project>.Success(result);
        }
        else
        {
            return ServiceResult<Project>.Failed(500, "Project not found");
        }

    }

    public async Task<ServiceResult<bool>> EditProjectAsync(int id, ProjectEditForm form)
    {

        var existingProjectResult = await _projectRepository.GetEntityAsync(c => c.Id == id);

        if (!existingProjectResult.Succeeded || existingProjectResult.Result == null)
            return ServiceResult<bool>.Failed(404, "Project not found");

        var projectEntity = existingProjectResult.Result;

        projectEntity.Image = string.IsNullOrWhiteSpace(form.ProjectImagePath) ? projectEntity.Image : form.ProjectImagePath;
        projectEntity.Title = string.IsNullOrWhiteSpace(form.Title) ? projectEntity.Title : form.Title;
        projectEntity.Description = string.IsNullOrWhiteSpace(form.Description) ? projectEntity.Description : form.Description;
        projectEntity.StartDate = form.StartDate;
        projectEntity.EndDate = form.EndDate ?? projectEntity.EndDate;
        projectEntity.Budget = form.Budget ?? projectEntity.Budget;
        projectEntity.ClientId = form.ClientId != 0 ? form.ClientId : projectEntity.ClientId;
        projectEntity.StatusId = form.StatusId != 0 ? form.StatusId : projectEntity.StatusId;
        projectEntity.ProjectMembers = form.ProjectMemberId != null ? new List<ProjectMemberEntity> { new ProjectMemberEntity { MemberId = form.ProjectMemberId } } : projectEntity.ProjectMembers;


        await _projectRepository.BeginTransactionAsync();

            try
            {
               var result = await _projectRepository.UpdateAsync(projectEntity);

                await _projectRepository.SaveAsync();
                await _projectRepository.CommitTransactionAsync();

                return ServiceResult<bool>.Success(true);

            }
            catch (Exception e)
            {
                await _projectRepository.RollbackTransactionAsync();
                return ServiceResult<bool>.Failed(500, e.Message);

            }
    }


    public async Task<ServiceResult<bool>> DeleteProjectAsync(int id)
    {
        var existingProjectResult = await _projectRepository.GetEntityAsync(c => c.Id == id);

        if (!existingProjectResult.Succeeded || existingProjectResult.Result == null)
            return ServiceResult<bool>.Failed(404, "Project not found");

        var existingProject = existingProjectResult.Result;

        await _projectRepository.BeginTransactionAsync();

        try
        {
            var result = await _projectRepository.DeleteAsync(existingProject);
    
            await _projectRepository.SaveAsync();
            await _projectRepository.CommitTransactionAsync();
            return ServiceResult<bool>.Success(true);
        }
        catch (Exception e)
        {
            await _projectRepository.RollbackTransactionAsync();
            return ServiceResult<bool>.Failed(500, e.Message);
        }
    }

}
