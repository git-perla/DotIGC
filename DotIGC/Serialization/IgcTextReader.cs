namespace DotIGC.Serialization
{
    using DotIGC.Records;
    using System;
    using System.Collections.Generic;
    using System.IO;
    
    public class IgcTextReader : IDisposable
    {
        Stream stream;
        StreamReader reader;
        RecordSerializerContainer<RecordType> serializers;
        bool disposed;

        public IgcTextReader(Stream stream, RecordSerializerContainer<RecordType> serializers)
        {
            this.stream = stream;
            this.reader = new StreamReader(stream);
            this.serializers = serializers;
        }

        public bool Read(out Record record)
        {
            record = null;

            if (!stream.CanRead || reader.EndOfStream)
                return false;

            var text = reader.ReadLine();
            var recordType = RecordTypeExtensions.Parse(text);
            
            record = this.serializers[recordType].Deserialize(new SerializationContext(recordType, ThreeLetterCode.Unkown, text));
            return true;
        }

        public void Dispose()
        {
            if (disposed)
                return;

            this.reader.Dispose();
            this.reader = null;
            this.stream = null;            
            
            disposed = true;
        }       
    }
}
