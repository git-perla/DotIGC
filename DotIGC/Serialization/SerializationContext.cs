namespace DotIGC.Serialization
{
    using DotIGC.Records;
    
    public class SerializationContext
    {
        public SerializationContext(RecordType recordType, ThreeLetterCode tlc, string text)
        {
            RecordType = recordType;
            ThreeLetterCode = tlc;
            Text = text;
        }
        
        public RecordType RecordType { get; private set; }

        public ThreeLetterCode ThreeLetterCode { get; private set; }

        public string Text { get; private set; }
    }
}
