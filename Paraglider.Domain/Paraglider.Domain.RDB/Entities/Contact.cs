﻿using Paraglider.Infrastructure.Common.Interfaces;

namespace Paraglider.Domain.RDB.Entities;

public class Contact : IIdentified
{
    public Guid Id { get; set; }
    public string Key { get; set; } = null!;
    public string Value { get; set; } = null!;
}