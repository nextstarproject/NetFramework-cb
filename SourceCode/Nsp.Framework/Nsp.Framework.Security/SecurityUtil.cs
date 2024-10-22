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
        
        while (hash.Length < length)
        {
            hash = hash.Concat(hash).ToArray();
        }

        return hash.Take(length).ToArray();
    }
}