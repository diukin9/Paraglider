using FluentValidation;
using FluentValidation.Results;
using Paraglider.Infrastructure.Common.Response;
using static Paraglider.Infrastructure.Common.AppData;

namespace Paraglider.Infrastructure.Common;

/// <summary>
///     Generic operation result for any requests for Web API service and some MVC actions.
/// </summary>
[Serializable]
public class OperationResult<T>
{
    /// <summary>
    ///     Operation result metadata
    /// </summary>
    public Metadata<T>? Metadata { get; set; }

    /// <summary>
    ///     Exception that occurred during execution
    /// </summary>
    public Exception? Exception { get; set; }

    /// <summary>
    ///     Returns True when Exception is null and Metadata.Type != MetadataType.Error
    /// </summary>
    public virtual bool IsOk => Exception is null && Metadata?.Type != MetadataType.Error;

    public OperationResult<T> AddSuccess(string message, T? data = default)
    {
        Metadata = new Metadata<T>(message, data, MetadataType.Success);
        return this;
    }

    public OperationResult<T> AddWarning(string message, T? data = default)
    {
        Metadata = new Metadata<T>(message, data, MetadataType.Warning);
        return this;
    }

    public OperationResult<T> AddError(string message, Exception? exception = null, T? data = default)
    {
        Exception = exception;
        Metadata = new Metadata<T>(message, data, MetadataType.Error);
        return this;
    }

    public OperationResult<T> AddError(List<ValidationFailure> failures, T? data = default)
    {
        Exception = new ValidationException(failures);
        Metadata = new Metadata<T>(ExceptionMessages.ValidationError, data, MetadataType.Error);
        return this;
    }
}

public class OperationResult : OperationResult<None>
{
    public OperationResult AddSuccess(string message)
    {
        Metadata = new Metadata<None>(message, default, MetadataType.Success);
        return this;
    }

    public OperationResult AddWarning(string message)
    {
        Metadata = new Metadata<None>(message, default, MetadataType.Warning);
        return this;
    }

    public OperationResult AddError(string message, Exception? exception = null)
    {
        Exception = exception;
        Metadata = new Metadata<None>(message, default, MetadataType.Error);
        return this;
    }

    public OperationResult AddError(List<ValidationFailure> failures)
    {
        Exception = new ValidationException(failures);
        Metadata = new Metadata<None>(ExceptionMessages.ValidationError, default, MetadataType.Error);
        return this;
    }
}

public class None
{

}
