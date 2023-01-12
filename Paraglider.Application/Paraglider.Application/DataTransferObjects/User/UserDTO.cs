using Paraglider.Infrastructure.Common.Interfaces;
using System.Text.Json.Serialization;

namespace Paraglider.Application.DataTransferObjects;

public class UserDTO : IDataTransferObject
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("firstname")]
    public string FirstName { get; set; } = null!;

    [JsonPropertyName("surname")]
    public string Surname { get; set; } = null!;

    [JsonPropertyName("username")]
    public string UserName { get; set; } = null!;

    [JsonPropertyName("email")]
    public string? Email { get; set; }

    [JsonPropertyName("city")]
    public CityDTO City { get; set; } = null!;

    [JsonPropertyName("planning")]
    public PlanningDTO Planning { get; set; } = null!;

    [JsonPropertyName("favourites")]
    public List<ComponentDTO> Favourites { get; set; } = null!;
}