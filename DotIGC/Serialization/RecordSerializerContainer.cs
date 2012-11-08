namespace DotIGC.Serialization
{
    using DotIGC.Records;
    using System;
    using System.Collections.Generic;

    public static class RecordSerializerContainer
    {
        public static RecordSerializerContainer<RecordType> Default
        {
            get 
            {
                var container = new RecordSerializerContainer<RecordType>(RecordSerializer.Default);
                container.Bind(RecordType.A).To<ManufacturerSerializer>();

                var subrecordSerializers = new RecordSerializerContainer<ThreeLetterCode>(RecordSerializer.Default);
                container.Bind(RecordType.H).ToConstant(new HeaderSerializer(subrecordSerializers));
                return container;
            }
        }
    }
    
    public class RecordSerializerContainer<TKey>
    {
        Dictionary<TKey, IRecordSerializer> serializers = new Dictionary<TKey, IRecordSerializer>();
        IRecordSerializer defaultSerializer;

        public RecordSerializerContainer(IRecordSerializer defaultSerializer)
        {
            if (defaultSerializer == null)
                throw new ArgumentNullException("defaultSerializer");

            this.defaultSerializer = defaultSerializer;
        }
        
        public IRecordSerializer this[TKey key]
        {
            get 
            {
                IRecordSerializer serializer;
                if (this.serializers.TryGetValue(key, out serializer))
                    return serializer;

                return defaultSerializer;
            }
        }

        public IRecordSerializerBinder Bind(TKey key)
        {
            return new Binder<TKey>(key, this);
        }

        internal void Register(TKey key, IRecordSerializer serializer)
        {
            this.serializers[key] = serializer;   
        }
    }

    public interface IRecordSerializerBinder
    {
        void To<T>() where T : IRecordSerializer;
        void ToConstant<T>(T constsant) where T : IRecordSerializer;
    }

    internal class Binder<TKey> : IRecordSerializerBinder
    {
        TKey key;
        RecordSerializerContainer<TKey> container;

        public Binder(TKey key, RecordSerializerContainer<TKey> container)
        {
            this.key = key;
            this.container = container;
        }
        
        public void To<T>() where T : IRecordSerializer
        {
            this.container.Register(key, Activator.CreateInstance<T>());
        }

        public void ToConstant<T>(T constsant) where T : IRecordSerializer
        {
            this.container.Register(key, constsant);
        }
    }
}
