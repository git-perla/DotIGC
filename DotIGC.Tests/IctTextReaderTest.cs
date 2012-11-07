namespace DotIGC.Tests
{
    using DotIGC.Records;
    using DotIGC.Serialization;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    
    [TestClass]
    public class IctTextReaderTest
    {
        [TestMethod]
        public void Can_read()
        {
            var path = "Data/Simple.igc";
            var records = new List<IRecord>();
            
            using (var stream = new FileStream(path, FileMode.Open))
            using (var reader = new IgcTextReader(stream, RecordSerializerFactory.Default))
            {
                IRecord record;
                while (reader.Read(out record))
                {
                    if (record != null)
                        records.Add(record);
                }
            }

            var manufacturerRecord = records.OfType<ManufacturerRecord>().FirstOrDefault();
            Assert.IsNotNull(manufacturerRecord);
            Assert.AreEqual(manufacturerRecord.Manufacturer, "XXX");
        }
    }
}
