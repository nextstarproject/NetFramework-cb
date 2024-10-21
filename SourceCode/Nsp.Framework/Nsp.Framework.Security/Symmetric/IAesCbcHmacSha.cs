namespace Nsp.Framework.Security.Symmetric;

public interface IAesCbcHmacSha : ISymmetricAlgorithm
{
    /// <summary>
    /// Aes 位数
    /// </summary>
    public int KeyBitSize { get; }
    /// <summary>
    /// Aes 字节大小
    /// </summary>
    public int KeyByteSize { get; }
    /// <summary>
    /// Hmac 位数
    /// </summary>
    public int HmacKeyBitSize { get; }
    /// <summary>
    /// Hmac 字节大小
    /// </summary>
    public int HmacKeyByteSize { get; }
    public byte[] AesKey { get; }
    public byte[] HmacKey { get; }
    public string AesKeyString { get; }
    public string AesKeyHex { get; }
    public string AesKeyBase64 { get; }
    public string HmacKeyString { get; }
    public string HmacKeyHex { get; }
    public string HmacKeyBase64 { get; }
}