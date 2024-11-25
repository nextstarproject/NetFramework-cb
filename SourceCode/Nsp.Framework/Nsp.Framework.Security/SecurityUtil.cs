using System.Text;

namespace Nsp.Framework.Security;

public static class SecurityUtil
{
    /// <summary>
    /// Bytes 转 Hex 字符串
    /// </summary>
    /// <param name="bytes"></param>
    /// <returns></returns>
    public static string BytesToHexString(byte[] bytes)
    {
        return BitConverter.ToString(bytes).Replace("-", "").ToLowerInvariant();
    }

    /// <summary>
    /// Hex 转 Bytes 字符串
    /// </summary>
    /// <param name="hex"></param>
    /// <returns></returns>
    public static byte[] HexStringToByte(string hex)
    {
        var length = hex.Length;
        var bytes = new byte[length / 2];

        for (var i = 0; i < length; i += 2)
        {
            // 将每两个十六进制字符转换为一个字节
            bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
        }

        return bytes;
    }

    public static byte[] GetBytes(string input, int length)
    {
        using var sha256 = SHA256.Create();
        var hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));

        if (hash.Length >= length) return hash[..length];

        var result = new byte[length];
        Buffer.BlockCopy(hash, 0, result, 0, hash.Length);

        var offset = hash.Length;
        while (offset < length)
        {
            var bytesToCopy = Math.Min(hash.Length, length - offset);
            Buffer.BlockCopy(hash, 0, result, offset, bytesToCopy);
            offset += bytesToCopy;
        }

        return result;
    }

    public static byte[] FillRepeatBytes(byte[] key, int length)
    {
        ArgumentNullException.ThrowIfNull(key);
        if (length < 0) throw new ArgumentOutOfRangeException(nameof(length), "Length cannot be negative.");

        if (key.Length >= length) return key[..length];

        var destinationKey = new byte[length];
        Buffer.BlockCopy(key, 0, destinationKey, 0, key.Length);

        var remaining = length - key.Length;
        var numCopies = remaining / key.Length;
        var remainder = remaining % key.Length;

        for (var i = 0; i < numCopies; i++)
        {
            Buffer.BlockCopy(key, 0, destinationKey, key.Length + i * key.Length, key.Length);
        }

        Buffer.BlockCopy(key, 0, destinationKey, length - remainder, remainder);

        return destinationKey;
    }
}