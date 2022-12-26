using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Paraglider.Data.EntityFrameworkCore.Repositories.Interfaces;
using Paraglider.Domain.RDB.Entities;
using Paraglider.Infrastructure.Common.Repository;

namespace Paraglider.Data.EntityFrameworkCore.Repositories;

public class UserRepository : Repository<ApplicationUser>, IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<ApplicationUser>> FindAsync(Expression<Func<ApplicationUser, bool>> selector)
    {
        return await _context.Users.IncludeAll().Where(selector).ToListAsync();
    }

    public override async Task<ApplicationUser?> FindByIdAsync(Guid id)
    {
        var user = await _context.Users.IncludeAll()
            .Where(x => x.Id == id)
            .SingleOrDefaultAsync();
        return user;
    }

    public async Task<ApplicationUser?> FindByUsernameAsync(string username)
    {
        var user = await _context.Users.IncludeAll()
            .Where(u => string.Compare(u.UserName, username, StringComparison.OrdinalIgnoreCase) == 0)
            .SingleOrDefaultAsync();
        return user;
    }

    public async Task<ApplicationUser?> FindByEmailAsync(string email)
    {
        var user = await _context.Users.IncludeAll()
            .Where(u => string.Compare(u.Email, email, StringComparison.OrdinalIgnoreCase) == 0)
            .SingleOrDefaultAsync();
        return user;
    }

    public async Task<bool> ChangeCity(Guid userId, Guid newCityId, CancellationToken cancellationToken)
    {
        var retrievedUser = await _context.Users.FindAsync(userId);
        if (retrievedUser == null)
            return false;

        var city = await _context.Cities.FindAsync(newCityId);
        if (city == null)
            return false;

        retrievedUser.City = city;

        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<ApplicationUser?> FindByNameIdentifierAsync(string nameIdentifier)
    {
        return await FindByUsernameAsync(nameIdentifier) ?? await FindByEmailAsync(nameIdentifier);
    }
}
