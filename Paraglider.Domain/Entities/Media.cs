using Paraglider.Domain.Abstractions;
using Paraglider.Domain.Enums;

namespace Paraglider.Domain.Entities
{
    public class Media : IIdentified 
    {
        public Guid Id { get; set; }
        public MediaType Type { get; set; }
        public string Url { get; set; } = null!;
    }
}