namespace DotIGC.Serialization
{
    using DotIGC.Records;
    using DotIGC.Serialization;
    using System;
    using System.Collections.Generic;
    
    public class RecordSerializerFactory
    {
        Dictionary<RecordType, IRecordSerializer> serializers = new Dictionary<RecordType, IRecordSerializer>();

        public IRecordSerializer this[RecordType recordType]
        {
            get 
            {
                return this.serializers[recordType];
            }

            set 
            {
                this.serializers[recordType] = value;
            }
        }

        public RecordSerializerBinder Bind(RecordType recordType)
        {
            return new RecordSerializerBinder(this, recordType);
        }

        public static RecordSerializerFactory Default
        {
            get 
            {
                var factory = new RecordSerializerFactory();
                factory.Bind(RecordType.A).To<ManufacturerSerializer>();
                factory.Bind(RecordType.H).To<HeaderSerializer>();
                return factory;
            }
        }

        public bool TryGetSerializer(RecordType recordType, out IRecordSerializer serializer)
        {
            return serializers.TryGetValue(recordType, out serializer);
        }
    }

    public class RecordSerializerBinder
    {
        RecordSerializerFactory factory;
        RecordType recordType;
        
        public RecordSerializerBinder(RecordSerializerFactory factory, RecordType recordType)
        {
            this.factory = factory;
            this.recordType = recordType;
        }

        public void To<T>() where T : IRecordSerializer
        {
            factory[recordType] = Activator.CreateInstance<T>();
        }
    }
}
