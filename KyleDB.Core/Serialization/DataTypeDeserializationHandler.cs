using KyleDB.Core.Abstraction.Serialization;
using System.IO;
using System.Text;

namespace KyleDB.Core.Serialization
{
    public class DataTypeDeserializationHandler : IDeserializationHandler
    {
        public long HandleBigInt(BinaryReader reader) => reader.ReadInt64();

        public string HandleChar(BinaryReader reader, int length) => new string(reader.ReadChars(length));

        public int HandleInt(BinaryReader reader) => reader.ReadInt32();

        public short HandleSmallInt(BinaryReader reader) => reader.ReadInt16();

        public byte HandleTinyInt(BinaryReader reader) => reader.ReadByte();
    }
}