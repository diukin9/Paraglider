using System.ComponentModel.DataAnnotations;
using static Paraglider.Infrastructure.Common.AppData;

namespace Paraglider.Infrastructure.Common.Response;

[Serializable]
public class InternalOperation<T>
{
    public Metadata<T>? Metadata { get; set; }

    public Exception? Exception { get; set; }

    public virtual bool IsOk => Exception is null && Metadata?.Type != MetadataType.Error;

    public InternalOperation<T> AddSuccess(T? data = default)
    {
        Metadata = new Metadata<T>(string.Empty, data, MetadataType.Success);
        return this;
    }

    public InternalOperation<T> AddError(string message, Exception? exception = null, T? data = default)
    {
        Exception = exception;
        Metadata = new Metadata<T>(message, data, MetadataType.Error);
        return this;
    }

    public InternalOperation<T> AddError(ValidationResult validation)
    {
        Exception = new ValidationException("Validation error");
        Metadata = new Metadata<T>(validation.ErrorMessage!, MetadataType.Error);
        return this;
    }

    public T? Content => Metadata!.DataObject;
}

public class InternalOperation : InternalOperation<None>
{
    public InternalOperation AddSuccess()
    {
        Metadata = new Metadata<None>(string.Empty, default, MetadataType.Success);
        return this;
    }

    public InternalOperation AddError(string message, Exception? exception = null)
    {
        Exception = exception;
        Metadata = new Metadata<None>(message, default, MetadataType.Error);
        return this;
    }

    public new InternalOperation AddError(ValidationResult validation)
    {
        Exception = new ValidationException("Validation error");
        Metadata = new Metadata<None>(validation.ErrorMessage!, MetadataType.Error);
        return this;
    }
}

[Serializable]
public class Metadata<T>
{
    public string? Message { get; }

    public MetadataType Type { get; }

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

public enum MetadataType
{
    Success,
    Error
}

public class None
{

}
