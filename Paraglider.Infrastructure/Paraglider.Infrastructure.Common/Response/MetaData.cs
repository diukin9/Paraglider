namespace Paraglider.Infrastructure.Common.Response;

/// <summary>
///     Metadata object base for all type  <see cref="IMetadataMessage" />
/// </summary>
[Serializable]
public class Metadata<T>
{
    /// <summary>
    /// Message sent to the metadata
    /// </summary>
    public string? Message { get; }

    /// <summary>
    /// Metadata type
    /// </summary>
    public MetadataType Type { get; }

    /// <summary>
    /// Data object
    /// </summary>
    public T? DataObject { get; private set; }

    public Metadata(string message, MetadataType type)
    {
        Type = type;
        Message = message;
    }

    public Metadata(string message, T? data, MetadataType type)
    {
        Type = type;
        Message = message;
        DataObject = data;
    }
}