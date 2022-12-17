﻿using Paraglider.Application.DataTransferObjects;
using Paraglider.Infrastructure.Common.Interfaces;

namespace Paraglider.Application.DataTransferObjects;

public class AlbumDTO : IDataTransferObject
{
    public string? Name { get; set; }

    public List<MediaDTO> Media { get; set; } = new List<MediaDTO>();
}
