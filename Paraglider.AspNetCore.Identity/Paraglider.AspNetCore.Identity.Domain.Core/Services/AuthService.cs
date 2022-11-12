using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Paraglider.AspNetCore.Identity.Domain.Entities;
using Paraglider.AspNetCore.Identity.Domain.Services.Interfaces;
using Paraglider.AspNetCore.Identity.Infrastructure.Extensions;
using Paraglider.AspNetCore.Identity.Infrastructure.Responses.OperationResult;
using System.Security.Claims;
using static Paraglider.AspNetCore.Identity.Infrastructure.AppData;

namespace Paraglider.AspNetCore.Identity.Domain.Services
{
    /// <summary>
    /// auth service
    /// </summary>
    public class AuthService : IAuthService
    {
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthService(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }

        /// <summary>
        /// Configuring external authentication properties
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="callbackUrl"></param>
        /// <returns></returns>
        public OperationResult<AuthenticationProperties> ConfigureExternalAuthenticationProperties(string? provider, string? callbackUrl)
        {
            var operation = new OperationResult<AuthenticationProperties>();

            provider.ValidateForNull(ref operation);
            if (!operation.IsOk) return operation;

            callbackUrl.ValidateForNull(ref operation);
            if (!operation.IsOk) return operation;

            return operation.AddResult(_signInManager.ConfigureExternalAuthenticationProperties(provider, callbackUrl));
        }

        #region Sign in

        /// <summary>
        /// Logging in through an external provider
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public async Task<OperationResult> ExternalLoginSignInAsync(ExternalLoginInfo? info)
        {
            var operation = new OperationResult();

            info.Validate(ref operation);
            if (!operation.IsOk) return operation;

            var email = info!.Principal.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            var externalId = info!.Principal.Claims.SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var username = ExternalUsernameTemplate(info.LoginProvider, externalId);

            var signInResult = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, true);
            if (!signInResult.Succeeded) return operation.AddError(Messages.ExternalAuth_FailedAuth(email ?? username, info.LoginProvider));
            return operation;
        }

        /// <summary>
        /// Authorize via password
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <param name="isPersistent"></param>
        /// <param name="lockoutOnFailure"></param>
        /// <returns></returns>
        public async Task<OperationResult> PasswordSignInAsync(ApplicationUser user, string password, bool isPersistent, bool lockoutOnFailure)
        {
            var operation = new OperationResult();

            var signInResult = await _signInManager.PasswordSignInAsync(user, password, isPersistent, lockoutOnFailure);
            if (!signInResult.Succeeded) return operation.AddError(Messages.BasicAuth_FailedAuth(user.Email ?? user.UserName));

            return operation;
        }

        #endregion

        /// <summary>
        /// Get information about an external login
        /// </summary>
        /// <returns></returns>
        public async Task<OperationResult<ExternalLoginInfo>> GetExternalLoginInfoAsync()
        {
            var operation = new OperationResult<ExternalLoginInfo>();

            var info = await _signInManager.GetExternalLoginInfoAsync();
            info.ValidateForNull(ref operation);
            if (!operation.IsOk) return operation;

            return operation.AddResult(info);
        }

        /// <summary>
        /// Sign out of the account
        /// </summary>
        /// <returns></returns>
        public async Task<OperationResult> SignOutAsync()
        {
            await _signInManager.SignOutAsync();
            return new OperationResult();
        }
    }
}
