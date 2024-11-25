using System.Text;
using Nsp.Framework.Core;

namespace Nsp.Framework.Security.Symmetric;

public class Aes128Encryption : IAesEncryption
{
    public int KeyBitSize => 128;
    public int KeyByteSize => KeyBitSize / 8;

    public int IvByteSize => 16;

    public byte[] AesKey => _aesKey;
    public byte[] AesIv => _aesIv;

    private readonly byte[] _aesKey;
    private readonly byte[] _aesIv;

    public Aes128Encryption(byte[] key)
    {
        _aesKey = SecurityUtil.FillRepeatBytes(key, KeyByteSize);
        _aesIv = RandomStringUtil.CreateRandomKey(IvByteSize);
    }

    public Aes128Encryption(byte[] key, byte[] iv)
    {
        _aesKey = SecurityUtil.FillRepeatBytes(key, KeyByteSize);
        _aesIv = SecurityUtil.FillRepeatBytes(iv, IvByteSize);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key">如果转换后长度超过，则截断，不足则重复填充</param>
    public Aes128Encryption(string key)
    {
        _aesKey = SecurityUtil.GetBytes(key, KeyByteSize);
        _aesIv = RandomStringUtil.CreateRandomKey(IvByteSize);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key">如果转换后长度超过，则截断，不足则重复填充</param>
    /// <param name="iv">如果转换后长度超过，则截断，不足则重复填充</param>
    public Aes128Encryption(string key, string iv)
    {
        _aesKey = SecurityUtil.GetBytes(key, KeyByteSize);
        _aesIv = SecurityUtil.GetBytes(iv, IvByteSize);
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