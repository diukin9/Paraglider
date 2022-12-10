using Paraglider.Domain.RDB.Entities;
using Paraglider.Domain.RDB.Enums;
using Paraglider.Infrastructure.Common.Abstractions;

namespace Paraglider.Data.EntityFrameworkCore.Repositories.Interfaces;

public interface ICityRepository : IRepository<City>
{
    public Task<IEnumerable<string>> GetKeysAsync(Source source);
    public Task<City?> FindByNameAsync(string key);
    public Task<City?> FindByKeyAsync(Source source, string key);
    public Task<City> GetDefaultCity();
}
