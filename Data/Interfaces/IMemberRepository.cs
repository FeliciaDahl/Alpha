using Data.Entites;
using System.Linq.Expressions;

namespace Data.Interfaces;

public interface IMemberRepository : IBaseRepository<MemberEntity>
{
    Task<IEnumerable<MemberEntity>> GetAllAsync(Func<IQueryable<MemberEntity>, IQueryable<MemberEntity>>? includeExpression = null);
    Task<MemberEntity?> GetAsync(Expression<Func<MemberEntity, bool>> predicate, Func<IQueryable<MemberEntity>, IQueryable<MemberEntity>>? includeExpression = null);
}