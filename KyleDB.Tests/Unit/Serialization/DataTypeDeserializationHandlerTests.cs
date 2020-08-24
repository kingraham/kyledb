﻿using KyleDB.Core.Abstraction.Serialization;
using KyleDB.Core.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;

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
                Assert.AreEqual(9, GetHandler().HandleBigInt(read));
        }

        [DataTestMethod]
        [DataRow(new char[] { 'h', 'e', 'l', 'l', 'o', ' ', ' ', ' ' }, 8, "hello   ")]
        [DataRow(new char[] { 'h', 'i', }, 2, "hi")]

        public void ShouldHandleChar(char[] buffer, int length, string expected)
        {
            using (MemoryStream ms = new MemoryStream(buffer.Select(c => (byte)c).ToArray()))
            using (BinaryReader read = new BinaryReader(ms))
                Assert.AreEqual(expected, GetHandler().HandleChar(read, length));
        }

        [TestMethod]
        public void ShouldHandleDate()
        {
            using (MemoryStream ms = new MemoryStream(new byte[] { 217, 185, 55 }))
            using (BinaryReader read = new BinaryReader(ms))
                Assert.AreEqual(new System.DateTime(9999, 12, 30), GetHandler().HandleDate(read));
        }

        [TestMethod]
        public void ShouldHandleDateTime()
        {
            using (MemoryStream ms = new MemoryStream(new byte[] { 57, 246, 45, 23, 58, 59, 231, 3 }))
            using (BinaryReader read = new BinaryReader(ms))
                Assert.AreEqual(new System.DateTime(9999, 12, 31, 23, 58, 59, 999), GetHandler().HandleDateTime(read));            
        }

        [TestMethod]
        public void ShouldHandleInt()
        {
            using (MemoryStream ms = new MemoryStream(new byte[] { 0x4, 0x0, 0x0, 0x0 }))
            using (BinaryReader read = new BinaryReader(ms))
                Assert.AreEqual(4, GetHandler().HandleInt(read));
        }

        [TestMethod]
        public void ShouldHandleSmallInt()
        {
            using (MemoryStream ms = new MemoryStream(new byte[] { 0x5, 0x6 }))
            using (BinaryReader read = new BinaryReader(ms))
                Assert.AreEqual(1541, GetHandler().HandleSmallInt(read));
        }

        [TestMethod]
        public void ShouldHandleTinyInt()
        {
            using (MemoryStream ms = new MemoryStream(new byte[] { 0x3 }))
            using (BinaryReader read = new BinaryReader(ms))
                Assert.AreEqual(3, GetHandler().HandleTinyInt(read));
        }

        private IDeserializationHandler GetHandler() => new DataTypeDeserializationHandler();
    }
}