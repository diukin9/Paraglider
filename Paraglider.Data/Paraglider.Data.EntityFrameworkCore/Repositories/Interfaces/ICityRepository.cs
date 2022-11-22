using Paraglider.Domain.RDB.Entities;
using Paraglider.Infrastructure.Common.Abstractions;

namespace Paraglider.Data.EntityFrameworkCore.Repositories.Interfaces
{
    public interface ICityRepository : IRepository<City>, IShouldSaveChanges
    {
        public Task<City?> GetByKeyAsync(string key);
        public Task<City> GetDefaultCity();
    }
}
