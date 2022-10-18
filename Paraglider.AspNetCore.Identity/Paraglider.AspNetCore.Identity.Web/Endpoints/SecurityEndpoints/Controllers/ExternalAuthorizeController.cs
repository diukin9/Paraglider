using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Paraglider.AspNetCore.Identity.Domain;
using Paraglider.AspNetCore.Identity.Infrastructure;
using Paraglider.AspNetCore.Identity.Web.Application;

namespace Paraglider.AspNetCore.Identity.Web.Controllers
{
    public class ExternalAuthorizeController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public ExternalAuthorizeController(
            SignInManager<ApplicationUser> signInManager, 
            UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        [Route("api/yandex-authenticate")]
        [FeatureGroupName("Authorizations")]
        public IActionResult YandexAuthenticate()
        {
            //TODO added constants
            const string provider = "Yandex.ru";
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, "https://localhost:10001/api/yandex-authorization");
            return Challenge(properties, provider);
        }

        [HttpPost]
        [Route("api/yandex-authorization")]
        [FeatureGroupName("Authorizations")]
        public OperationResult YandexAuthorization()
        {
            throw new NotImplementedException();
        }
    }
}
