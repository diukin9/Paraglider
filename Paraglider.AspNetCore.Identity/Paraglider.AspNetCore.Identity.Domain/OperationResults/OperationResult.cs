namespace Paraglider.AspNetCore.Identity.Domain
{
    /// <summary>
    /// Generic operation result for any requests for Web API service and some MVC actions.
    /// </summary>
    [Serializable]
    public class OperationResult : BaseOperationResult
    {
        /// <summary>
        /// Returns True when Exception == null and Metadata.Type != MetadataType.Error
        /// </summary>
        public virtual bool IsOk => Exception == null && Metadata?.Type != MetadataType.Error;

        /// <summary>
        /// Returns as factory method OperationResult
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        public static OperationResult CreateResult(Exception? exception = null)
        {
            var operation = new OperationResult
            {
                Exception = exception
            };
            return operation;
        }

        /// <summary>
        /// Returns as factory method OperationResult
        /// </summary>
        /// <returns></returns>
        public static OperationResult CreateResult()
        {
            return CreateResult(null);
        }
    }

    /// <summary>
    /// Generic operation result with return response for any requests for Web API service and some MVC actions.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class OperationResult<T> : OperationResult
    {
        /// <summary>
        /// Result for server operation
        /// </summary>
        public T? Result { get; set; }

        /// <summary>
        /// Returns True when Exception == null and Metadata.Type != MetadataType.Error and Result != null
        /// </summary>
        public override bool IsOk => Exception == null && Metadata?.Type != MetadataType.Error && Result != null;

        /// <summary>
        /// Returns as factory method OperationResult with return response 
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="result"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        public static OperationResult<TResult> CreateResult<TResult>(TResult result, Exception? exception = null)
        {
            var operation = new OperationResult<TResult>
            {
                Result = result,
                Exception = exception
            };
            return operation;
        }

        /// <summary>
        /// Returns as factory method OperationResult
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <returns></returns>
        public static OperationResult<TResult> CreateResult<TResult>()
        {
            return CreateResult(default(TResult)!);
        }
    }
}
