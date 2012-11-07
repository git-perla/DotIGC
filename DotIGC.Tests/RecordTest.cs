
namespace DotIGC.Tests
{
    using DotIGC.Records;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    
    [TestClass]
    public class RecordTest
    {
        [TestMethod]
        public void New_instance_succeeds()
        {
            var record = new Record(RecordType.A);
            Assert.IsTrue(record.RecordType == RecordType.A);
        }
    }
}
