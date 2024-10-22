using System.Text;

namespace Nsp.Framework.Security.Symmetric;

public class Aes192Gcm : IAesGcm
{
    public int KeyBitSize => 192;
    public int KeyByteSize => KeyBitSize / 8;
    
    public byte[] AesKey => _aesKey;
    
    public int NonceByteSize => 96 / 8; // 96-bit nonce for GCM
    public int TagByteSize => 128 / 8;   // 128-bit authentication tag

    private readonly byte[] _aesKey;
    
    public Aes192Gcm(byte[] key)
    {
        if (key.Length != KeyByteSize)
            throw new ArgumentException($"Key must be {KeyBitSize} bits ({KeyByteSize} bytes).", nameof(key));
        _aesKey = key;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="key">如果转换后长度超过，则截断，不足则补0</param>
    public Aes192Gcm(string key)
    {
        _aesKey = SecurityUtil.GetBytes(key, KeyByteSize);
    }
    
    public string Encrypt(string plainText)
    {
        var plainBytes = Encoding.UTF8.GetBytes(plainText);
        var encryptedBytes = Encrypt(plainBytes);
        return Encoding.UTF8.GetString(encryptedBytes);
    }
    
    public string Decrypt(string cipherText)
    {
        var cipherBytes = Encoding.UTF8.GetBytes(cipherText);
        var decryptedBytes = Decrypt(cipherBytes);
        return Encoding.UTF8.GetString(decryptedBytes);
    }

    public string EncryptToHex(string plainText)
    {
        var plainBytes = Encoding.UTF8.GetBytes(plainText);
        var encryptedBytes = Encrypt(plainBytes);
        return SecurityUtil.BytesToHexString(encryptedBytes);
    }

    public string DecryptFromHex(string cipherHexText)
    {
        var cipherBytes = SecurityUtil.HexStringToByte(cipherHexText);
        var decryptedBytes = Decrypt(cipherBytes);
        return Encoding.UTF8.GetString(decryptedBytes);
    }

    public string EncryptToBase64(string plainText)
    {
        var plainBytes = Encoding.UTF8.GetBytes(plainText);
        var encryptedBytes = Encrypt(plainBytes);
        return Convert.ToBase64String(encryptedBytes);
    }

    public string DecryptFromBase64(string cipherBase64)
    {
        var cipherBytes = Convert.FromBase64String(cipherBase64);
        var decryptedBytes = Decrypt(cipherBytes);
        return Encoding.UTF8.GetString(decryptedBytes);
    }
    
    public byte[] Encrypt(byte[] plainBytes)
    {
        using (var aesGcm = new AesGcm(_aesKey, TagByteSize))
        {
            var nonce = new byte[NonceByteSize];
            RandomNumberGenerator.Fill(nonce); // 随机生成 nonce

            var ciphertext = new byte[plainBytes.Length];
            var tag = new byte[TagByteSize]; // 认证标签

            // 执行加密操作
            aesGcm.Encrypt(nonce, plainBytes, ciphertext, tag);

            // 拼接 nonce + ciphertext + tag 一起返回
            var result = new byte[nonce.Length + ciphertext.Length + tag.Length];
            Buffer.BlockCopy(nonce, 0, result, 0, nonce.Length);
            Buffer.BlockCopy(ciphertext, 0, result, nonce.Length, ciphertext.Length);
            Buffer.BlockCopy(tag, 0, result, nonce.Length + ciphertext.Length, tag.Length);

            return result;
        }
    }

    public byte[] Decrypt(byte[] cipherBytes)
    {
        using (var aesGcm = new AesGcm(_aesKey, TagByteSize))
        {
            // 提取 nonce, ciphertext, 和 tag
            var nonce = new byte[NonceByteSize];
            Buffer.BlockCopy(cipherBytes, 0, nonce, 0, NonceByteSize);

            var ciphertextLength = cipherBytes.Length - NonceByteSize - TagByteSize;
            var ciphertext = new byte[ciphertextLength];
            Buffer.BlockCopy(cipherBytes, NonceByteSize, ciphertext, 0, ciphertextLength);

            var tag = new byte[TagByteSize];
            Buffer.BlockCopy(cipherBytes, NonceByteSize + ciphertextLength, tag, 0, TagByteSize);

            var decryptedBytes = new byte[ciphertext.Length];

            // 执行解密操作
            aesGcm.Decrypt(nonce, ciphertext, tag, decryptedBytes);

            return decryptedBytes;
        }
    }
}