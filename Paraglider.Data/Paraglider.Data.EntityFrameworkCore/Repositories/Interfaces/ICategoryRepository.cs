using Paraglider.Domain.Common.Enums;
using Paraglider.Domain.RDB.Entities;
using Paraglider.Infrastructure.Common.Abstractions;

namespace Paraglider.Data.EntityFrameworkCore.Repositories.Interfaces;

public interface ICategoryRepository : IRepository<Category>
{
    public Task<Category?> FindByKeyAsync(Source source, string key);
}
