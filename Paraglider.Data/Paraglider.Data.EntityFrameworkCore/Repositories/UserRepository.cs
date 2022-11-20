using Microsoft.EntityFrameworkCore;
using Paraglider.Data.EntityFrameworkCore.Repositories.Interfaces;
using Paraglider.Domain.RDB.Entities;
using Paraglider.Domain.RDB.Enums;
using Paraglider.Infrastructure.Common;

namespace Paraglider.Data.EntityFrameworkCore.Repositories;

public class UserRepository : Repository<ApplicationUser>, IUserRepository
{
    public UserRepository(ApplicationDbContext context) : base(context)
    {

    }

    public async Task<ApplicationUser?> FindByExternalAuthInfoAsync(ExternalAuthProvider provider, string externalId)
    {
        var user = await this.GetAll(disableTracking: true)
            .Include(x => x.City)
            .Include(x => x.WeddingPlannings)
            .Include(x => x.ExternalAuthInfo)
            .Where(x => x.ExternalAuthInfo.Any(y => y.ExternalId == externalId && y.ExternalProvider == provider))
            .SingleOrDefaultAsync();
        return user;
    }

    public async Task<ApplicationUser?> FindByEmailAsync(string email)
    {
        var user = await this.GetAll(disableTracking: true)
            .Where(u => u.Email == email)
            .Include(x => x.City)
            .Include(x => x.WeddingPlannings)
            .Include(x => x.ExternalAuthInfo)
            .SingleOrDefaultAsync();
        return user;
    }

    public async Task<ApplicationUser?> FindByUsernameAsync(string username)
    {
        var user = await this.GetAll(disableTracking: true)
            .Where(u => u.UserName == username)
            .Include(x => x.City)
            .Include(x => x.WeddingPlannings)
            .Include(x => x.ExternalAuthInfo)
            .SingleOrDefaultAsync();
        return user;
    }
}
