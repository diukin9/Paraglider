namespace Paraglider.AspNetCore.Identity.Infrastructure.Responses.OperationResult.Abstractions;

/// <summary>
///     Any action result
/// </summary>
[Serializable]
public abstract class BaseOperationResult
{
    /// <summary>
    ///     Operation result metadata
    /// </summary>
    public Metadata? Metadata { get; set; }

    /// <summary>
    ///     Exception that occurred during execution
    /// </summary>
    public Exception? Exception { get; set; }
}