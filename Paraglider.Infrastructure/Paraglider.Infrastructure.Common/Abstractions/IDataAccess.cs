using System.Linq.Expressions;

namespace Paraglider.Infrastructure.Common.Abstractions;

public interface IDataAccess<T> where T : class, IAggregateRoot, new()
{
    public Task<T> GetByIdAsync(Guid id);

    public Task<List<T>> GetAllAsync();

    public Task<List<T>> GetAsync(Expression<Func<T, bool>> expression);

    public Task AddAsync(T value);

    public Task AddRangeAsync(IEnumerable<T> values);

    public Task<bool> UpdateAsync(T value);

    public Task Delete(Guid id);

    public Task Delete(T value);
}
