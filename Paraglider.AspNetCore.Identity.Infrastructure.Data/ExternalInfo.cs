using Paraglider.AspNetCore.Identity.Domain;

namespace Paraglider.AspNetCore.Identity.Infrastructure.Data
{
    public class ExternalInfo : IExternalInfo, IHaveId
    {
        public Guid Id { get; set; }
        public string ExternalId { get; set; }
        public ExternalAuthProvider ExternalProvider { get; set; }
    }
}
