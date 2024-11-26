namespace Nsp.Framework.Security.Symmetric;

public interface IAesEncryption : ISymmetricAlgorithm
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
    /// 加密向量字节大小
    /// </summary>
    public static abstract int IvByteSize { get; }
    public byte[] AesKey { get; }
    public byte[] AesIv { get; }
    
    public string AesKeyBase64 { get; }
    public string AesIvBase64 { get; }
}