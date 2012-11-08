namespace DotIGC.Serialization
{
    using DotIGC.Records;
    using System;
    
    public class HeaderSerializer : IRecordSerializer
    {
        RecordSerializerContainer<ThreeLetterCode> serializers;

        public HeaderSerializer(RecordSerializerContainer<ThreeLetterCode> serializers)
        {
            if (serializers == null)
                throw new ArgumentNullException("serializers");

            this.serializers = serializers;
        }
        
        public Record Deserialize(SerializationContext context)
        {
            if (context == null)
                throw new ArgumentException("context");

            if (context.RecordType != RecordType.H)
                throw new ArgumentException("Wrong record type");

            var source = context.Text[1] == 'F' ? DataSource.FlightRecorder : DataSource.Other;
            var tlc = (ThreeLetterCode)Enum.Parse(typeof(ThreeLetterCode), context.Text.Substring(2, 3));
            return this.serializers[tlc].Deserialize(new SerializationContext(context.RecordType, tlc, context.Text));
        }

        public string Serialize(Record record)
        {
            throw new System.NotImplementedException();
        }
    }
}
