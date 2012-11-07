namespace DotIGC.Records
{
    /// <summary>
    /// Represents a record in a IGC data file.
    /// </summary>
    public interface IRecord
    {
        /// <summary>
        /// Gets the <see cref="RecordType"/>.
        /// </summary>
        RecordType RecordType
        {
            get;
        }
    }
}
