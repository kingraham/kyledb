using System.IO;

namespace KyleDB.Core.Serialization
{
    public class DataTypeDeserializationHandler
    {
        public static int HandleInt(BinaryReader reader) => reader.ReadInt32();
        public static byte HandleTinyInt(BinaryReader reader) => reader.ReadByte();
        public static short HandleSmallInt(BinaryReader reader) => reader.ReadInt16();
        public static long HandleBigInt(BinaryReader reader) => reader.ReadInt64();
    }
}