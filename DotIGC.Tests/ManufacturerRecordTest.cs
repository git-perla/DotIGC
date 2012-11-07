namespace DotIGC.Tests
{
    using DotIGC.Records;
    using DotIGC.Serialization;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    
    [TestClass]
    public class ManufacturerRecordTest
    {
        [TestMethod]
        public void Can_parse_record_with_variable_flight_record_id_and_optional_text_string()
        {
            var recordText = "ALXNGIIFLIGHT:1";
            var parser = new ManufacturerSerializer();
            var record = parser.Deserialize(recordText) as ManufacturerRecord;
            Assert.IsTrue(record.Manufacturer == "LXN");
            Assert.IsTrue(record.Id == "GIIFLIGHT");
            Assert.IsTrue(record.AdditionalData == "1");
        }
    }
}
