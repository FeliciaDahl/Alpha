using Data.Entites;
using System.Linq.Expressions;

namespace Data.Interfaces;

public interface IMemberAdressRepository : IBaseRepository<MemberAdressEntity>
{
    Task<IEnumerable<MemberAdressEntity>> GetAllAsync(Func<IQueryable<MemberAdressEntity>, IQueryable<MemberAdressEntity>>? includeExpression = null);
    Task<MemberAdressEntity?> GetAsync(Expression<Func<MemberAdressEntity, bool>> predicate, Func<IQueryable<MemberAdressEntity>, IQueryable<MemberAdressEntity>>? includeExpression = null);
}