﻿using Paraglider.AspNetCore.Identity.Domain.Abstractions;

namespace Paraglider.AspNetCore.Identity.Domain.Entities
{
    public class Album : IHaveId
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public virtual List<Media> Media { get; set; } = new List<Media>();
    }
}
