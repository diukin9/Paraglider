using MapsterMapper;
using MongoDB.Bson;
using System.Linq.Expressions;

namespace Paraglider.Infrastructure.Common.Abstractions;

public abstract class NoSqlRepository<TEntity> : IRepository<TEntity>
    where TEntity : class, IAggregateRoot, new()
{
    private readonly IMongoDataAccess<TEntity> _dataAccess;

    public NoSqlRepository(IMongoDataAccess<TEntity> dataAccess)
    {
        _dataAccess = dataAccess;
    }

    public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> selector)
    {
        return await _dataAccess.FindAsync(selector);
    }

    public async Task<TEntity?> FindByIdAsync(Guid id)
    {
        return await _dataAccess.FindByIdAsync(id);
    }

    public async Task AddAsync(TEntity entity)
    {
        await _dataAccess.AddAsync(entity);
    }

    public async Task AddRangeAsync(IEnumerable<TEntity> entities)
    {
        await _dataAccess.AddRangeAsync(entities);
    }

    public async Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> selector)
    {
        return await _dataAccess.IsExistAsync(selector);
    }

    public async Task RemoveAsync(Guid id)
    {
        await _dataAccess.RemoveAsync(id);
    }

    public async Task RemoveAsync(TEntity entity)
    {
        await _dataAccess.RemoveAsync(entity);
    }

    public async Task RemoveRangeAsync(IEnumerable<TEntity> entities)
    {
        foreach (var entity in entities)
        {
            await _dataAccess.RemoveAsync(entity);
        }
    }

    public async Task UpdateAsync(TEntity entity)
    {
        await _dataAccess.UpdateAsync(entity);
    }
}

public abstract class NoSqlRepository<TEntity, TCommon> : IRepository<TEntity>
    where TCommon : class, IAggregateRoot, new()
    where TEntity : IDerived<TCommon>, new()
{
    private readonly IMongoDataAccess<TCommon> _dataAccess;
    private readonly IMapper _mapper;

    public NoSqlRepository(IMongoDataAccess<TCommon> dataAccess, IMapper mapper)
    {
        _dataAccess = dataAccess;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> selector)
    {
        var param = Expression.Parameter(typeof(TCommon));
        var body = new Visitor<TCommon>(param).Visit(selector.Body);
        var lambda = Expression.Lambda<Func<TCommon, bool>>(body, param);
        //var union = Expression.AndAlso(lambda.Body, _filterByType.Body);
        //var expression = Expression.Lambda(union, lambda.Parameters[0]);
        var collection = await _dataAccess.FindAsync(lambda);
        return collection.Select(x => _mapper.Map<TEntity>(x)).ToList();
    }

    public async Task<TEntity?> FindByIdAsync(Guid id)
    {
        return _mapper.Map<TEntity>(await _dataAccess.FindByIdAsync(id));
    }

    public async Task AddAsync(TEntity entity)
    {
        await _dataAccess.AddAsync(_mapper.Map<TCommon>(entity));
    }

    public async Task AddRangeAsync(IEnumerable<TEntity> entities)
    {
        var collection = entities.Select(x => _mapper.Map<TCommon>(x)).ToList();
        await _dataAccess.AddRangeAsync(collection);
    }

    public async Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> selector)
    {
        var expression = _mapper.Map<Expression<Func<TCommon, bool>>>(selector);
        return await _dataAccess.IsExistAsync(expression);
    }

    public async Task RemoveAsync(Guid id)
    {
        await _dataAccess.RemoveAsync(id);
    }

    public async Task RemoveAsync(TEntity entity)
    {
        await _dataAccess.RemoveAsync(_mapper.Map<TCommon>(entity));
    }

    public async Task RemoveRangeAsync(IEnumerable<TEntity> entities)
    {
        foreach (var entity in entities)
        {
            await _dataAccess.RemoveAsync(_mapper.Map<TCommon>(entity));
        }
    }

    public async Task UpdateAsync(TEntity entity)
    {
        await _dataAccess.UpdateAsync(_mapper.Map<TCommon>(entity));
    }
}