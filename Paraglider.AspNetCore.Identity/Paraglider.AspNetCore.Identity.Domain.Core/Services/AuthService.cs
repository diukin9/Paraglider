using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Paraglider.AspNetCore.Identity.Domain.Entities;
using Paraglider.AspNetCore.Identity.Domain.Services.Interfaces;
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
        public OperationResult<AuthenticationProperties> ConfigureExternalAuthenticationProperties(string provider, string callbackUrl)
        {
            return new OperationResult<AuthenticationProperties>
            {
                Result = _signInManager.ConfigureExternalAuthenticationProperties(provider, callbackUrl)
            };
        }

        /// <summary>
        /// Logging in through an external provider
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public async Task<OperationResult> ExternalLoginSignInAsync(ExternalLoginInfo info)
        {
            var result = new OperationResult();

            var email = info.Principal.Claims.Single(c => c.Type == ClaimTypes.Email).Value;

            var signInResult = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, true);
            if (!signInResult.Succeeded)
            {
                result.AddError(Messages.ExternalAuth_FailedAuth(email, info.LoginProvider));
            }
            return result;
        }

        /// <summary>
        /// Get information about an external login
        /// </summary>
        /// <returns></returns>
        public async Task<OperationResult<ExternalLoginInfo>> GetExternalLoginInfoAsync()
        {
            var result = new OperationResult<ExternalLoginInfo>();

            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                result.AddError(Messages.ExternalAuth_EmptyExternalLoginInfo());
            }
            else
            {
                result.Result = info;
            }

            return result;
        }
    }
}
