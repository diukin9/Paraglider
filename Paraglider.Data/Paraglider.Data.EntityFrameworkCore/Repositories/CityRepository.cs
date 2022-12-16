using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Paraglider.Data.EntityFrameworkCore.Repositories.Interfaces;
using Paraglider.Domain.Common.Enums;
using Paraglider.Domain.RDB.Entities;
using Paraglider.Infrastructure.Common;
using Paraglider.Infrastructure.Common.Repository;

namespace Paraglider.Data.EntityFrameworkCore.Repositories;

public class CityRepository : Repository<City>, ICityRepository
{
    private readonly ApplicationDbContext _context;

    public CityRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<City>> FindAsync(Expression<Func<City, bool>> selector)
    {
        return await _context.Cities.IncludeAll()
            .Where(selector)
            .ToListAsync();
    }

    public override async Task<City?> FindByIdAsync(Guid id)
    {
        return await _context.Cities.IncludeAll()
            .Where(x => x.Id == id)
            .SingleOrDefaultAsync();
    }

    public async Task<City?> FindByKeyAsync(Source source, string key)
    {
        return await _context.Cities.IncludeAll()
            .Where(x => x.Keys.Any(y => y.Source == source && y.Key == key))
            .SingleOrDefaultAsync();
    }

    public async Task<City?> FindByNameAsync(string name)
    {
        var city = await _context.Cities.IncludeAll()
            .Where(u => u.Name == name)
            .SingleOrDefaultAsync();
        return city;
    }

    public async Task<City> GetDefaultCity()
    {
        return (await FindByNameAsync(AppData.DefaultCityName))!;
    }

    public async Task<IEnumerable<string>> GetKeysAsync(Source source)
    {
        return await _context.Cities
            .SelectMany(x => x.Keys)
            .Where(x => x.Source == source)
            .Select(x => x.Key)
            .ToListAsync();
    }
}
