using Microsoft.EntityFrameworkCore;
using Paraglider.Data.EntityFrameworkCore.Repositories.Interfaces;
using Paraglider.Domain.RDB.Entities;
using Paraglider.Infrastructure.Common;

namespace Paraglider.Data.EntityFrameworkCore.Repositories
{
    public class CityRepository : Repository<City>, ICityRepository
    {
        public CityRepository(ApplicationDbContext context) : base(context)
        {

        }

        public async Task<City?> GetByKeyAsync(string key, bool disableTracking = false)
        {
            var city = await this.GetAll(disableTracking)
                .Where(u => u.Key == key)
                .SingleOrDefaultAsync();
            return city;
        }

        public async Task<City> GetDefaultCity()
        {
            return (await GetByKeyAsync(AppData.DefaultCityKey))!;
        }
    }
}
