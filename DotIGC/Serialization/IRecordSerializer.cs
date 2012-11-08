namespace DotIGC.Serialization
{
    using DotIGC.Records;
    
    public interface IRecordSerializer
    {
        Record Deserialize(SerializationContext text);
        
        string Serialize(Record record);
    }
}
