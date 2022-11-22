using Microsoft.EntityFrameworkCore;
using Paraglider.Data.EntityFrameworkCore.Repositories.Interfaces;
using Paraglider.Domain.RDB.Entities;
using Paraglider.Infrastructure.Common;
using Paraglider.Infrastructure.Common.Abstractions;
using System.Linq.Expressions;

namespace Paraglider.Data.EntityFrameworkCore.Repositories
{
    public class CityRepository : RDbRepository<City>, ICityRepository
    {
        private readonly ApplicationDbContext _context;

        public CityRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<IEnumerable<City>> FindAsync(Expression<Func<City, bool>> selector)
        {
            return await _context.Cities.Where(selector).ToListAsync();
        }

        public override async Task<City?> FindByIdAsync(Guid id)
        {
            return await _context.Cities.Where(x => x.Id == id).SingleOrDefaultAsync();
        }

        public async Task<City?> GetByKeyAsync(string key)
        {
            var city = await _context.Cities
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
