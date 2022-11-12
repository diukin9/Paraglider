using Paraglider.Domain.Enums;

namespace Paraglider.Domain.ValueObjects
{
    public class ExternalInfo
    {
        public ExternalInfo() { }

        public ExternalInfo(string externalId, ExternalProvider provider)
        {
            if (string.IsNullOrEmpty(externalId))
            {
                throw new ArgumentNullException("The external id cannot be empty");
            }

            Id = externalId;
            Provider = provider;
        }

        public readonly string Id;
        public readonly ExternalProvider Provider;

        public string GetUniqueId()
        {
            return $"{Enum.GetName(typeof(ExternalProvider), Provider)}-{Id}";
        }
    }
}
