using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Net;

namespace AdvancedScada.IODriverV2.Comm
{
    public class Conversion
    {
        public static byte[] HexToBytes(string hex)
        {
            if (hex == null)
                throw new ArgumentNullException("The data is null");

            if (hex.Length % 2 != 0)
                throw new FormatException("Hex Character Count Not Even");

            var bytes = new byte[hex.Length / 2];

            for (var i = 0; i < bytes.Length; i++)
                bytes[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);

            return bytes;
        }
        /// <summary>
        /// Converts a network order byte array to an array of UInt16 values in host order
        /// </summary>
        /// <param name="networkBytes">The network order byte array</param>
        /// <returns>The host order ushort array</returns>
        public static ushort[] NetworkBytesToHostUInt16(byte[] networkBytes)
        {
            if (networkBytes == null)
                throw new ArgumentNullException("networkBytes");

            if (networkBytes.Length % 2 != 0)
                throw new FormatException("NetworkBytesNotEven");

            ushort[] result = new ushort[networkBytes.Length / 2];

            for (int i = 0; i < result.Length; i++)
                result[i] = (ushort)IPAddress.NetworkToHostOrder(BitConverter.ToInt16(networkBytes, i * 2));

            return result;
        }
        public static void SwapBytes(ref byte[] b, int index)
        {
            if (b.Length > index + 1)
            {
                var bt = b[index];
                b[index] = b[index + 1];
                b[index + 1] = bt;
            }
        }

        public static void SwapWords(ref byte[] b, int index)
        {
            if (b.Length > index + 3)
            {
                byte bt1 = 0;
                byte bt2 = 0;
                bt1 = b[index];
                bt2 = b[index + 1];

                b[index] = b[index + 2];
                b[index + 1] = b[index + 3];

                b[index + 2] = bt1;
                b[index + 3] = bt2;
            }
        }

        public static object DynamicConverter(string value, Type t)
        {
            if (t == typeof(bool))
            {
                var boolValue = false;
                if (bool.TryParse(value, out boolValue)) return boolValue;

                var intValue = 0;
                if (int.TryParse(value, out intValue))
                    return Convert.ChangeType(intValue, t);
                throw new Exception("Invalid Conversion of " + value);
            }

            return Convert.ChangeType(value, t);
        }

        public static string[] ExtractData(byte[] rawData, int startByte, int NumberOfElements, int BitsPerElement,
            bool swapBytes, bool swapWords)
        {
            var ResultingValues = new string[NumberOfElements];
            var ResultingValuesIndex = 0;

            var BytesPerElement = Convert.ToInt32(Math.Ceiling(16 / 8.0));

            //* Loop through extracting each value AND avoid exceeding the number of bytes in the RawData
            while (ResultingValuesIndex < NumberOfElements &&
                   startByte + Math.Ceiling(ResultingValuesIndex * (BitsPerElement / 8.0)) + BytesPerElement - 1 <=
                   rawData.Length)
            {
                //* Bit or byte read?
                if (BitsPerElement > 1)
                {
                    //Dim Result As Integer = 0
                    var ValueDataBytes = new byte[Convert.ToInt32(BitsPerElement / 8.0)];
                    //* Ensure there is enought data to process
                    if (rawData.Length >= startByte + ResultingValuesIndex * BytesPerElement + BytesPerElement)
                    {
                        for (var i = 0; i < BytesPerElement; i++)
                            ValueDataBytes[i] = rawData[startByte + ResultingValuesIndex * BytesPerElement + i];
                        //DWored

                        if (BitsPerElement == 64)
                        {
                            Array.Reverse(ValueDataBytes);
                            ResultingValues[ResultingValuesIndex] =
                                Convert.ToString(BitConverter.ToInt64(ValueDataBytes, 0));
                        }
                        else
                        {
                            // Wored
                            if (swapBytes) SwapBytes(ref ValueDataBytes, 0);
                            ResultingValues[ResultingValuesIndex] =
                                Convert.ToString(BitConverter.ToInt16(ValueDataBytes, 0));
                        }
                    }
                }
                else
                {
                    //* It is a bit read, so extract from the byte
                    //* Byte 0 Bit 0 is always the first bit specified in the address
                    var ByteNumber = Convert.ToInt32(Math.Floor(ResultingValuesIndex / 8.0));
                    var BitNumber = Convert.ToInt32((ResultingValuesIndex / 8.0 - ByteNumber) * 8);
                    ResultingValues[ResultingValuesIndex] =
                        Convert.ToString((rawData[ByteNumber + startByte] & Convert.ToByte(Math.Pow(2, BitNumber))) >
                                         0);
                }

                ResultingValuesIndex += 1;
            }

            return ResultingValues;
        }
    }
    
}
