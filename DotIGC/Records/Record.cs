namespace DotIGC.Records
{
    /// <summary>
    /// Represents a record in a IGC data file.
    /// </summary>
    public class Record
    {
        public Record(RecordType recordType, ThreeLetterCode tlc, string text)
        {
            RecordType = recordType;
            ThreeLetterCode = tlc;
            Text = text;
        }

        public Record(RecordType recordType, string text) : this(recordType, ThreeLetterCode.Unkown, text) { }

        public Record(RecordType recordType) : this(recordType, string.Empty) { }
        
        /// <summary>
        /// Gets the <see cref="RecordType"/>.
        /// </summary>
        public RecordType RecordType { get; private set; }

        /// <summary>
        /// Gets the three letter code.
        /// </summary>
        public ThreeLetterCode ThreeLetterCode { get; private set; }

        /// <summary>
        /// Gets the unformated text string from the IGC data file.
        /// </summary>
        public string Text { get; private set; }

        public override string ToString()
        {
            return Text;
        }
    }

    public class Record<TValue> : Record
    {
        public Record(RecordType recordType, ThreeLetterCode tlc, string text, TValue value) : base(recordType, tlc, text)
        {
            Value = value;
        }

        public TValue Value { get; private set; }

        public override string ToString()
        {
            return string.Format("RecordType: {0} TLC: {1} Value: {2}", RecordType, ThreeLetterCode, Value);
        }
    }
}
