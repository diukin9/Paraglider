namespace Paraglider.AspNetCore.Identity.Infrastructure.Data
{
    public interface IExternalInfo
    {
        public ExternalAuthProvider ExternalProvider { get; set; }
        public string ExternalId { get; }
    }
}
