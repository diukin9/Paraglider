using MongoDB.Bson;
using MongoDB.Driver;
using Paraglider.Infrastructure.Common.Abstractions;
using System.Linq.Expressions;

namespace Paraglider.Infrastructure.Common;

public abstract class MongoDataAccess<TEntity, TType> : IMongoDataAccess<TEntity> 
    where TEntity : class, IAggregateRoot, new()
{
    private readonly IMongoCollection<TEntity> _collection;

    public MongoDataAccess(IMongoClient client, IMongoDbSettings settings, string collectionName)
    {
        _collection = client.GetDatabase(settings.DatabaseName).GetCollection<TEntity>(collectionName);
    }

    public async Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> expression)
    {
        return await (await _collection.FindAsync(expression)).ToListAsync();
    }

    public async Task<TEntity> FindByIdAsync(Guid id)
    {
        return await (await _collection.FindAsync(x => x.Id == id)).SingleAsync();
    }

    public async Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> selector)
    {
        return await _collection.FindSync(selector).AnyAsync();
    }

    public async Task AddAsync(TEntity value)
    {
        await _collection.InsertOneAsync(value);
    }

    public async Task AddRangeAsync(IEnumerable<TEntity> values)
    {
        await _collection.InsertManyAsync(values);
    }

    public async Task RemoveAsync(Guid id)
    {
        await _collection.DeleteOneAsync(x => x.Id == id);
    }

    public async Task RemoveAsync(TEntity value)
    {
        await RemoveAsync(value.Id);
    }

    public async Task<bool> UpdateAsync(TEntity value)
    {
        var filter = Builders<TEntity>.Filter.Eq("Id", value.Id);
        var options = new ReplaceOptions { IsUpsert = true };
        var result = await _collection.ReplaceOneAsync(filter, value, options);
        return result.IsAcknowledged;
    }
}