using System.Text;
using Nsp.Framework.Core;

namespace Nsp.Framework.Security.Symmetric;

public class Aes256Encryption : IAesEncryption
{
    public static int KeyBitSize => 256;
    public static int KeyByteSize => KeyBitSize / 8;
    
    public static int IvByteSize => 16;
    
    public byte[] AesKey => _aesKey;
    public byte[] AesIv => _aesIv;
    public string AesKeyBase64 => Convert.ToBase64String(_aesKey);
    public string AesIvBase64 => Convert.ToBase64String(_aesIv);

    private readonly byte[] _aesKey;
    private readonly byte[] _aesIv;
    
    public Aes256Encryption()
    {
        _aesKey = RandomStringUtil.CreateRandomKey(KeyByteSize);
        _aesIv = RandomStringUtil.CreateRandomKey(IvByteSize);
    }

    public Aes256Encryption(byte[] key)
    {
        SecurityInvalidKeyException.ThrowIfInsufficient(key, KeyByteSize);
        _aesKey = key;
        _aesIv = RandomStringUtil.CreateRandomKey(IvByteSize);
    }
    
    public Aes256Encryption(byte[] key, byte[] iv)
    {
        SecurityInvalidKeyException.ThrowIfInsufficient(key, KeyByteSize);
        SecurityInvalidKeyException.ThrowIfInsufficient(iv, IvByteSize);
        _aesKey = key;
        _aesIv = iv;
    }
    
    public Aes256Encryption(string keyBase64)
    {
        var keyBytes = Convert.FromBase64String(keyBase64);
        SecurityInvalidKeyException.ThrowIfInsufficient(keyBytes, KeyByteSize);
        _aesKey = keyBytes;
        _aesIv = RandomStringUtil.CreateRandomKey(IvByteSize);
    }

    public Aes256Encryption(string keyBase64, string ivBase64)
    {
        var keyBytes = Convert.FromBase64String(keyBase64);
        SecurityInvalidKeyException.ThrowIfInsufficient(keyBytes, KeyByteSize);
        var ivBytes = Convert.FromBase64String(ivBase64);
        SecurityInvalidKeyException.ThrowIfInsufficient(ivBytes, IvByteSize);
        _aesKey = keyBytes;
        _aesIv = ivBytes;
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
        using (var aesAlg = Aes.Create())
        {
            aesAlg.Key = _aesKey;
            aesAlg.IV = _aesIv;
            aesAlg.Mode = CipherMode.CBC; // 可根据需要选择模式

            using (var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV))
            using (var msEncrypt = new MemoryStream())
            {
                using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    csEncrypt.Write(plainBytes);
                }
                return msEncrypt.ToArray();
            }
        }
    }

    public byte[] Decrypt(byte[] cipherBytes)
    {
        using (var aesAlg = Aes.Create())
        {
            aesAlg.Key = _aesKey;
            aesAlg.IV = _aesIv;
            aesAlg.Mode = CipherMode.CBC; // 可根据需要选择模式

            using (var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV))
            using (var msDecrypt = new MemoryStream(cipherBytes))
            {
                using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (var resultStream = new MemoryStream())
                    {
                        csDecrypt.CopyTo(resultStream);
                        return resultStream.ToArray();
                    }
                }
            }
        }
    }
}