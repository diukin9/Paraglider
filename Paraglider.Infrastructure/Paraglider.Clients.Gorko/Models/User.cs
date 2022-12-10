using Paraglider.Clients.Gorko.Models.Abstractions;

namespace Paraglider.Clients.Gorko.Models;

public partial class User : IHaveId, IHaveReviews
{
    //v2
    public long? Id { get; set; }

    public string? Name { get; set; }

    public string? ProfileUrl { get; set; }

    public string? AvatarUrl { get; set; }

    public Role? Role { get; set; }

    public IReadOnlyCollection<CatalogMedia>? CatalogMedia { get; set; }

    public City? City { get; set; }

    public string? Text { get; set; }

    public List<Contact>? Contacts { get; set; }
    
    public bool Banned { get; set; }

    public List<Spec>? Specs { get; set; }


    //v3
    public IReadOnlyCollection<Review>? Reviews { get; set; }

    // #region Serializer's callbacks
    //
    // [OnDeserialized]
    // internal void OnDeserialized(StreamingContext streamingContext)
    // {
    //     Text = Text?.RemoveHtmlTags();
    // }
    //
    // #endregion
}