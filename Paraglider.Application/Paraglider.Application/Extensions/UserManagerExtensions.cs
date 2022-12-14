using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Paraglider.Domain.RDB.Entities;
using Paraglider.Domain.RDB.Enums;

namespace Paraglider.Application.Extensions;

public static class UserManagerExtensions
{
    public static async Task<ApplicationUser?> FindByExternalLoginInfoAsync(
        this UserManager<ApplicationUser> userManager,
        AuthProvider provider,
        string externalId)
    {
        var user =  await userManager.Users
            .Where(x => x.ExternalAuthInfo
                .Any(y => y.ExternalId == externalId && y.Provider == provider))
            .SingleOrDefaultAsync();

        return user;
    }

    public static async Task<UserLoginInfo?> FindUserLoginInfoAsync(
        this UserManager<ApplicationUser> userManager,
        ApplicationUser user,
        string loginProvider,
        string providerkey)
    {
        var collection = await userManager.GetLoginsAsync(user);

        var login = collection
            .Where(x => x.LoginProvider == loginProvider && x.ProviderKey == providerkey)
            .SingleOrDefault();

        return login;
    }

    public static async Task<ApplicationUser?> FindByNameIdentifierAsync(
        this UserManager<ApplicationUser> userManager,
        string nameIdentifier)
    {
        return await userManager.FindByEmailAsync(nameIdentifier) 
            ?? await userManager.FindByNameAsync(nameIdentifier);
    }
}
