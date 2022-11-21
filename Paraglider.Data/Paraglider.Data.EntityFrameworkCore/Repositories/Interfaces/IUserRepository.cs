using Paraglider.Domain.RDB.Entities;
using Paraglider.Domain.RDB.Enums;
using Paraglider.Infrastructure.Common.Abstractions;

namespace Paraglider.Data.EntityFrameworkCore.Repositories.Interfaces;

public interface IUserRepository : IRepository<ApplicationUser>
{
    public Task<ApplicationUser?> FindByEmailAsync(string email);
    public Task<ApplicationUser?> FindByUsernameAsync(string username);

    public Task<ApplicationUser?> FindByExternalAuthInfoAsync(ExternalAuthProvider provider, string externalId);
}
