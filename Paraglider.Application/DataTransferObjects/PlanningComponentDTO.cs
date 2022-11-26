﻿using Mapster;
using Newtonsoft.Json;
using Paraglider.Domain.RDB.Entities;
using Paraglider.Infrastructure.Common.Abstractions;

namespace Paraglider.API.DataTransferObjects;

public class PlanningComponentDTO : IDataTransferObject
{
    [JsonIgnore]
    public Guid ComponentId { get; set; }
    public object? Component { get; set; }
    public ComponentDescDTO ComponentDesc { get; set; } = null!;

    public void Register(TypeAdapterConfig config)
    {
#pragma warning disable CS8603 // Возможно, возврат ссылки, допускающей значение NULL.

        config.NewConfig<PlanningComponent, PlanningComponentDTO>()
            .Ignore(dest => dest.Component)
            .RequireDestinationMemberSource(true);

#pragma warning restore CS8603 // Возможно, возврат ссылки, допускающей значение NULL.
    }
}