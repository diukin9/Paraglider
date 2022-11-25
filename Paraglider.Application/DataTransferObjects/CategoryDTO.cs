using Mapster;
using Paraglider.Domain.RDB.Entities;
using Paraglider.Infrastructure.Common.Abstractions;

namespace Paraglider.API.DataTransferObjects;

public record CategoryDTO : IDataTransferObject
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;

    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Category, CategoryDTO>()
            .RequireDestinationMemberSource(true);
    }
}
