using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Paraglider.Infrastructure.Common.Abstractions;

public abstract class Repository<TEntity> : IRepository<TEntity>  where TEntity : class, IAggregateRoot, new()
{
    private readonly DbContext _context;

    public Repository(DbContext context)
    {
        _context = context;
    }

    public abstract Task<TEntity?> FindByIdAsync(Guid id);

    public abstract Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> selector);

    public async Task AddAsync(TEntity entity)
    {
        await _context.Set<TEntity>().AddAsync(entity);
    }

    public async Task AddRangeAsync(IEnumerable<TEntity> entities)
    {
        await _context.Set<TEntity>().AddRangeAsync(entities);
    }

    public async Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> selector)
    {
        return await _context.Set<TEntity>().AnyAsync(selector);
    }

    public async Task RemoveAsync(TEntity entity)
    {
        await Task.FromResult(_context.Set<TEntity>().Remove(entity));
    }

    public async Task RemoveAsync(Guid id)
    {
        var entity = await _context.Set<TEntity>().SingleAsync(x => x.Id == id);
        await RemoveAsync(entity);
    }

    public async Task RemoveRangeAsync(IEnumerable<TEntity> entities)
    {
        _context.Set<TEntity>().RemoveRange(entities);
        await Task.FromResult(default(TEntity));
    }

    public async Task UpdateAsync(TEntity entity)
    {
        await Task.FromResult(_context.Set<TEntity>().Update(entity));
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
