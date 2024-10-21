namespace Nsp.Framework.Security.Mac;

/// <summary>
/// 和 <see cref="HmacSha256"/> 一致，后续可能多函数
/// </summary>
public class HmacSha256Signature : HmacSha256, IHmacShaSignature
{
    public HmacSha256Signature(byte[] key) : base(key)
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key">如果转换后长度超过，则截断，不足则补0</param>
    public HmacSha256Signature(string key) : base(key)
    {
    }

    public string GenerateSignature(string plainText)
    {
        return base.Encrypt(plainText);
    }

    public bool VerifySignature(string plainText, string hmacText)
    {
        return base.Compare(plainText, hmacText);
    }

    public string GenerateSignatureToHex(string plainText)
    {
        return base.EncryptToHex(plainText);
    }

    public bool VerifySignatureFromHex(string plainText, string hexText)
    {
        return base.CompareFromHex(plainText, hexText);
    }

    public string GenerateSignatureToBase64(string plainText)
    {
        return base.EncryptToBase64(plainText);
    }

    public bool VerifySignatureFromBase64(string plainText, string base64Text)
    {
        return base.CompareFromBase64(plainText, base64Text);
    }
}