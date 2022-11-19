using Paraglider.Domain.Entities;

namespace Paraglider.Data.EntityFrameworkCore.Repositories.Interfaces
{
    public interface ICityRepository
    {
        public Task<City?> GetByKeyAsync(string key, bool disableTracking = false);
        public Task<City> GetDefaultCity();
    }
}
