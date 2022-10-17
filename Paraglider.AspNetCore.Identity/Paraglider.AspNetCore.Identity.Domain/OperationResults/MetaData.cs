namespace Paraglider.AspNetCore.Identity.Domain
{
    /// <summary>
    /// Metadata object base for all type  <see cref="IMetadataMessage"/>
    /// </summary>
    [Serializable]
    public class Metadata : IMetadataMessage
    {
        private readonly BaseOperationResult _source;

        public Metadata()
        {
            Type = MetadataType.Info;
        }

        public Metadata(BaseOperationResult source, string message) : this()
        {
            _source = source;
            Message = message;
        }

        public Metadata(BaseOperationResult source, string message, MetadataType type = MetadataType.Info)
        {
            Type = type;
            _source = source;
            Message = message;
        }

        public string Message { get; }

        public MetadataType Type { get; }

        public object DataObject { get; private set; }

        /// <summary>
        /// Add special type of metadata
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
}