using KyleDB.Core.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace KyleDB.Tests.Serialization
{
    [TestClass]
    public class DataTypeDeserializationHandlerTests
    {
        [TestMethod]
        public void ShouldHandleBigInt()
        {
            using (MemoryStream ms = new MemoryStream(new byte[] { 0x9, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0 }))
            using (BinaryReader read = new BinaryReader(ms))
                Assert.AreEqual(9, DataTypeDeserializationHandler.HandleBigInt(read));
        }

        [TestMethod]
        public void ShouldHandleInt()
        {
            using (MemoryStream ms = new MemoryStream(new byte[] { 0x4, 0x0, 0x0, 0x0 }))
            using (BinaryReader read = new BinaryReader(ms))
                Assert.AreEqual(4, DataTypeDeserializationHandler.HandleInt(read));
        }

        [TestMethod]
        public void ShouldHandleSmallInt()
        {
            using (MemoryStream ms = new MemoryStream(new byte[] { 0x5, 0x6 }))
            using (BinaryReader read = new BinaryReader(ms))
                Assert.AreEqual(1541, DataTypeDeserializationHandler.HandleSmallInt(read));
        }

        [TestMethod]
        public void ShouldHandleTinyInt()
        {
            using (MemoryStream ms = new MemoryStream(new byte[] { 0x3 }))
            using (BinaryReader read = new BinaryReader(ms))
                Assert.AreEqual(3, DataTypeDeserializationHandler.HandleTinyInt(read));
        }
    }
}