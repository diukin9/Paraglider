using Paraglider.Domain.Entities;

namespace Paraglider.Data.Repositories.Interfaces
{
    public interface ICityRepository
    {
        public Task<City?> GetByKeyAsync(string? key);
        public Task<City> GetDefaultCity();
    }
}
