using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using Paraglider.Infrastructure.Common.Interfaces;

namespace Paraglider.Application.DataTransferObjects;

public class ContactsDTO : IDataTransferObject
{
    [BsonElement("phoneNumber")]
    public string PhoneNumber { get; set; } = null!;

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public string? Email { get; set; }

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public string? Telegram { get; set; }

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public string? WhatsApp { get; set; }

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public string? Viber { get; set; }

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public string? Vkontakte { get; set; }

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public string? Instagram { get; set; }
}
