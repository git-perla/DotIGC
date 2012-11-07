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
        RecordSerializerFactory serializers;
        bool disposed;

        public IgcTextReader(Stream stream, RecordSerializerFactory serializers)
        {
            this.stream = stream;
            this.reader = new StreamReader(stream);
            this.serializers = serializers;
        }

        public bool Read(out IRecord record)
        {
            record = null;

            if (!stream.CanRead || reader.EndOfStream)
                return false;

            var text = reader.ReadLine();
            var recordType = RecordTypeExtensions.Parse(text);

            IRecordSerializer serializer;
            if (!this.serializers.TryGetSerializer(recordType, out serializer))
                return true;
            
            record = serializer.Deserialize(text);
            
            return true;
        }

        public void Dispose()
        {
            if (disposed)
                return;

            this.reader.Dispose();
            this.reader = null;
            this.stream = null;
            this.serializers = null;
            
            disposed = true;
        }       
    }
}
