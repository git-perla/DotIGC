namespace DotIGC.Serialization
{
    using DotIGC.Records;
    using System;

    public class ManufacturerSerializer : IRecordSerializer
    {
        public Record Deserialize(SerializationContext context)
        {
            if (context == null)
                throw new ArgumentException("context");

            if (context.RecordType != RecordType.A)
                throw new ArgumentException("Wrong record type");
        
            var code = context.Text.Substring(1, 3);
            var substring = context.Text.Substring(4).Split(new[] { ':' });
            var id = substring[0];

            return new ManufacturerRecord(code, id, substring.Length > 0 ? substring[1] : string.Empty, RecordType.A);
        }

        public string Serialize(Record record)
        {
            throw new System.NotImplementedException();
        }
    }
}
