namespace Paraglider.Infrastructure.Common.Extensions;

public static class OperationResultExtensions
{
    public static OperationResult AddInfo(this OperationResult source, string message, object? data = null)
    {
        source.Metadata = new Metadata(source, message, data);
        return source;
    }

    public static OperationResult AddSuccess(this OperationResult source, string message, object? data = null)
    {
        source.Metadata = new Metadata(source, message, data, MetadataType.Success);
        return source;
    }

    public static OperationResult AddWarning(this OperationResult source, string message, object? data = null)
    {
        source.Metadata = new Metadata(source, message, data, MetadataType.Warning);
        return source;
    }

    public static OperationResult AddError(this OperationResult source, string message, Exception? exception = null, object? data = null)
    {
        source.Exception = exception;
        source.Metadata = new Metadata(source, message, data, MetadataType.Error);
        return source;
    }

    public static string? GetMessage(this OperationResult source) => source?.Metadata?.Message;

    public static object? GetDataObject(this OperationResult source) => source?.Metadata?.DataObject;
}