namespace DotIGC.Records
{
    /// <summary>
    /// Represents a record in a IGC data file.
    /// </summary>
    public class Record : IRecord
    {
        public Record(RecordType recordType)
        {
            RecordType = recordType;
        }
        
        /// <summary>
        /// Gets the <see cref="RecordType"/>.
        /// </summary>
        public RecordType RecordType { get; private set; }
    }
}
