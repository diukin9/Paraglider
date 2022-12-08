using MongoDB.Bson;
using MongoDB.Driver;
using Paraglider.Infrastructure.Common.Abstractions;
using System.Linq.Expressions;

namespace Paraglider.Infrastructure.Common.MongoDB;

public abstract class MongoDataAccess<TEntity> : IMongoDataAccess<TEntity>
    where TEntity : class, IIdentified, new()
{
    private readonly IMongoCollection<TEntity> _collection;

    public MongoDataAccess(IMongoClient client, IMongoDbSettings settings)
    {
        _collection = client
            .GetDatabase(settings.DatabaseName)
            .GetCollection<TEntity>(typeof(TEntity).Name);
    }

    public async Task<IEnumerable<TEntity>> FindAsync(
        Expression<Func<TEntity, bool>>? filter = null,
        Expression<Func<TEntity, object>>? sort = null,
        SortDirection sortDirection = SortDirection.Ascending,
        int skip = 0,
        int limit = 1000)
    {
        filter = filter ??= _ => true;

        var sortDocument = sort is not null
            ? sortDirection == SortDirection.Ascending
                ? Builders<TEntity>.Sort.Ascending(sort)
                : Builders<TEntity>.Sort.Descending(sort)
            : new BsonDocument();

        var entities = await _collection
            .Find(Builders<TEntity>.Filter.Where(filter))
            .Sort(sortDocument)
            .Skip(skip)
            .Limit(limit)
            .ToListAsync();

        return entities;
    }

    public async Task<TEntity?> FindByIdAsync(Guid id)
    {
        return await _collection.Find(x => x.Id == id).SingleOrDefaultAsync();
    }

    public async Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> selector)
    {
        return await _collection.Find(selector).AnyAsync();
    }

    public async Task<long> CountAsync(Expression<Func<TEntity, bool>>? filter = null)
    {
        filter = filter ??= _ => true;

        return await _collection
            .Find(Builders<TEntity>.Filter.Where(filter))
            .CountDocumentsAsync();
    }

    public async Task AddAsync(TEntity value)
    {
        await _collection.InsertOneAsync(value);
    }

    public async Task AddRangeAsync(IEnumerable<TEntity> values)
    {
        await _collection.InsertManyAsync(values);
    }

    public void AddRange(IEnumerable<TEntity> values)
    {
        _collection.InsertMany(values);
    }

    public async Task<bool> UpdateAsync(TEntity value)
    {
        var filter = Builders<TEntity>.Filter.Where(x => x.Id == value.Id);
        var result = await _collection.ReplaceOneAsync(filter, value);
        return result.IsAcknowledged;
    }

    public async Task RemoveAsync(Guid id)
    {
        await _collection.DeleteOneAsync(x => x.Id == id);
    }

    public async Task RemoveAsync(TEntity entity)
    {
        await RemoveAsync(entity.Id);
    }

    public async Task RemoveAsync(Expression<Func<TEntity, bool>> filter)
    {
        await _collection.DeleteManyAsync(filter);
    }
}