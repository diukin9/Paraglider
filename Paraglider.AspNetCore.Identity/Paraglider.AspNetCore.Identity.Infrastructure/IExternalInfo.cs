namespace Paraglider.AspNetCore.Identity.Infrastructure
{
    public interface IExternalInfo
    {
        public AuthProvider ExternalProvider { get; set; }
        public string ExternalId { get; }
    }
}
