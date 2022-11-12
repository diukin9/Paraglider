using Paraglider.AspNetCore.Identity.Domain.Abstractions;
using Paraglider.AspNetCore.Identity.Domain.Enums;

namespace Paraglider.AspNetCore.Identity.Domain.Entities
{
    public class Media : IHaveId 
    {
        public Guid Id { get; set; }
        public MediaType Type { get; set; }
        public string Url { get; set; } = null!;
    }
}