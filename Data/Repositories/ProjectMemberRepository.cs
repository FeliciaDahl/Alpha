using Data.Contexts;
using Data.Entites;
using Data.Interfaces;
using Domain.Models;

namespace Data.Repositories;

public class ProjectMemberRepository(DataContext context) : BaseRepository<ProjectMemberEntity, ProjectMember>(context), IProjectMemberRepository
{
}
