﻿using Paraglider.Domain.RDB.Entities;
using Paraglider.Infrastructure.Common.Abstractions;

namespace Paraglider.Data.EntityFrameworkCore.Repositories.Interfaces;

public interface IUserRepository : IRepository<ApplicationUser>
{
    public Task<ApplicationUser?> FindByUsernameAsync(string username);
    public Task<bool> ChangeCity(Guid userId, Guid newCityId, CancellationToken cancellationToken);
}
