﻿using Newtonsoft.Json;
using Paraglider.GorkoClient.Models.Abstractions;

namespace Paraglider.GorkoClient.Models;

public class Car : IHaveId
{
    public long? Id { get; set; }
    public string? Name { get; set; }
    public City? City { get; set; }

    public string? Text { get; set; }

    public IReadOnlyCollection<CatalogMedia>? Media { get; set; }

    [JsonProperty("params")] public CarParameters? Parameters { get; set; }

    public IReadOnlyCollection<Contact>? Contacts { get; set; }


    public long? CityId => City?.Id;
    public string? CityName => City?.Name;
}