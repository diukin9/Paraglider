using Mapster;

namespace Paraglider.Infrastructure.Common.Abstractions;

public abstract class IDerived<TCommon> : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        var commonType = typeof(TCommon);

        config.NewConfig(commonType, GetType())
            .RequireDestinationMemberSource(true);

        config.NewConfig(GetType(), commonType)
            .RequireDestinationMemberSource(false);
    }
}