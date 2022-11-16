namespace Paraglider.Domain.Enums
{
    public enum IntervalType
    {
        /// <summary>
        /// An interval has a beginning but no end
        /// </summary>
        From,

        /// <summary>
        /// The interval has a beginning and an end
        /// </summary>
        FromTo,

        /// <summary>
        /// An interval has an end, but no beginning
        /// </summary>
        To
    }
}
