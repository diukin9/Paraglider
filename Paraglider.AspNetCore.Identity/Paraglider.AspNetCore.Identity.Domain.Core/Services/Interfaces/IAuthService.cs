﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Paraglider.AspNetCore.Identity.Infrastructure.Responses.OperationResult;

namespace Paraglider.AspNetCore.Identity.Domain.Services.Interfaces
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
    }
}
