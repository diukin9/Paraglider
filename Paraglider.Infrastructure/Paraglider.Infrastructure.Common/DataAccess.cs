using MongoDB.Driver;
using Paraglider.Infrastructure.Common.Abstractions;
using System.Linq.Expressions;

namespace Paraglider.Infrastructure.Common;

public abstract class DataAccess<T> : IDataAccess<T> where T : class, IAggregateRoot, new()
{
    private readonly IMongoCollection<T> _collection;

    public DataAccess(IMongoClient client, IMongoDbSettings settings, string collectionName)
    {
        _collection = client.GetDatabase(settings.DatabaseName).GetCollection<T>(collectionName);
    }

    public async Task AddAsync(T value)
    {
        await _collection.InsertOneAsync(value);
    }

    public async Task AddRangeAsync(IEnumerable<T> values)
    {
        await _collection.InsertManyAsync(values);
    }

    public async Task Delete(Guid id)
    {
        await _collection.DeleteOneAsync(x => x.Id == id);
    }

    public async Task Delete(T value)
    {
        await Delete(value.Id);
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await (await _collection.FindAsync(_ => true)).ToListAsync();
    }

    public async Task<List<T>> GetAsync(Expression<Func<T, bool>> expression)
    {
        return await (await _collection.FindAsync(expression)).ToListAsync();
    }

    public async Task<T> GetByIdAsync(Guid id)
    {
        return await (await _collection.FindAsync(x => x.Id == id)).SingleAsync();
    }

    public async Task<bool> UpdateAsync(T value)
    {
        var filter = Builders<T>.Filter.Eq("Id", value.Id);
        var options = new ReplaceOptions { IsUpsert = true };
        var result = await _collection.ReplaceOneAsync(filter, value, options);
        return result.IsAcknowledged;
    }
}
