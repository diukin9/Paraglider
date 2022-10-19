using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Paraglider.AspNetCore.Identity.Infrastructure;
using Paraglider.AspNetCore.Identity.Web.Application;
using System.Net;
using System.Security.Claims;
using static Paraglider.AspNetCore.Identity.Domain.AppData;

namespace Paraglider.AspNetCore.Identity.Web.Controllers
{
    public class ExternalAuthController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<ExternalAuthController> _logger;

        public ExternalAuthController(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            ILogger<ExternalAuthController> logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
        }

        [HttpGet]
        [Route("api/external-auth")]
        [FeatureGroupName("Security")]
        public IActionResult ExternalAuth([FromQuery] string provider, [FromQuery] string returnUrl = "/")
        {
            if (provider != Enum.GetName(typeof(AuthProvider), AuthProvider.Yandex))
            {
                _logger.LogError(Messages.InvalidExternalAuthProvider(provider));
                return BadRequest(Messages.InvalidExternalAuthProvider(provider));
            }

            var root = $"/api/external-auth-handler?returnUrl={WebUtility.UrlEncode(returnUrl)}";
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, root);

            return Challenge(properties, provider);
        }

        [Route("api/external-auth-handler")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> ExternalAuthHandler(string remoteError, string returnUrl = "/")
        {
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null || remoteError != null)
            {
                _logger.LogError(Messages.EmptyExternalLoginInfo());
                return BadRequest(Messages.EmptyExternalLoginInfo());
            }

            var externalId = info.Principal.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var email = info.Principal.Claims.Single(c => c.Type == ClaimTypes.Email).Value;
            var provider = (AuthProvider)Enum.Parse(typeof(AuthProvider), info.LoginProvider);

            var user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey)
                ?? await _userManager.FindByEmailAsync(info.Principal.Claims.Single(c => c.Type == ClaimTypes.Email).Value)
                ?? await _userManager.Users.Include(x => x.ExternalInfo)
                    .Where(x => x.ExternalInfo.Any(y => 
                        y.ExternalProvider == provider && y.ExternalId == externalId))
                    .SingleOrDefaultAsync();

            if (user == null)
            {
                _logger.LogInformation(Messages.ExternalUserNotExist(
                    info.Principal.Claims.Single(c => c.Type == ClaimTypes.Email).Value,
                    info.Principal.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value
                ));

                var externalInfo = new ExternalInfo()
                {
                    ExternalId = info.Principal.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value,
                    ExternalProvider = (AuthProvider)Enum.Parse(typeof(AuthProvider), info.LoginProvider)
                };

                user = new ApplicationUser()
                {
                    Email = info.Principal.Claims.Single(c => c.Type == ClaimTypes.Email).Value,
                    UserName = info.Principal.Claims.Single(c => c.Type == ClaimTypes.Email).Value,
                    EmailConfirmed = true,
                    FirstName = info.Principal.Claims.Single(c => c.Type == ClaimTypes.GivenName).Value,
                    Surname = info.Principal.Claims.Single(c => c.Type == ClaimTypes.Surname).Value,
                    ExternalInfo = new List<ExternalInfo>() { externalInfo }
                };

                var createResult = await _userManager.CreateAsync(user);
                if (!createResult.Succeeded)
                {
                    _logger.LogError(Messages.FailedCreatedExternalUser(user.Email, externalInfo.ExternalId));
                    return BadRequest(Messages.FailedCreatedExternalUser(user.Email, externalInfo.ExternalId));
                }
                _logger.LogInformation(Messages.SuccessfullyCreatedExternalUser(user.Email, externalInfo.ExternalId));
            }

            var identityResult = await _userManager.AddLoginAsync(user, info);
            if (!identityResult.Succeeded)
            {
                _logger.LogError(Messages.FailedAssignExternalLoginInfo(user.Email));
                return BadRequest(Messages.FailedAssignExternalLoginInfo(user.Email));
            }

            var signInResult = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, true);
            if (!signInResult.Succeeded)
            {
                _logger.LogError(Messages.FailedAuthByExternalProvider(user.Email, info.LoginProvider));
                return BadRequest(Messages.FailedAuthByExternalProvider(user.Email, info.LoginProvider));
            }

            _logger.LogInformation(Messages.SuccessfullyAuthByExternalProvider(user.Email, info.LoginProvider));
            return Redirect(returnUrl!);
        }
    }
}
