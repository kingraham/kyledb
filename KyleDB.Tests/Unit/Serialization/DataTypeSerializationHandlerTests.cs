using KyleDB.Core.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;

namespace KyleDB.Tests.Serialization
{
    [TestClass]
    public class DataTypeSerializationHandlerTests
    {
        [TestMethod]
        public void ShouldHandleBigInt()
        {
            using (MemoryStream ms = new MemoryStream())
            using (BinaryWriter write = new BinaryWriter(ms))
            {
                GetHandler().HandleBigInt(write, 5);
                AssertStream(ms, new byte[] { 0x5, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0 });
            }
        }

        [TestMethod]
        public void ShouldHandleChar()
        {
            using (MemoryStream ms = new MemoryStream())
            using (BinaryWriter write = new BinaryWriter(ms))
            {
                GetHandler().HandleChar(write, 10, "hey");
                AssertStream(ms, new char[] { 'h', 'e', 'y', ' ', ' ', ' ', ' ', ' ', ' ', ' ' }.Select(c => (byte)c).ToArray());
            }
        }

        [TestMethod]
        public void ShouldHandleInt()
        {
            using (MemoryStream ms = new MemoryStream())
            using (BinaryWriter write = new BinaryWriter(ms))
            {
                GetHandler().HandleInt(write, 4);
                AssertStream(ms, new byte[] { 0x4, 0x0, 0x0, 0x0 });
            }
        }

        [TestMethod]
        public void ShouldHandleSmallInt()
        {
            using (MemoryStream ms = new MemoryStream())
            using (BinaryWriter write = new BinaryWriter(ms))
            {
                GetHandler().HandleSmallInt(write, 3);
                AssertStream(ms, new byte[] { 0x3, 0x0 });
            }
        }

        [TestMethod]
        public void ShouldHandleTinyInt()
        {
            using (MemoryStream ms = new MemoryStream())
            using (BinaryWriter write = new BinaryWriter(ms))
            {
                GetHandler().HandleTinyInt(write, 255);
                AssertStream(ms, new byte[] { 255 });
            }
        }

        [TestMethod]
        public void ShouldThrowException_OnHandleChar_WhenExceedsMaxLength()
        {
            using (MemoryStream ms = new MemoryStream())
            using (BinaryWriter write = new BinaryWriter(ms))
            {
                try
                {
                    GetHandler().HandleChar(write, 10, "thiswouldexceedmaxlength");
                    Assert.Fail("Exception was not thrown.");
                }
                catch
                {
                }
            }
        }

        private void AssertStream(MemoryStream ms, byte[] expected)
        {
            ms.Position = 0;
            var buffer = ms.ToArray();
            Assert.AreEqual(expected.Length, buffer.Length);
            for (int i = 0; i < expected.Length; i++)
                Assert.AreEqual(expected[i], buffer[i], $"Mismatch at byte {i}");
        }

        private DataTypeSerializationHandler GetHandler() => new DataTypeSerializationHandler();
    }
}