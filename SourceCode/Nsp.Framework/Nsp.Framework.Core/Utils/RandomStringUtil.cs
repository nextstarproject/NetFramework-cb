using System.Security.Cryptography;

namespace Nsp.Framework.Core;

public static class RandomStringUtil
{
    public const string UrlSafeCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._~";
    public const string NumericCharacters = "0123456789";
    public const string DistinguishableCharacters = "CDEHKMPRTUWXY012458";
    public const string AsciiPrintableCharacters =
        "!\"#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstuvwxyz{|}~";
    public const string AlphanumericCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

    private static readonly RandomNumberGenerator Rng = RandomNumberGenerator.Create();
    private static readonly Random Random = new Random();

    /// <summary>
    /// 生成随机的URL安全字符
    /// </summary>
    /// <param name="len"></param>
    /// <returns></returns>
    public static string UrlSafe(int len = 32)
    {
        return new string(Enumerable.Repeat(UrlSafeCharacters, len)
            .Select(s => s[Random.Next(s.Length)]).ToArray());
    }

    /// <summary>
    /// 生成由数字构成的随机字符串
    /// </summary>
    /// <param name="len"></param>
    /// <returns></returns>
    public static string Numeric(int len = 32)
    {
        return new string(Enumerable.Repeat(NumericCharacters, len)
            .Select(s => s[Random.Next(s.Length)]).ToArray());
    }

    /// <summary>
    /// 生成易分辨的字符串
    /// </summary>
    /// <param name="len"></param>
    /// <returns></returns>
    public static string Distinguishable(int len = 32)
    {
        return new string(Enumerable.Repeat(DistinguishableCharacters, len)
            .Select(s => s[Random.Next(s.Length)]).ToArray());
    }

    /// <summary>
    /// 生成有Ascii打印字符组成的字符串
    /// </summary>
    /// <param name="len"></param>
    /// <returns></returns>
    public static string AsciiPrintable(int len = 32)
    {
        return new string(Enumerable.Repeat(AsciiPrintableCharacters, len)
            .Select(s => s[Random.Next(s.Length)]).ToArray());
    }

    /// <summary>
    /// 由大小写字符和数字组成的随机字符串
    /// </summary>
    /// <param name="len"></param>
    /// <returns></returns>
    public static string Alphanumeric(int len = 32)
    {
        return new string(Enumerable.Repeat(AlphanumericCharacters, len)
            .Select(s => s[Random.Next(s.Length)]).ToArray());
    }

    /// <summary>
    /// 创建16进制Id
    /// </summary>
    /// <param name="length"></param>
    /// <returns></returns>
    public static string CreateHexUniqueId(int length = 32)
    {
        var bytes = CreateRandomKey(length * 2);
        return BitConverter.ToString(bytes).Replace("-", "")[..length];
    }

    /// <summary>
    /// 创建 base64 Id
    /// </summary>
    /// <param name="length"></param>
    /// <returns></returns>
    public static string CreateBase64UniqueId(int length = 32)
    {
        var bytes = CreateRandomKey(length * 2);
        return Convert.ToBase64String(bytes)[..length];
    }

    public static byte[] CreateRandomKey(int length)
    {
        var bytes = new byte[length];
        Rng.GetBytes(bytes);
        return bytes;
    }

    public static string CreateRandomHexKey(int length, bool lowercase = false)
    {
        var str = RandomNumberGenerator.GetHexString(length, lowercase);
        return str;
    }
}