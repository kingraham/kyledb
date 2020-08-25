using Common;
using System;
using System.IO;

namespace KyleDB.Core.Abstraction.Serialization
{
    public interface IDeserializationHandler
    {
        long HandleBigInt(BinaryReader reader);

        string HandleChar(BinaryReader reader, int length);

        DateTime HandleDate(BinaryReader reader);

        DateTime HandleDateTime(BinaryReader reader);

        BigDecimal HandleDecimal(BinaryReader reader, int length, int exponent);

        int HandleInt(BinaryReader reader);

        short HandleSmallInt(BinaryReader reader);

        byte HandleTinyInt(BinaryReader reader);
    }
}