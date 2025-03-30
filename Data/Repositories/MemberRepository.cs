using Data.Contexts;
using Data.Entites;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Data.Repositories;

public class MemberRepository(DataContext context) : BaseRepository<MemberEntity>(context), IMemberRepository
{

    public override async Task<IEnumerable<MemberEntity>> GetAllAsync(Func<IQueryable<MemberEntity>, IQueryable<MemberEntity>>? includeExpression = null)
    {
        IQueryable<MemberEntity> query = _dbSet;

        if (includeExpression != null)
            query = includeExpression(query);

        query = query.Include(m => m.MemberAdress);

        return await query.ToListAsync();

    }

    public override async Task<MemberEntity?> GetAsync(Expression<Func<MemberEntity, bool>> predicate, Func<IQueryable<MemberEntity>, IQueryable<MemberEntity>>? includeExpression = null)
    {
        IQueryable<MemberEntity> query = _dbSet;

        if (includeExpression != null)
            query = includeExpression(query);

        query = query.Include(m => m.MemberAdress);

        return await query.FirstOrDefaultAsync(predicate);
    }

}


