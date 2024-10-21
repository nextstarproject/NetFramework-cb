namespace Nsp.Framework.Security.Symmetric;

public interface IAesEncryption : ISymmetricAlgorithm
{
    /// <summary>
    /// Aes 位数
    /// </summary>
    public int KeyBitSize { get; }
    /// <summary>
    /// Aes 字节大小
    /// </summary>
    public int KeyByteSize { get; }
    public byte[] AesKey { get; }
    public byte[] AesIv { get; }
    public string AesKeyString { get; }
    public string AesKeyHex { get; }
    public string AesKeyBase64 { get; }
    public string AesIvString { get; }
    public string AesIvHex { get; }
    public string AesIvBase64 { get; }
    
}