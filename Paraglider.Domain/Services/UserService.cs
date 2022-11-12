using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Paraglider.Domain.Entities;
using Paraglider.Domain.Enums;
using Paraglider.Domain.Services.Interfaces;
using Paraglider.Infrastructure.Extensions;
using Paraglider.Infrastructure.Responses.OperationResult;
using System.Security.Claims;
using static Paraglider.Infrastructure.AppData;

namespace Paraglider.Domain.Services
{
    /// <summary>
    /// User service
    /// </summary>
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _accessor;

        public UserService(UserManager<ApplicationUser> userManager, IHttpContextAccessor accessor)
        {
            _userManager = userManager;
            _accessor = accessor;
        }

        #region Find user

        public async Task<OperationResult<ApplicationUser>> GetCurrentUserAsync()
        {
            var operation = new OperationResult<ApplicationUser>();

            var user = await _userManager.GetUserAsync(_accessor!.HttpContext!.User);
            if (user == null) return operation.AddError(Messages.Auth_NoAuthorizedUser);

            return operation.AddResult(user);
        }

        /// <summary>
        /// Search for a user by email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<OperationResult<ApplicationUser>> FindByEmailAsync(string? email)
        {
            var operation = new OperationResult<ApplicationUser>();

            email.ValidateForNull(ref operation);
            if (!operation.IsOk) return operation;

            var user = await _userManager.FindByEmailAsync(email);
            user.ValidateForNull(ref operation);
            if (!operation.IsOk) return operation;

            return operation.AddResult(user);
        }

        /// <summary>
        /// Searching for a user by ExternalInfo
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="externalId"></param>
        /// <returns></returns>
        public async Task<OperationResult<ApplicationUser>> FindByExternalInfo(ExternalAuthProvider provider, string? externalId)
        {
            var operation = new OperationResult<ApplicationUser>();

            externalId.ValidateForNull(ref operation);
            if (!operation.IsOk) return operation;

            var user = await _userManager.Users.Include(x => x.ExternalAuthInfo)
                .Where(x => x.ExternalAuthInfo.Any(y => y.ExternalProvider == provider && y.ExternalId == externalId))
                .SingleOrDefaultAsync();

            if (user == null) return operation.AddError(Messages.UserNotFound);

            return operation.AddResult(user);
        }

        /// <summary>
        /// Searching for a user by ExternalInfo
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public async Task<OperationResult<ApplicationUser>> FindByExternalInfo(ExternalInfo? info)
        {
            var operation = new OperationResult<ApplicationUser>();

            info.ValidateForNull(ref operation);
            if (!operation.IsOk) return operation;

            var findResult = await FindByExternalInfo(info!.ExternalProvider, info.ExternalId);
            findResult.RescheduleResult(ref operation);
            if (!operation.IsOk) return operation;

            return operation.AddResult(findResult.Result!);
        }

        /// <summary>
        /// Search for a user by username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<OperationResult<ApplicationUser>> FindByUsernameAsync(string? username)
        {
            var operation = new OperationResult<ApplicationUser>();

            username.ValidateForNull(ref operation);
            if (!operation.IsOk) return operation;

            var user = await _userManager.FindByNameAsync(username);
            if (user == null)return operation.AddError(Messages.UserNotFound);

            return operation.AddResult(user);
        }

        /// <summary>
        /// Finding a user for external authorization
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public async Task<OperationResult<ApplicationUser>> FindUserForExternalAuthAsync(ExternalLoginInfo info)
        {
            var operation = new OperationResult<ApplicationUser>();

            info.ValidateForNull(ref operation);
            if (!operation.IsOk) return operation;

            var provider = (ExternalAuthProvider)Enum.Parse(typeof(ExternalAuthProvider), info.LoginProvider);
            var externalId = info!.Principal.Claims.SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var email = info.Principal.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            var username = ExternalUsernameTemplate(info.LoginProvider, externalId);

            var user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey)
                ?? (await FindByEmailAsync(email)).Result
                ?? (await FindByUsernameAsync(username)).Result
                ?? (await FindByExternalInfo(provider, externalId)).Result;

            if (user == null) return operation.AddError(Messages.UserNotFound);

            return operation.AddResult(user);
        }

        #endregion

        #region Create user

        /// <summary>
        /// Create a user using an external provider
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public async Task<OperationResult<ApplicationUser>> CreateUserUsingExternalProvider(ExternalLoginInfo info)
        {
            var operation = new OperationResult<ApplicationUser>();

            var externalId = info!.Principal.Claims.SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            externalId.ValidateForNull(ref operation);
            if (!operation.IsOk) return operation;

            var firstName = info.Principal.Claims.SingleOrDefault(c => c.Type == ClaimTypes.GivenName)?.Value;
            firstName.ValidateForNull(ref operation);
            if (!operation.IsOk) return operation;

            var surname = info.Principal.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Surname)?.Value;
            surname.ValidateForNull(ref operation);
            if (!operation.IsOk) return operation;

            var username = ExternalUsernameTemplate(info.LoginProvider, externalId);
            var email = info!.Principal.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            var provider = (ExternalAuthProvider)Enum.Parse(typeof(ExternalAuthProvider), info.LoginProvider);

            var externalInfo = new ExternalInfo() { ExternalId = externalId!, ExternalProvider = provider };

            var user = new ApplicationUser()
            {
                Email = email,
                UserName = username,
                EmailConfirmed = true,
                FirstName = firstName!,
                Surname = surname!,
                ExternalAuthInfo = new List<ExternalInfo>() { externalInfo }
            };

            var createResult = await _userManager.CreateAsync(user);
            if (!createResult.Succeeded)
            {
                return operation.AddError(Messages.ExternalAuth_UserNotCreated(user.Email ?? username, externalInfo.ExternalId));
            }

            return operation
                .AddResult(user)
                .AddSuccess(Messages.ExternalAuth_UserCreated(user.Email ?? username, externalInfo.ExternalId));
        }

        #endregion

        /// <summary>
        /// Add external login information 
        /// </summary>
        /// <param name="info"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<OperationResult> AddLoginAsync(ExternalLoginInfo info, ApplicationUser user)
        {
            var operation = new OperationResult();

            info.Validate(ref operation);
            if (!operation.IsOk) return operation;

            user.Validate(ref operation);
            if (!operation.IsOk) return operation;

            var identityResult = await _userManager.AddLoginAsync(user, info);
            if (!identityResult.Succeeded)
            {
                operation.AddError(Messages.ExternalAuth_FailedAssignExternalLoginInfo(user.Email ?? user.UserName));
            }

            return operation;
        }
    }
}
