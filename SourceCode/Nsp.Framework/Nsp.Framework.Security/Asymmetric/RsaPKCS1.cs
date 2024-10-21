using System.Text;

namespace Nsp.Framework.Security.Asymmetric;

public class RsaPKCS1 : RsaProvider, IRsaAsymmetricAlgorithm
{
    public RsaPKCS1(int size = 2048) : base(size)
    {
    }

    public RsaPKCS1(string xmlPrivateAndPublic) : base(xmlPrivateAndPublic)
    {
    }

    public RsaPKCS1(string base64PrivateKey, string base64PublicKey) : base(base64PrivateKey, base64PublicKey)
    {
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
        return _rsa.Encrypt(plainBytes, RSAEncryptionPadding.Pkcs1);
    }

    public byte[] Decrypt(byte[] cipherBytes)
    {
        return _rsa.Decrypt(cipherBytes, RSAEncryptionPadding.Pkcs1);
    }
}