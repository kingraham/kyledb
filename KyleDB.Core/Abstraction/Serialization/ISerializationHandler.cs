using Common;
using System;
using System.IO;

namespace KyleDB.Core.Abstraction.Serialization
{
    public interface ISerializationHandler
    {
        void HandleBigInt(BinaryWriter writer, long value);

        void HandleChar(BinaryWriter writer, int length, string value);

        void HandleDate(BinaryWriter writer, DateTime value);

        void HandleDateTime(BinaryWriter writer, DateTime value);

        void HandleDecimal(BinaryWriter writer, BigDecimal value);

        void HandleInt(BinaryWriter writer, int value);

        void HandleSmallInt(BinaryWriter writer, short value);

        void HandleTinyInt(BinaryWriter writer, byte value);
    }
}