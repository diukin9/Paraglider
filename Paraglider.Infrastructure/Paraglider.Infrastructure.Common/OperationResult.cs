namespace Paraglider.Infrastructure.Common;

/// <summary>
///     Generic operation result for any requests for Web API service and some MVC actions.
/// </summary>
[Serializable]
public class OperationResult
{
    /// <summary>
    ///     Operation result metadata
    /// </summary>
    public Metadata? Metadata { get; set; }

    /// <summary>
    ///     Exception that occurred during execution
    /// </summary>
    public Exception? Exception { get; set; }

    /// <summary>
    ///     Returns True when Exception is null and Metadata.Type != MetadataType.Error
    /// </summary>
    public virtual bool IsOk => Exception is null && Metadata?.Type != MetadataType.Error;
}