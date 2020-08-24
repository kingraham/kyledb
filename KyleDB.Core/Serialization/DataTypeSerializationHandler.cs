using KyleDB.Core.Abstraction.Serialization;
using System;
using System.IO;

namespace KyleDB.Core.Serialization
{    
    public class DataTypeSerializationHandler : ISerializationHandler
    {
        public void HandleBigInt(BinaryWriter writer, long value) => writer.Write(value);

        public void HandleChar(BinaryWriter writer, int length, string value) {
            char[] buf = value.ToCharArray();
            var bufDifference = length - buf.Length;
            if (bufDifference < 0)
                throw new InvalidOperationException($"String or binary data would be truncated"); // TODO: Synchronize error handling            
            writer.Write(buf);
            for (int i = 0; i < bufDifference; i++)
                writer.Write(' ');
        }        

        public void HandleInt(BinaryWriter writer, int value) => writer.Write(value);

        public void HandleSmallInt(BinaryWriter writer, short value) => writer.Write(value);

        public void HandleTinyInt(BinaryWriter writer, byte value) => writer.Write(value);
    }
}