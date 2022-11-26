using Paraglider.Domain.RDB.Entities;
using Paraglider.Infrastructure.Common.Abstractions;

namespace Paraglider.Data.EntityFrameworkCore.Repositories.Interfaces;

public interface ICityRepository : IRepository<City>
{
    public Task<City?> FindByNameAsync(string key);
    public Task<City> GetDefaultCity();

    public Task<City?> FindByIdAsync(Guid id);
}
