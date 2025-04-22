
using Data.Entites;
using Domain.Dto;
using Domain.Models;


namespace Business.Factories;

public class ProjectFactory
{
    public static ProjectEntity Create(ProjectRegistrationForm form)
    {
        return new ProjectEntity
        {
          
            Title = form.Title,
            Image = form.ProjectImagePath,
            Description = form.Description,
            StartDate = form.StartDate,
            EndDate = form.EndDate,
            Budget = form.Budget,
            ClientId = form.ClientId,
            StatusId = form.StatusId,
    
        };
        
    }


    public static Project ToModel(ProjectEntity entity)
    {
        return new Project
        {
            Id = entity.Id,
            Title = entity.Title,
            Image = entity.Image,
            StatusId = entity.StatusId,
            Description = entity.Description,
            StartDate = entity.StartDate,
            EndDate = entity.EndDate,
            Budget = entity.Budget,

            Client = new Client
            {
                Id = entity.Client.Id,
                ClientName = entity.Client.ClientName
            },
            Status = new Status
            {
                Id = entity.Status.Id,
                StatusName = entity.Status.StatusName
            },

            ProjectMembers = entity.ProjectMembers.Select(pm => new Member
            {
                Id = pm.Member.Id,
                FirstName = pm.Member.FirstName,
                LastName = pm.Member.LastName,
            }).ToList()
        };
    }
}
