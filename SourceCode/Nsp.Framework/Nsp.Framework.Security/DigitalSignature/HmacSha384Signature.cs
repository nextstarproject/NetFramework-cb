using Nsp.Framework.Security.Mac;

namespace Nsp.Framework.Security.DigitalSignature;

/// <summary>
/// 和 <see cref="HmacSha384"/> 一致，后续可能多函数
/// </summary>
public class HmacSha384Signature : HmacSha384, IHmacShaSignatureAlgorithm
{
    public HmacSha384Signature(byte[] key) : base(key)
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key">如果转换后长度超过，则截断，不足则重复填充</param>
    public HmacSha384Signature(string key) : base(key)
    {
    }
    
    public string GenerateSignature(string plainText)
    {
        return base.Encrypt(plainText);
    }

    public bool VerifySignature(string plainText, string signatureText)
    {
        return base.Compare(plainText, signatureText);
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
    
    public byte[] GenerateSignature(byte[] plainBytes)
    {
        return base.Encrypt(plainBytes);
    }

    public bool VerifySignature(byte[] plainBytes, byte[] signatureBytes)
    {
        return base.Compare(plainBytes, signatureBytes);
    }
}