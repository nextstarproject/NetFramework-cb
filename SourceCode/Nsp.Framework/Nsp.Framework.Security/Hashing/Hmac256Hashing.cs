using System.Text;

namespace Nsp.Framework.Security.Hashing;

public class Hmac256Hashing : IHmacHashingAlgorithm
{
    public int KeyBitSize => 256;
    public int KeyByteSize => KeyBitSize / 8;

    public byte[] HmacKey => _hmacKey;
    public string HmacKeyString => Encoding.UTF8.GetString(_hmacKey);

    private readonly byte[] _hmacKey;

    public Hmac256Hashing(byte[] key)
    {
        if (key.Length != KeyByteSize)
            throw new ArgumentException($"Key must be {KeyBitSize} bits ({KeyByteSize} bytes).", nameof(key));
        _hmacKey = key;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key">如果转换后长度超过，则截断，不足则补0</param>
    public Hmac256Hashing(string key)
    {
        _hmacKey = GetBytes(key, KeyByteSize, nameof(key));
    }

    public string Encrypt(string plainText)
    {
        var bytes = Encoding.UTF8.GetBytes(plainText);
        return Encoding.UTF8.GetString(Encrypt(bytes));
    }

    public bool Compare(string plainText, string hmacText)
    {
        var textBytes = Encoding.UTF8.GetBytes(plainText);
        var hmacBytes = Encoding.UTF8.GetBytes(hmacText);
        return Compare(textBytes, hmacBytes);
    }

    public string EncryptBase64(string plainText)
    {
        var bytes = Encoding.UTF8.GetBytes(plainText);
        return Convert.ToBase64String(Encrypt(bytes));
    }

    public bool CompareBase64(string plainText, string base64Text)
    {
        var textBytes = Convert.FromBase64String(plainText);
        var hmacBytes = Convert.FromBase64String(base64Text);
        return Compare(textBytes, hmacBytes);
    }

    public byte[] Encrypt(byte[] plainBytes)
    {
        using (var hmac = new HMACSHA256(_hmacKey))
        {
            var hash = hmac.ComputeHash(plainBytes);
            return hash;
        }
    }

    public bool Compare(byte[] plainBytes, byte[] hmacBytes)
    {
        var computedHmac = Encrypt(plainBytes);
        return computedHmac.SequenceEqual(hmacBytes);
    }

    #region Private Method

    private byte[] GetBytes(string input, int length, string paramName)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(input);

        if (bytes.Length < length)
        {
            Array.Resize(ref bytes, length);
            for (int i = bytes.Length; i < length; i++)
            {
                bytes[i] = 0; // 用 0 字符填充
            }
        }
        else if (bytes.Length > length)
        {
            Array.Resize(ref bytes, length); // 截断多余部分
        }

        return bytes;
    }

    #endregion
}