using KyleDB.Core.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace KyleDB.Tests.Serialization
{
    [TestClass]
    public class DataTypeSerializationHandlerTests
    {
        private void AssertStream(MemoryStream ms, byte[] expected)
        {
            ms.Position = 0;
            var buffer = ms.ToArray();
            Assert.AreEqual(expected.Length, buffer.Length);
            for (int i = 0; i < expected.Length; i++)
                Assert.AreEqual(expected[i], buffer[i], $"Mismatch at byte {i}");
        }

        [TestMethod]
        public void ShouldHandleBigInt()
        {
            using (MemoryStream ms = new MemoryStream())
            using (BinaryWriter write = new BinaryWriter(ms))
            {
                DataTypeSerializationHandler.HandleBigInt(write, 5);
                AssertStream(ms, new byte[] { 0x5, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0 });
            }
        }

        [TestMethod]
        public void ShouldHandleInt()
        {
            using (MemoryStream ms = new MemoryStream())
            using (BinaryWriter write = new BinaryWriter(ms))
            {
                DataTypeSerializationHandler.HandleInt(write, 4);
                AssertStream(ms, new byte[] { 0x4, 0x0, 0x0, 0x0 });
            }
        }

        [TestMethod]
        public void ShouldHandleSmallInt()
        {
            using (MemoryStream ms = new MemoryStream())
            using (BinaryWriter write = new BinaryWriter(ms))
            {
                DataTypeSerializationHandler.HandleSmallInt(write, 3);
                AssertStream(ms, new byte[] { 0x3, 0x0 });
            }
        }

        [TestMethod]
        public void ShouldHandleTinyInt()
        {
            using (MemoryStream ms = new MemoryStream())
            using (BinaryWriter write = new BinaryWriter(ms))
            {
                DataTypeSerializationHandler.HandleTinyInt(write, 255);
                AssertStream(ms, new byte[] { 255 });
            }
        }
    }
}