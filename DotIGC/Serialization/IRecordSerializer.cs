namespace DotIGC.Serialization
{
    using DotIGC.Records;
    
    public interface IRecordSerializer
    {
        IRecord Deserialize(string text);
        
        string Serialize(IRecord record);
    }
}
