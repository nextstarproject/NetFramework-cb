namespace Nsp.Framework.Security.Mac;

public interface IHmacShaAlgorithm : IMacAlgorithm
{
    /// <summary>
    /// Hmac 位数
    /// </summary>
    public int KeyBitSize { get; }
    /// <summary>
    /// Hmac 字节大小
    /// </summary>
    public int KeyByteSize { get; }
    public byte[] HmacKey { get; }
    public string HmacKeyString { get; }
    public string HmacKeyHex { get; }
    public string HmacKeyBase64 { get; }
}