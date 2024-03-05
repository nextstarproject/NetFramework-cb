using System.Text;

namespace System;

public static class StringExtensions
{
    /// <summary>
    /// 将给定的字符串加密为 md5 十六进制
    /// </summary>
    public static string ToMd5([NotNull] this string str)
    {
        ArgumentNullException.ThrowIfNull(str);
        using var md5 = MD5.Create();
        var inputBytes = Encoding.UTF8.GetBytes(str);
        var hashBytes = md5.ComputeHash(inputBytes);

        var sb = new StringBuilder();
        foreach (var hashByte in hashBytes)
        {
            sb.Append(hashByte.ToString("X2"));
        }

        return sb.ToString();
    }

    /// <summary>
    /// 将给定的字符串加密为 sh1 十六进制
    /// </summary>
    public static string ToSha1([NotNull] this string str)
    {
        ArgumentNullException.ThrowIfNull(str);
        using var encrypt = SHA1.Create();
        var inputBytes = Encoding.UTF8.GetBytes(str);
        var hashBytes = encrypt.ComputeHash(inputBytes);

        var sb = new StringBuilder();
        foreach (var hashByte in hashBytes)
        {
            sb.Append(hashByte.ToString("X2"));
        }

        return sb.ToString();
    }

    /// <summary>
    /// 将给定的字符串加密为 sha256 十六进制
    /// </summary>
    public static string ToSha256([NotNull] this string str)
    {
        ArgumentNullException.ThrowIfNull(str);
        using var encrypt = SHA256.Create();
        var inputBytes = Encoding.UTF8.GetBytes(str);
        var hashBytes = encrypt.ComputeHash(inputBytes);

        var sb = new StringBuilder();
        foreach (var hashByte in hashBytes)
        {
            sb.Append(hashByte.ToString("X2"));
        }

        return sb.ToString();
    }
    
    /// <summary>
    /// 将给定的字符串加密为 SHA384 十六进制
    /// </summary>
    public static string ToSha384([NotNull] this string str)
    {
        ArgumentNullException.ThrowIfNull(str);
        using var encrypt = SHA384.Create();
        var inputBytes = Encoding.UTF8.GetBytes(str);
        var hashBytes = encrypt.ComputeHash(inputBytes);

        var sb = new StringBuilder();
        foreach (var hashByte in hashBytes)
        {
            sb.Append(hashByte.ToString("X2"));
        }

        return sb.ToString();
    }

    /// <summary>
    /// 将给定的字符串加密为 sha512 十六进制
    /// </summary>
    public static string ToSha512([NotNull] this string str)
    {
        ArgumentNullException.ThrowIfNull(str);
        using var encrypt = SHA512.Create();
        var inputBytes = Encoding.UTF8.GetBytes(str);
        var hashBytes = encrypt.ComputeHash(inputBytes);

        var sb = new StringBuilder();
        foreach (var hashByte in hashBytes)
        {
            sb.Append(hashByte.ToString("X2"));
        }

        return sb.ToString();
    }

    /// <summary>
    /// 将给定的字符串加密为 md5 base64
    /// </summary>
    public static string ToMd5Base64([NotNull] this string str)
    {
        ArgumentNullException.ThrowIfNull(str);
        using var encrypt = MD5.Create();
        var inputBytes = Encoding.UTF8.GetBytes(str);
        var hashBytes = encrypt.ComputeHash(inputBytes);

        var output = Convert.ToBase64String(hashBytes);
        return output;
    }

    /// <summary>
    /// 将给定的字符串加密为 SHA1 base64
    /// </summary>
    public static string ToSha1Base64([NotNull] this string str)
    {
        ArgumentNullException.ThrowIfNull(str);
        using var encrypt = SHA1.Create();
        var inputBytes = Encoding.UTF8.GetBytes(str);
        var hashBytes = encrypt.ComputeHash(inputBytes);

        var output = Convert.ToBase64String(hashBytes);
        return output;
    }

    /// <summary>
    /// 将给定的字符串加密为 SHA256 base64
    /// </summary>
    public static string ToSha256Base64([NotNull] this string str)
    {
        ArgumentNullException.ThrowIfNull(str);
        using var encrypt = SHA256.Create();
        var inputBytes = Encoding.UTF8.GetBytes(str);
        var hashBytes = encrypt.ComputeHash(inputBytes);

        var output = Convert.ToBase64String(hashBytes);
        return output;
    }
    
    /// <summary>
    /// 将给定的字符串加密为 SHA384 base64
    /// </summary>
    public static string ToSha384Base64([NotNull] this string str)
    {
        ArgumentNullException.ThrowIfNull(str);
        using var encrypt = SHA384.Create();
        var inputBytes = Encoding.UTF8.GetBytes(str);
        var hashBytes = encrypt.ComputeHash(inputBytes);

        var output = Convert.ToBase64String(hashBytes);
        return output;
    }

    /// <summary>
    /// 将给定的字符串加密为 SHA512 base64
    /// </summary>
    public static string ToSha512Base64([NotNull] this string str)
    {
        ArgumentNullException.ThrowIfNull(str);
        using var encrypt = SHA512.Create();
        var inputBytes = Encoding.UTF8.GetBytes(str);
        var hashBytes = encrypt.ComputeHash(inputBytes);

        var output = Convert.ToBase64String(hashBytes);
        return output;
    }
}