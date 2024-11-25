using System.Text;

namespace Nsp.Framework.Core;

public class Base32Utils
{
    public static string Encode(byte[] data)
    {
        var result = new StringBuilder();
        var bitIndex = 0;
        var bitValue = 0;

        foreach (var b in data)
        {
            bitValue = (bitValue << 8) | b;
            bitIndex += 8;

            while (bitIndex >= 5)
            {
                var value = (bitValue >> (bitIndex - 5)) & 0x1F;
                result.Append(StringConst.Base32Characters[value]);
                bitIndex -= 5;
            }
        }

        if (bitIndex > 0)
        {
            var value = (bitValue << (5 - bitIndex)) & 0x1F;
            result.Append(StringConst.Base32Characters[value]);
        }

        return result.ToString();
    }


    public static byte[] Decode(string encoded)
    {
        var result = new List<byte>();
        var bitIndex = 0;
        var bitValue = 0;

        foreach (var c in encoded)
        {
            var value = StringConst.Base32Characters.IndexOf(c);
            if (value == -1)
            {
                throw new ArgumentException("Invalid Base32 character: " + c);
            }

            bitValue = (bitValue << 5) | value;
            bitIndex += 5;

            while (bitIndex >= 8)
            {
                result.Add((byte) ((bitValue >> (bitIndex - 8)) & 0xFF));
                bitIndex -= 8;
            }
        }

        if (bitIndex > 0)
        {
            result.Add((byte) ((bitValue << (8 - bitIndex)) & 0xFF));
        }

        return result.ToArray();
    }
}