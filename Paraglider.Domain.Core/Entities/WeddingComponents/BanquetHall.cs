using Paraglider.Domain.Abstractions;

namespace Paraglider.Domain.Entities
{
    public class BanquetHall : WeddingComponent
    {
        public virtual List<Premise> Premises { get; set; } = new List<Premise>();
    }
}
