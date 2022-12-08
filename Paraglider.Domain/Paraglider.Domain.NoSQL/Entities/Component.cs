﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Paraglider.Domain.NoSQL.ValueObjects;
using Paraglider.Infrastructure.Common.Abstractions;

namespace Paraglider.Domain.NoSQL.Entities;

[BsonIgnoreExtraElements]
public class Component : IIdentified
{
    [BsonId]
    [BsonElement("id")]
    public Guid Id { get; set; } //

    [BsonElement("externalId")]
    public string ExternalId { get; set; } = null!; //

    [BsonElement("provider")]
    public string Provider { get; set; } = null!; //

    [BsonElement("category")]
    public Category Category { get; set; } = null!; //

    [BsonElement("name")]
    public string Name { get; set; } = null!; //

    [BsonElement("description")]
    public string? Description { get; set; } = null!; //

    [BsonElement("avatarUrl")]
    public string? AvatarUrl { get; set; } //

    [BsonElement("city")]
    public City City { get; set; } = null!; //

    [BsonElement("album")]
    public Album Album { get; set; } = null!; //

    [BsonElement("contacts")]
    public Contacts Contacts { get; set; } = null!; //

    [BsonElement("reviews")]
    public List<Review> Reviews { get; set; } = new List<Review>(); //

    [BsonElement("manufactureYear")]
    public DateTime? ManufactureYear { get; set; } //

    [BsonElement("minRentLength")]
    public TimeSpan? MinRentLength { get; set; } //

    [BsonElement("capacity")]
    public int? Capacity { get; set; } //

    [BsonElement("services")]
    public List<Service>? Services { get; set; } //

    [BsonElement("halls")]
    public List<Hall>? Halls { get; set; } //

    [BsonElement("createdAt")]
    public DateTime CreatedAt { get; set; }

    [BsonElement("updatedAt")]
    public DateTime UpdatedAt { get; set; }
}