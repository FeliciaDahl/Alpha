using Data.Contexts;
using Data.Entites;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Data.Repositories;

public class MemberAdressRepository(DataContext context) : BaseRepository<MemberAdressEntity>(context), IMemberAdressRepository
{

    public override async Task<IEnumerable<MemberAdressEntity>> GetAllAsync(Func<IQueryable<MemberAdressEntity>, IQueryable<MemberAdressEntity>>? includeExpression = null)
    {
        IQueryable<MemberAdressEntity> query = _dbSet;

        if (includeExpression != null)
            query = includeExpression(query);

        query = query.Include(m => m.Member);

        return await query.ToListAsync();

    }

    public override async Task<MemberAdressEntity?> GetAsync(Expression<Func<MemberAdressEntity, bool>> predicate, Func<IQueryable<MemberAdressEntity>, IQueryable<MemberAdressEntity>>? includeExpression = null)
    {
        IQueryable<MemberAdressEntity> query = _dbSet;

        if (includeExpression != null)
            query = includeExpression(query);

        query = query.Include(m => m.Member);

        return await query.FirstOrDefaultAsync(predicate);
    }
}
