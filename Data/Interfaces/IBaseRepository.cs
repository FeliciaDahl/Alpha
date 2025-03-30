using System.Linq.Expressions;

namespace Data.Interfaces
{
    public interface IBaseRepository<TEntity>
    {
        void Add(TEntity entity);
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        void Delete(TEntity entity);
        Task<IEnumerable<TEntity>> GetAllAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>>? includeExpression = null);
        Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IQueryable<TEntity>>? includeExpression = null);
        Task RollbackTransactionAsync();
        Task<int> SaveAsync();
        void Update(TEntity entity);
    }
}