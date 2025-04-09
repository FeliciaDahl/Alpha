
using Data.Contexts;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;
using Data.Interfaces;
using Data.Models;
using System.Globalization;
using Domain.Extensions;

namespace Data.Repositories;



public abstract class BaseRepository<TEntity, TModel>(DataContext context) : IBaseRepository<TEntity, TModel> where TEntity : class
{
    protected readonly DataContext _context = context;
    protected readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

    private IDbContextTransaction _transaction = null!;

    #region Transaction Management

    public virtual async Task BeginTransactionAsync()
    {
        _transaction ??= await _context.Database.BeginTransactionAsync();

    }

    public virtual async Task CommitTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.CommitAsync();
            await _transaction.DisposeAsync();
            _transaction = null!;
        }
    }

    public virtual async Task RollbackTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
            _transaction = null!;
        }

    }

    #endregion

    public virtual async Task<RepositoryResults<bool>> AddAsync(TEntity entity)
    {
        if (entity == null)
        {
            return RepositoryResults<bool>.Failed(400, "Entity cant be null");
        }
        try
        {
            await _dbSet.AddAsync(entity);
            return RepositoryResults<bool>.Success(true);
        }
        catch (Exception ex)
        {
            return RepositoryResults<bool>.Failed(500, $"An error occurred: {ex.Message}");
        }
    }

    public virtual async Task<RepositoryResults<IEnumerable<TModel>>> GetAllAsync(bool orderByDescending = false, Expression<Func<TEntity, object>>? sortBy = null, Expression<Func<TEntity, bool>>? where = null, params Expression<Func<TEntity, object>>[] includes)
    {

        try
        {
            IQueryable<TEntity> query = _dbSet;

            if (where != null)
                query = query.Where(where);

            if (includes != null && includes.Length != 0)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            if (sortBy != null)
            {
                if (orderByDescending)
                    query = query.OrderByDescending(sortBy);
                else
                    query = query.OrderBy(sortBy);
            }

            var entities = await query.ToListAsync();

            var result = entities.Select(e => e.MapTo<TModel>());
            return RepositoryResults<IEnumerable<TModel>>.Success(result);

        }
        catch (Exception ex)
        {
            return RepositoryResults<IEnumerable<TModel>>.Failed(500, $"An error occurred: {ex.Message}");

        }
    }

    public virtual async Task<RepositoryResults<TModel?>> GetAsync( Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includes)
    {

        try
        {
            IQueryable<TEntity> query = _dbSet;

            if (includes != null && includes.Length != 0)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }


            var entity = await query.FirstOrDefaultAsync(where);


            if (entity == null)
                return RepositoryResults<TModel?>.Failed(404, $"{nameof(TEntity)} not found");

            var result = entity.MapTo<TModel>();

            return RepositoryResults<TModel?>.Success(result);
        }
        catch (Exception ex)
        {
            return RepositoryResults<TModel?>.Failed(500, $"An error occurred: {ex.Message}");

        }

    }

    public virtual async Task<RepositoryResults<TEntity?>> GetEntityAsync(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includes)
    {

        try
        {
            IQueryable<TEntity> query = _dbSet;

            if (includes != null && includes.Length != 0)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            var entity = await query.FirstOrDefaultAsync(where);


            if (entity == null)
                return RepositoryResults<TEntity?>.Failed(404, $"{nameof(TEntity)} not found");

            return RepositoryResults<TEntity?>.Success(entity);
        }
        catch (Exception ex)
        {
            return RepositoryResults<TEntity?>.Failed(500, $"An error occurred: {ex.Message}");

        }

    }



    //Be om hjälp med denna! Den säger att det finns två med samma ID.. Vilket det ska finnas då det är samma som ska uppdateras...
    public virtual async Task<RepositoryResults<bool>> UpdateAsync(TEntity entity)
    {
        try
        {
            if (entity == null)
                return RepositoryResults<bool>.Failed(400, "Entity cant be null");

           
            _dbSet.Update(entity);
           
    
            return RepositoryResults<bool>.Success(true);

        }
        catch (Exception ex)
        {
            return RepositoryResults<bool>.Failed(500, $"An error occurred: {ex.Message}");

        }
    }

    public virtual async Task<RepositoryResults<bool>> DeleteAsync(TEntity entity)
    {
        try
        {
            if (entity == null)
                return RepositoryResults<bool>.Failed(400, "Entity cant be null");


            _dbSet.Remove(entity);
            return RepositoryResults<bool>.Success(true);
        }
        catch (Exception ex)
        {
            return RepositoryResults<bool>.Failed(500, $"An error occurred: {ex.Message}");
        }

    }

    public virtual async Task<RepositoryResults<bool>> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
    {
        try
        {
            var exists = await _dbSet.AnyAsync(predicate);

            if (exists)
                return RepositoryResults<bool>.Failed(404, $"{nameof(TEntity)} not found");

            return RepositoryResults<bool>.Success(exists);
        }
        catch (Exception ex)
        {
            return RepositoryResults<bool>.Failed(500, $"An error occurred: {ex.Message}");
        }
    }

    public virtual async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }

}

