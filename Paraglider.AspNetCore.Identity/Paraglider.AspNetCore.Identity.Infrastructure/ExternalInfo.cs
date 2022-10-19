using Paraglider.AspNetCore.Identity.Domain;

namespace Paraglider.AspNetCore.Identity.Infrastructure
{
    public class ExternalInfo : IExternalInfo, IHaveId
    {
        public Guid Id { get; set; }
        public string ExternalId { get; set; }
        public AuthProvider ExternalProvider { get; set; }
    }
}
