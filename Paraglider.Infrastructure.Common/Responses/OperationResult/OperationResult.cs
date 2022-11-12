namespace Paraglider.Infrastructure.Responses.OperationResult;

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
    ///     Returns True when Exception == null and Metadata.Type != MetadataType.Error
    /// </summary>
    public virtual bool IsOk => Exception == null && Metadata?.Type != MetadataType.Error;
}

/// <summary>
///     Generic operation result with return response for any requests for Web API service and some MVC actions.
/// </summary>
/// <typeparam name="T"></typeparam>
[Serializable]
public class OperationResult<T> : OperationResult
{
    /// <summary>
    ///     Result for server operation
    /// </summary>
    public T? Result { get; set; }
}