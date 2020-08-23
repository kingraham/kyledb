using System.IO;

namespace KyleDB.Core.Serialization
{
    public class DataTypeSerializationHandler
    {
        public static void HandleBigInt(BinaryWriter writer, long value) => writer.Write(value);

        public static void HandleInt(BinaryWriter writer, int value) => writer.Write(value);

        public static void HandleSmallInt(BinaryWriter writer, short value) => writer.Write(value);

        public static void HandleTinyInt(BinaryWriter writer, byte value) => writer.Write(value);
    }
}