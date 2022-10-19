using Paraglider.AspNetCore.Identity.Domain;

namespace Paraglider.AspNetCore.Identity.Web.Endpoints.SecurityEndpoints.ViewModels
{
    public class BasicAuthViewModel : IViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
