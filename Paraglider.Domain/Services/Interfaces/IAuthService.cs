using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Paraglider.Domain.Entities;
using Paraglider.Infrastructure.Responses.OperationResult;

namespace Paraglider.Domain.Services.Interfaces
{
    /// <summary>
    /// auth service
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Get information about an external login
        /// </summary>
        /// <returns></returns>
        public Task<OperationResult<ExternalLoginInfo>> GetExternalLoginInfoAsync();

        /// <summary>
        /// Logging in through an external provider
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public Task<OperationResult> ExternalLoginSignInAsync(ExternalLoginInfo info);

        /// <summary>
        /// Configuring external authentication properties
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="callbackUrl"></param>
        /// <returns></returns>
        public OperationResult<AuthenticationProperties> ConfigureExternalAuthenticationProperties(string provider, string callbackUrl);

        /// <summary>
        /// Sign out of the account
        /// </summary>
        /// <returns></returns>
        public Task<OperationResult> SignOutAsync();

        /// <summary>
        /// Sign out of the account
        /// </summary>
        /// <returns></returns>
        public Task<OperationResult> PasswordSignInAsync(ApplicationUser user, string password, bool isPersistent, bool lockoutOnFailure);
    }
}
