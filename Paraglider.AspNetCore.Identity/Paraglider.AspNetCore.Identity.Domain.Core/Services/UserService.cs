using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Paraglider.AspNetCore.Identity.Domain.Entities;
using Paraglider.AspNetCore.Identity.Domain.Enums;
using Paraglider.AspNetCore.Identity.Domain.Services.Interfaces;
using Paraglider.AspNetCore.Identity.Infrastructure.Responses.OperationResult;
using System.Security.Claims;
using static Paraglider.AspNetCore.Identity.Infrastructure.AppData;

namespace Paraglider.AspNetCore.Identity.Domain.Services
{
    /// <summary>
    /// User service
    /// </summary>
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// Search for a user by email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<OperationResult<ApplicationUser>> FindByEmailAsync(string email)
        {
            var operation = new OperationResult<ApplicationUser>();

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                operation.AddError(Messages.UserNotFound);
                return operation;
            }

            operation.Result = user;
            return operation;
        }

        /// <summary>
        /// Searching for a user by ExternalInfo
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="externalId"></param>
        /// <returns></returns>
        public async Task<OperationResult<ApplicationUser>> FindByExternalInfo(ExternalAuthProvider provider, string externalId)
        {
            var operation = new OperationResult<ApplicationUser>();

            var user = await _userManager.Users.Include(x => x.ExternalInfo)
                .Where(x => x.ExternalInfo.Any(y => y.ExternalProvider == provider && y.ExternalId == externalId))
                .SingleOrDefaultAsync();

            if (user == null)
            {
                operation.AddError(Messages.UserNotFound);
                return operation;
            }

            operation.Result = user;
            return operation;
        }

        /// <summary>
        /// Searching for a user by ExternalInfo
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public async Task<OperationResult<ApplicationUser>> FindByExternalInfo(ExternalInfo info)
        {
            return await FindByExternalInfo(info.ExternalProvider, info.ExternalId);
        }

        /// <summary>
        /// Search for a user by username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<OperationResult<ApplicationUser>> FindByUsernameAsync(string username)
        {
            var operation = new OperationResult<ApplicationUser>();

            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                operation.AddError(Messages.UserNotFound);
                return operation;
            }

            operation.Result = user;
            return operation;
        }

        /// <summary>
        /// Finding a user for external authorization
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public async Task<OperationResult<ApplicationUser>> FindUserForExternalAuthAsync(ExternalLoginInfo info)
        {
            var result = new OperationResult<ApplicationUser>();
            var provider = (ExternalAuthProvider)Enum.Parse(typeof(ExternalAuthProvider), info.LoginProvider);
            var externalId = info!.Principal.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value;

            var user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey)
                ?? (await FindByEmailAsync(info.Principal.Claims.Single(c => c.Type == ClaimTypes.Email).Value)).Result
                ?? (await FindByUsernameAsync(ExternalUsernameTemplate(info.LoginProvider, externalId))).Result
                ?? (await FindByExternalInfo(provider, externalId)).Result;

            if (user == null)
            {
                result.AddError(Messages.UserNotFound);
                return result;
            }

            result.Result = user;
            return result;
        }

        /// <summary>
        /// Create a user using an external provider
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public async Task<OperationResult<ApplicationUser>> CreateUserUsingExternalProvider(ExternalLoginInfo info)
        {
            var result = new OperationResult<ApplicationUser>();

            var externalId = info!.Principal.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var email = info!.Principal.Claims.Single(c => c.Type == ClaimTypes.Email).Value;
            var username = ExternalUsernameTemplate(info.LoginProvider, externalId);
            var firstName = info.Principal.Claims.Single(c => c.Type == ClaimTypes.GivenName).Value;
            var surname = info.Principal.Claims.Single(c => c.Type == ClaimTypes.Surname).Value;
            var provider = (ExternalAuthProvider)Enum.Parse(typeof(ExternalAuthProvider), info.LoginProvider);

            var externalInfo = new ExternalInfo()
            {
                ExternalId = externalId,
                ExternalProvider = provider
            };

            var user = new ApplicationUser()
            {
                Email = email,
                UserName = email,
                EmailConfirmed = true,
                FirstName = firstName,
                Surname = surname,
                ExternalInfo = new List<ExternalInfo>() { externalInfo }
            };

            var createResult = await _userManager.CreateAsync(user);
            if (!createResult.Succeeded)
            {
                result.AddError(Messages.ExternalAuth_UserNotCreated(user.Email, externalInfo.ExternalId));
                return result;
            }

            result.Result = user;
            result.AddInfo(Messages.ExternalAuth_UserCreated(user.Email, externalInfo.ExternalId));
            return result;
        }

        /// <summary>
        /// Add external login information 
        /// </summary>
        /// <param name="info"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<OperationResult> AddLoginAsync(ExternalLoginInfo info, ApplicationUser user)
        {
            var operation = new OperationResult();

            var identityResult = await _userManager.AddLoginAsync(user, info);
            if (!identityResult.Succeeded)
            {
                operation.AddError(Messages.ExternalAuth_FailedAssignExternalLoginInfo(user.Email));
            }

            return operation;
        }
    }
}
