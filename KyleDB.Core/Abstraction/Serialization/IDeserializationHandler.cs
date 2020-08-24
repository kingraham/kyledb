using System.IO;

namespace KyleDB.Core.Abstraction.Serialization
{
    public interface IDeserializationHandler
    {
        long HandleBigInt(BinaryReader reader);

        string HandleChar(BinaryReader reader, int length);

        int HandleInt(BinaryReader reader);

        short HandleSmallInt(BinaryReader reader);

        byte HandleTinyInt(BinaryReader reader);
    }
}