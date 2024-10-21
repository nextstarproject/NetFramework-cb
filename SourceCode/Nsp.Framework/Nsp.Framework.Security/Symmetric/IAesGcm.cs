namespace Nsp.Framework.Security.Symmetric;

public interface IAesGcm : ISymmetricAlgorithm
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
    public string AesKeyString { get; }
    public string AesKeyHex { get; }
    public string AesKeyBase64 { get; }
    /// <summary>
    /// 默认始终为 96 / 8 = 12 字节
    /// </summary>
    public int NonceByteSize { get; }
    /// <summary>
    /// 默认始终为 128 / 8 = 16 字节
    /// </summary>
    public int TagByteSize { get; }
}