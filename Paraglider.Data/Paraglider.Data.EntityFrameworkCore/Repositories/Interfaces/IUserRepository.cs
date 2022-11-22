using Paraglider.Domain.RDB.Entities;
using Paraglider.Infrastructure.Common.Abstractions;

namespace Paraglider.Data.EntityFrameworkCore.Repositories.Interfaces;

public interface IUserRepository : IRepository<ApplicationUser>, IShouldSaveChanges
{
    public Task<ApplicationUser?> FindByUsernameAsync(string username);
}
