namespace DotIGC.Serialization
{
    using DotIGC.Records;

    public class RecordSerializer : IRecordSerializer
    {
        public Record Deserialize(SerializationContext context)
        {
            var values = context.Text.Split(new[] { ':' });

            return values.Length > 1 ? 
                new Record<string>(context.RecordType, context.ThreeLetterCode, context.Text, values[1]) :
                new Record(context.RecordType, context.ThreeLetterCode, context.Text);
        }

        public string Serialize(Record record)
        {
            return record.ToString();
        }

        public static IRecordSerializer Default
        {
            get { return new RecordSerializer(); }
        }
    }
}
