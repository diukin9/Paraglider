using Microsoft.EntityFrameworkCore;
using Paraglider.Data.Repositories.Interfaces;
using Paraglider.Domain.Entities;
using Paraglider.Infrastructure;

namespace Paraglider.Data.Repositories;

public class UserRepository : Repository<ApplicationUser>, IUserRepository
{
    public UserRepository(ApplicationDbContext context) : base(context)
    {

    }

    public async Task<ApplicationUser?> FindByEmailAsync(string email)
    {
        var user = await this.GetAll(disableTracking: false)
            .Where(u => u.Email == email)
            .SingleOrDefaultAsync();
        return user;
    }

    public async Task<ApplicationUser?> FindByUsernameAsync(string username)
    {
        var user = await this.GetAll(disableTracking: false)
            .Where(u => u.UserName == username)
            .SingleOrDefaultAsync();
        return user;
    }
}
