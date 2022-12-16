﻿using Newtonsoft.Json;
using Paraglider.Infrastructure.Common.Abstractions;

namespace Paraglider.Application.DataTransferObjects;

public class CapacityDTO : IDataTransferObject
{
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public int? Min { get; set; }

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public int? Max { get; set; }
}
