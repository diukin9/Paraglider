namespace Paraglider.Infrastructure.Common.Response;

/// <summary>
///     Metadata object base for all type  <see cref="IMetadataMessage" />
/// </summary>
[Serializable]
public class Metadata
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
    public object? DataObject { get; private set; }

#pragma warning disable IDE0052 // Удалить непрочитанные закрытые члены
    private readonly OperationResult _source = null!;
#pragma warning restore IDE0052 // Удалить непрочитанные закрытые члены

    public Metadata()
    {
        Type = MetadataType.Info;
    }

    public Metadata(OperationResult source, string message) : this()
    {
        _source = source;
        Message = message;
    }

    public Metadata(OperationResult source, string message, MetadataType type = MetadataType.Info)
    {
        Type = type;
        _source = source;
        Message = message;
    }

    public Metadata(OperationResult source, string message, object? data, MetadataType type = MetadataType.Info)
    {
        Type = type;
        _source = source;
        Message = message;
        DataObject = data;
    }
}