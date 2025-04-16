using Data.Models;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IBaseRepository<TEntity, TModel>
    {
        Task<RepositoryResults<bool>> AddAsync(TEntity entity);
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task<RepositoryResults<bool>> DeleteAsync(TEntity entity);
        Task<RepositoryResults<bool>> ExistsAsync(Expression<Func<TEntity, bool>> predicate);
        Task<RepositoryResults<IEnumerable<TModel>>> GetAllAsync(bool orderByDescending = false, Expression<Func<TEntity, object>>? sortBy = null,Expression<Func<TEntity, bool>>? where = null, params Expression<Func<TEntity, object>>[] includes);
        Task<RepositoryResults<IEnumerable<TEntity>>> GetAllEntitiesAsync(bool orderByDescending = false, Expression<Func<TEntity, object>>? sortBy = null, Expression<Func<TEntity, bool>>? where = null, params Expression<Func<TEntity, object>>[] includes);
        Task<RepositoryResults<TModel>> GetAsync(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includes);
        Task<RepositoryResults<TEntity?>> GetEntityAsync(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includes);
        Task RollbackTransactionAsync();
        Task<int> SaveAsync();
        Task<RepositoryResults<bool>> UpdateAsync(TEntity entity);
    }
}