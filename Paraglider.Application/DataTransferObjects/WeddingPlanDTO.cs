﻿using Mapster;
using Paraglider.Domain.RDB.Entities;
using Paraglider.Infrastructure.Common.Abstractions;

namespace Paraglider.API.DataTransferObjects
{
    public class WeddingPlanDTO : IDataTransferObject
    {
        public Guid Id { get; set; }
        public CityDTO City { get; set; } = null!;

        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<WeddingPlanning, WeddingPlanDTO>()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.City, src => src.City)
                .RequireDestinationMemberSource(true);
        }
    }
}