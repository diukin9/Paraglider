using Paraglider.AspNetCore.Identity.Infrastructure.Abstractions;

namespace Paraglider.AspNetCore.Identity.Web.Endpoints.SecurityEndpoints.ViewModels
{
    public class BasicAuthViewModel : IViewModel
    {
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
