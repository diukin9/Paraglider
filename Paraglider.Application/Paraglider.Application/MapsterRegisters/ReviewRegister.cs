using Mapster;
using Paraglider.Application.DataTransferObjects;
using Paraglider.Domain.NoSQL.ValueObjects;

namespace Paraglider.Application.MapsterRegisters;

public class ReviewRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Review, ReviewDTO>();
    }
}
