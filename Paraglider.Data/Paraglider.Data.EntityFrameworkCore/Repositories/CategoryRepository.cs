using Microsoft.EntityFrameworkCore;
using Paraglider.Data.EntityFrameworkCore.Repositories.Interfaces;
using Paraglider.Domain.RDB.Entities;
using Paraglider.Domain.RDB.Enums;
using Paraglider.Infrastructure.Common.Repository;
using System.Linq.Expressions;

namespace Paraglider.Data.EntityFrameworkCore.Repositories;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    private readonly ApplicationDbContext _context;

    public CategoryRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<Category>> FindAsync(Expression<Func<Category, bool>> selector)
    {
        var categories = await _context.Categories.IncludeAll()
            .Where(selector)
            .ToListAsync();

        return categories;
    }

    public override async Task<Category?> FindByIdAsync(Guid id)
    {
        var category = await _context.Categories.IncludeAll()
            .Where(x => x.Id == id)
            .SingleOrDefaultAsync();

        return category;
    }

    public async Task<Category?> FindByKeyAsync(Source source, string key)
    {
        var categories = await _context.Categories.IncludeAll()
            .Where(x => x.Keys.Any(y => y.Source == source && y.Key == key))
            .SingleOrDefaultAsync();

        return categories;
    }
}
