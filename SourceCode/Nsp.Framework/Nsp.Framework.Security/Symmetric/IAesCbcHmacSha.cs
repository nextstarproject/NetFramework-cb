namespace Nsp.Framework.Security.Symmetric;

public interface IAesCbcHmacSha : ISymmetricAlgorithm
{
    /// <summary>
    /// Aes 位数
    /// </summary>
    public static abstract int KeyBitSize { get; }
    /// <summary>
    /// Aes 字节大小
    /// </summary>
    public static abstract int KeyByteSize { get; }
    /// <summary>
    /// Hmac 位数
    /// </summary>
    public static abstract int HmacKeyBitSize { get; }
    /// <summary>
    /// Hmac 字节大小
    /// </summary>
    public static abstract int HmacKeyByteSize { get; }
    public byte[] AesKey { get; }
    public byte[] HmacKey { get; }
    
    public string AesKeyBase64 { get; }
    public string HmacKeyBase64 { get; }
}