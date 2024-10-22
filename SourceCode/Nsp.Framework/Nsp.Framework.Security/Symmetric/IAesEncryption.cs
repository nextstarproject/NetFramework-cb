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
    public int IvByteSize { get; }
    public byte[] AesKey { get; }
    public byte[] AesIv { get; }
    
}