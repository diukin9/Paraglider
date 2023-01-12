using Paraglider.Domain.RDB.Entities;
using Paraglider.Domain.RDB.Enums;
using Paraglider.Infrastructure.Common.Repository;
using System.Linq.Expressions;

namespace Paraglider.Data.EntityFrameworkCore.Repositories.Interfaces;

public interface ICategoryRepository : IRepository<Category>
{
    public Task<Category?> FindByKeyAsync(Source source, string key);
}
