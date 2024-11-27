using System.Text;

namespace Nsp.Framework.Security.Mac;

public class HmacSha256 : IHmacShaAlgorithm
{
    public int KeyBitSize => 256;
    public int KeyByteSize => KeyBitSize / 8;

    public byte[] HmacKey => _hmacKey;

    private readonly byte[] _hmacKey;

    public HmacSha256(byte[] key)
    {
        _hmacKey = key;
    }

    public HmacSha256(string key)
    {
        _hmacKey = Encoding.UTF8.GetBytes(key);
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

    public string EncryptToHex(string plainText)
    {
        var bytes = Encoding.UTF8.GetBytes(plainText);
        return SecurityUtil.BytesToHexString(Encrypt(bytes));
    }

    public bool CompareFromHex(string plainText, string hexText)
    {
        var textBytes = Encoding.UTF8.GetBytes(plainText);
        var hmacBytes = SecurityUtil.HexStringToByte(hexText);
        return Compare(textBytes, hmacBytes);
    }

    public string EncryptToBase64(string plainText)
    {
        var bytes = Encoding.UTF8.GetBytes(plainText);
        return Convert.ToBase64String(Encrypt(bytes));
    }

    public bool CompareFromBase64(string plainText, string base64Text)
    {
        var textBytes = Encoding.UTF8.GetBytes(plainText);
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
}