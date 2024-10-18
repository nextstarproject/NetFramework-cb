namespace Nsp.Framework.Security.Hashing;

public interface IHmacHashingAlgorithm : IHashingAlgorithm
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
}