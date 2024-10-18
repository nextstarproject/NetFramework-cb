using System.Text;
using Nsp.Framework.Core;

namespace Nsp.Framework.Security.Symmetric;

public class Aes128CbcHmacSha256 : IAesCbcHmacSha
{
    public int KeyBitSize => 128;
    public int KeyByteSize => KeyBitSize / 8;
    public int HmacKeyBitSize => 256;
    public int HmacKeyByteSize => HmacKeyBitSize / 8;
    public byte[] AesKey => _aesKey;
    public byte[] HmacKey => _hmacKey;
    public string AesKeyString => Encoding.UTF8.GetString(_aesKey);
    public string HmacKeyString => Encoding.UTF8.GetString(_hmacKey);
    
    private readonly byte[] _aesKey;
    private readonly byte[] _hmacKey;

    public Aes128CbcHmacSha256(byte[] key)
    {
        if (key.Length != KeyByteSize)
            throw new ArgumentException($"Key must be {KeyBitSize} bits ({KeyByteSize} bytes).", nameof(key));
        _aesKey = key;
        _hmacKey = RandomStringUtil.CreateRandomKey(HmacKeyByteSize);
    }
    
    public Aes128CbcHmacSha256(byte[] aesKey, byte[] hmacKey)
    {
        if (aesKey.Length != KeyByteSize)
            throw new ArgumentException($"Key must be {KeyBitSize} bits ({KeyByteSize} bytes).", nameof(aesKey));
        if (hmacKey.Length != KeyByteSize)
            throw new ArgumentException($"Hmac key must be {HmacKeyBitSize} bits ({HmacKeyByteSize} bytes).", nameof(hmacKey));

        _aesKey = aesKey;
        _hmacKey = hmacKey;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="aesKey">如果转换后长度超过，则截断，不足则补0</param>
    public Aes128CbcHmacSha256(string aesKey)
    {
        _aesKey = GetBytes(aesKey, KeyByteSize, nameof(aesKey));
        _hmacKey = RandomStringUtil.CreateRandomKey(HmacKeyByteSize);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="aesKey">如果转换后长度超过，则截断，不足则补0</param>
    /// <param name="hmacKey">如果转换后长度超过，则截断，不足则补0</param>
    public Aes128CbcHmacSha256(string aesKey, string hmacKey)
    {
        _aesKey = GetBytes(aesKey, KeyByteSize, nameof(aesKey));
        _hmacKey = GetBytes(hmacKey, HmacKeyByteSize, nameof(hmacKey));
    }
    
    // 支持字符串的 Encrypt
    public string Encrypt(string plainText)
    {
        var plainBytes = Encoding.UTF8.GetBytes(plainText);
        var encryptedBytes = Encrypt(plainBytes);
        return Convert.ToBase64String(encryptedBytes);
    }
    
    // 支持字符串的 Decrypt
    public string Decrypt(string cipherText)
    {
        var cipherBytes = Convert.FromBase64String(cipherText);
        var decryptedBytes = Decrypt(cipherBytes);
        return Encoding.UTF8.GetString(decryptedBytes);
    }
    
    // 重载 Encrypt 支持 byte[]
    public byte[] Encrypt(byte[] plainBytes)
    {
        using (var aesAlg = Aes.Create())
        {
            aesAlg.Key = _aesKey;
            aesAlg.GenerateIV(); // 随机生成 IV
            aesAlg.Mode = CipherMode.CBC;

            using (var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV))
            using (var msEncrypt = new MemoryStream())
            {
                using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    csEncrypt.Write(plainBytes, 0, plainBytes.Length);
                }

                var iv = aesAlg.IV;
                var encryptedBytes = msEncrypt.ToArray();

                // Compute HMAC-SHA256
                using (var hmac = new HMACSHA256(_hmacKey))
                {
                    var hmacBytes = hmac.ComputeHash(encryptedBytes);
                    // Combine IV + EncryptedData + HMAC
                    var result = new byte[iv.Length + encryptedBytes.Length + hmacBytes.Length];
                    Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
                    Buffer.BlockCopy(encryptedBytes, 0, result, iv.Length, encryptedBytes.Length);
                    Buffer.BlockCopy(hmacBytes, 0, result, iv.Length + encryptedBytes.Length, hmacBytes.Length);

                    return result;
                }
            }
        }
    }

    // 重载 Decrypt 支持 byte[]
    public byte[] Decrypt(byte[] cipherBytes)
    {
        using (var aesAlg = Aes.Create())
        {
            aesAlg.Key = _aesKey;
            aesAlg.Mode = CipherMode.CBC;

            // Extract IV (first 16 bytes)
            var iv = new byte[16];
            Buffer.BlockCopy(cipherBytes, 0, iv, 0, iv.Length);
            aesAlg.IV = iv;

            // Extract Encrypted Data (next part)
            var hmacLength = 32; // HMAC-SHA256 length
            var encryptedDataLength = cipherBytes.Length - iv.Length - hmacLength;
            var encryptedBytes = new byte[encryptedDataLength];
            Buffer.BlockCopy(cipherBytes, iv.Length, encryptedBytes, 0, encryptedDataLength);

            // Extract HMAC
            var hmacBytes = new byte[hmacLength];
            Buffer.BlockCopy(cipherBytes, iv.Length + encryptedDataLength, hmacBytes, 0, hmacLength);

            // Verify HMAC
            using (var hmac = new HMACSHA256(_hmacKey))
            {
                var computedHmac = hmac.ComputeHash(encryptedBytes);
                if (!CryptographicOperations.FixedTimeEquals(computedHmac, hmacBytes))
                {
                    throw new CryptographicException("HMAC validation failed.");
                }
            }

            // Decrypt
            using (var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV))
            using (var msDecrypt = new MemoryStream(encryptedBytes))
            using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
            using (var resultStream = new MemoryStream())
            {
                csDecrypt.CopyTo(resultStream);
                return resultStream.ToArray();
            }
        }
    }
    
    private byte[] GetBytes(string input, int length, string paramName)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(input);

        if (bytes.Length < length)
        {
            Array.Resize(ref bytes, length);
            for (int i = bytes.Length; i < length; i++)
            {
                bytes[i] = 0; // 用 0 字符填充
            }
        }
        else if (bytes.Length > length)
        {
            Array.Resize(ref bytes, length); // 截断多余部分
        }

        return bytes;
    }
}