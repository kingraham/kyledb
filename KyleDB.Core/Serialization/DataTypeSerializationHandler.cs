using Common;
using KyleDB.Core.Abstraction.Serialization;
using System;
using System.IO;

namespace KyleDB.Core.Serialization
{
    public class DataTypeSerializationHandler : ISerializationHandler
    {
        public void HandleBigInt(BinaryWriter writer, long value) => writer.Write(value);

        public void HandleChar(BinaryWriter writer, int length, string value)
        {
            char[] buf = value.ToCharArray();
            var bufDifference = length - buf.Length;
            if (bufDifference < 0)
                throw new InvalidOperationException($"String or binary data would be truncated"); // TODO: Synchronize error handling
            writer.Write(buf);
            for (int i = 0; i < bufDifference; i++)
                writer.Write(' ');
        }

        /// <summary>
        /// Date Handling is 3 bytes in SQL Server.
        ///
        /// Days since 01-01-0001
        /// </summary>
        public void HandleDate(BinaryWriter writer, DateTime value) => writer.Write(BitConverter.GetBytes((value - new DateTime(1, 1, 1)).Days), 0, 3);

        /// <summary>
        /// DateTime Handling is 8 bytes in SQL Server.
        ///
        /// Year/Month/Day    Range is 1/1/1753 - 12/31/9999, Days -- 3 bytes for days since 1/1/1753
        /// Hour / Month / Second - 1 byte each
        /// Fractional - 2 bytes
        /// </summary>
        public void HandleDateTime(BinaryWriter writer, DateTime value)
        {
            var days = BitConverter.GetBytes((value - new DateTime(1753, 1, 1)).Days);
            writer.Write(days, 0, 3);
            writer.Write((byte)value.Hour);
            writer.Write((byte)value.Minute);
            writer.Write((byte)value.Second);
            writer.Write((short)value.Millisecond);
        }

        /// <summary>
        /// This works on normalized BigRational. Convert to BigInteger and then save.
        /// In general, better space savings than SQL Server
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="scale"></param>
        /// <param name="value"></param>
        public void HandleDecimal(BinaryWriter writer, BigDecimal value) => writer.Write(value.Mantissa.ToByteArray());

        public void HandleInt(BinaryWriter writer, int value) => writer.Write(value);

        public void HandleSmallInt(BinaryWriter writer, short value) => writer.Write(value);

        public void HandleTinyInt(BinaryWriter writer, byte value) => writer.Write(value);
    }
}