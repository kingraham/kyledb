using KyleDB.Core.Abstraction.Serialization;
using System;
using System.IO;

namespace KyleDB.Core.Serialization
{
    public class DataTypeDeserializationHandler : IDeserializationHandler
    {
        public long HandleBigInt(BinaryReader reader) => reader.ReadInt64();

        public string HandleChar(BinaryReader reader, int length) => new string(reader.ReadChars(length));

        public DateTime HandleDate(BinaryReader reader)
        {
            byte[] buf = new byte[4];
            buf[0] = reader.ReadByte();
            buf[1] = reader.ReadByte();
            buf[2] = reader.ReadByte();
            return new DateTime(1, 1, 1).AddDays(BitConverter.ToInt32(buf, 0));
        }

        /// <summary>
        /// DateTime Handling is 8 bytes in SQL Server.
        ///
        /// Year/Month/Day    Range is 1/1/1753 - 12/31/9999, Days -- 3 bytes for days since 1/1/1753
        /// Hour / Month / Second - 1 byte each
        /// Fractional - 2 bytes
        /// </summary>
        public DateTime HandleDateTime(BinaryReader reader)
        {
            byte[] buf = new byte[4];
            buf[0] = reader.ReadByte();
            buf[1] = reader.ReadByte();
            buf[2] = reader.ReadByte();
            return new DateTime(1753, 1, 1)
                .AddDays(BitConverter.ToInt32(buf, 0))
                .AddHours(reader.ReadByte())
                .AddMinutes(reader.ReadByte())
                .AddSeconds(reader.ReadByte())
                .AddMilliseconds(reader.ReadInt16());
        }

        public int HandleInt(BinaryReader reader) => reader.ReadInt32();

        public short HandleSmallInt(BinaryReader reader) => reader.ReadInt16();

        public byte HandleTinyInt(BinaryReader reader) => reader.ReadByte();
    }
}