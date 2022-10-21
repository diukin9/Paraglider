using Microsoft.AspNetCore.Identity;
using Paraglider.AspNetCore.Identity.Domain.Entities;
using Paraglider.AspNetCore.Identity.Domain.Enums;
using Paraglider.AspNetCore.Identity.Infrastructure.Responses.OperationResult;

namespace Paraglider.AspNetCore.Identity.Domain.Services.Interfaces
{
    /// <summary>
    /// User service
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Finding a user for external authorization
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public Task<OperationResult<ApplicationUser>> FindUserForExternalAuthAsync(ExternalLoginInfo info);

        /// <summary>
        /// Search for a user by email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public Task<OperationResult<ApplicationUser>> FindByEmailAsync(string email);

        /// <summary>
        /// Search for a user by username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public Task<OperationResult<ApplicationUser>> FindByUsernameAsync(string username);

        /// <summary>
        /// Searching for a user by ExternalInfo
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="externalId"></param>
        /// <returns></returns>
        public Task<OperationResult<ApplicationUser>> FindByExternalInfo(ExternalAuthProvider provider, string externalId);

        /// <summary>
        /// Searching for a user by ExternalInfo
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public Task<OperationResult<ApplicationUser>> FindByExternalInfo(ExternalInfo info);

        /// <summary>
        /// Create a user using an external provider
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public Task<OperationResult<ApplicationUser>> CreateUserUsingExternalProvider(ExternalLoginInfo info);

        /// <summary>
        /// Add external login information 
        /// </summary>
        /// <param name="info"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<OperationResult> AddLoginAsync(ExternalLoginInfo info, ApplicationUser user);
    }
}
