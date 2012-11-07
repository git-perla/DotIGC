namespace DotIGC.Serialization
{
    using DotIGC.Records;
    using System;

    public class ManufacturerSerializer : IRecordSerializer
    {
        public IRecord Deserialize(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                throw new ArgumentException("text");

            if (text[0] != 'A')
                throw new ArgumentException("Record is of type A");
        
            var code = text.Substring(1, 3);
            var substring = text.Substring(4).Split(new[] { ':' });
            var id = substring[0];

            return new ManufacturerRecord(code, id, substring.Length > 0 ? substring[1] : string.Empty, RecordType.A);
        }

        public string Serialize(IRecord record)
        {
            throw new System.NotImplementedException();
        }
    }
}
