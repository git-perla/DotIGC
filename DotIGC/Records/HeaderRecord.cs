namespace DotIGC.Records
{
    /// <summary>
    /// Represents a header record in a IGC data file.
    /// </summary>
    public class HeaderRecord : Record
    {
        public HeaderRecord(DataSource source, ThreeLetterCode code, string value) : base(RecordType.H, code, value)
        {
            DataSource = source;
        }

        public DataSource DataSource { get; private set; }

        public override string ToString()
        {
            return string.Format("{0} {1} {2}", RecordType, ThreeLetterCode, Text); 
        }
    }
}
