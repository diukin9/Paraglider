﻿namespace Paraglider.AspNetCore.Identity.Infrastructure.Responses.OperationResult;

/// <summary>
///     Metadata object base for all type  <see cref="IMetadataMessage" />
/// </summary>
[Serializable]
public class Metadata
{
    private readonly OperationResult _source;

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

    /// <summary>
    /// Message sent to the metadata
    /// </summary>
    public string Message { get; }

    /// <summary>
    /// Metadata type
    /// </summary>
    public MetadataType Type { get; }

    /// <summary>
    /// Data object
    /// </summary>
    public object? DataObject { get; private set; }

    /// <summary>
    ///     Add special type of metadata
    /// </summary>
    /// <param name="data"></param>
    public void AddData(object data)
    {
        if (data is Exception exception && _source.Metadata == null)
        {
            _source.Metadata = new Metadata(_source, exception.Message);
        }
        else
        {
            _source.Metadata!.DataObject = data;
        }
    }
}