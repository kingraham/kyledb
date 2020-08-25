using Common;
using KyleDB.Core.Abstraction.Serialization;
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
        public void ShouldHandleDate()
        {
            using (MemoryStream ms = new MemoryStream())
            using (BinaryWriter write = new BinaryWriter(ms))
            {
                GetHandler().HandleDate(write, new System.DateTime(9999, 12, 31));
                AssertStream(ms, new byte[] { 218, 185, 55 });
            }
        }

        [TestMethod]
        public void ShouldHandleDateTime()
        {
            using (MemoryStream ms = new MemoryStream())
            using (BinaryWriter write = new BinaryWriter(ms))
            {
                GetHandler().HandleDateTime(write, new System.DateTime(9999, 12, 31, 23, 59, 59, 999));
                AssertStream(ms, new byte[] { 57, 246, 45, 23, 59, 59, 231, 3 });
            }
        }

        [DataTestMethod]
        [DataRow("9.99", new byte[] { 231, 3 })] // scale (or exponent for BigDecimal) is embedded in the meta data, so the output should be the same regardless
        [DataRow("999", new byte[] { 231, 3 })]
        public void ShouldHandleDecimalNumeric(string value, byte[] expected)
        {
            using (MemoryStream ms = new MemoryStream())
            using (BinaryWriter write = new BinaryWriter(ms))
            {
                GetHandler().HandleDecimal(write, new BigDecimal(decimal.Parse(value)));
                AssertStream(ms, expected);
            }
        }

        [DataTestMethod]
        [DataRow("999999999999999999999999999999999999.99", new byte[] { 255, 255, 255, 255, 63, 34, 138, 9, 122, 196, 134, 90, 168, 76, 59, 75 })] // scale (or exponent for BigDecimal) is embedded in the meta data, so the output should be the same regardless
        [DataRow("99999999999999999999999999999999999999", new byte[] { 255, 255, 255, 255, 63, 34, 138, 9, 122, 196, 134, 90, 168, 76, 59, 75 })]
        public void ShouldHandleDecimalNumericLargeValues(string value, byte[] expected)
        {
            using (MemoryStream ms = new MemoryStream())
            using (BinaryWriter write = new BinaryWriter(ms))
            {
                GetHandler().HandleDecimal(write, new BigDecimal(value));
                AssertStream(ms, expected);
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

        private ISerializationHandler GetHandler() => new DataTypeSerializationHandler();
    }
}