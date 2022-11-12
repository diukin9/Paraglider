using Paraglider.Infrastructure.Abstractions;

namespace Paraglider.Web.Endpoints.SecurityEndpoints.ViewModels
{
    public class BasicAuthViewModel : IViewModel
    {
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
