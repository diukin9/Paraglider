﻿using Newtonsoft.Json;
using Paraglider.GorkoClient.Models.Abstractions;

namespace Paraglider.GorkoClient.Models;

//Используется для фильтрации
public partial class User : IHaveCityId
{
    [JsonProperty("city_id")] public long? CityId => City?.Id;
    [JsonProperty("city_name")] public string? CityName => City?.Name;
}