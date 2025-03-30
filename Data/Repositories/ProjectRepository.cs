
using Data.Contexts;
using Data.Entites;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Data.Repositories;

public class ProjectRepository(DataContext context) : BaseRepository<ProjectEntity>(context), IProjectRepository
{
    public override async Task<IEnumerable<ProjectEntity>> GetAllAsync(Func<IQueryable<ProjectEntity>, IQueryable<ProjectEntity>>? includeExpression = null)
    {
        IQueryable<ProjectEntity> query = _dbSet;

        if (includeExpression != null)
            query = includeExpression(query);

        query = query
            .Include(p => p.Client)
            .Include(p => p.ProjectMembers)
            .ThenInclude(pm => pm.Member);

        return await query.ToListAsync();

    }

    public override async Task<ProjectEntity?> GetAsync(Expression<Func<ProjectEntity, bool>> predicate, Func<IQueryable<ProjectEntity>, IQueryable<ProjectEntity>>? includeExpression = null)
    {
        IQueryable<ProjectEntity> query = _dbSet;

        if (includeExpression != null)
            query = includeExpression(query);

        if (predicate != null)
            query = query.Where(predicate);


        query = query
            .Include(p => p.Client)
            .Include(p => p.ProjectMembers)
            .ThenInclude(pm => pm.Member);

        return await query.FirstOrDefaultAsync();
    }
}
