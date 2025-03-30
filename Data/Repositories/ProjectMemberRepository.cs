using Data.Contexts;
using Data.Entites;
using Data.Interfaces;

namespace Data.Repositories;

public class ProjectMemberRepository(DataContext context) : BaseRepository<ProjectMemberEntity>(context), IProjectMemberRepository
{
}
