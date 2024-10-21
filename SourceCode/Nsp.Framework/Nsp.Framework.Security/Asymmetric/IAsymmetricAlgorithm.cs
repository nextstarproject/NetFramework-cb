namespace Nsp.Framework.Security.Asymmetric;

public interface IAsymmetricAlgorithm
{
    public const string BeginPublicKey = "-----BEGIN PUBLIC KEY-----";
    public const string EndPublicKey = "-----END PUBLIC KEY-----";
    public const string BeginPrivateKey = "-----BEGIN PRIVATE KEY-----";
    public const string EndPrivateKey = "-----END PRIVATE KEY-----";
    
    string Encrypt(string plainText);
    string Decrypt(string cipherText);
    string EncryptToHex(string plainText);
    string DecryptFromHex(string cipherHexText);
    string EncryptToBase64(string plainText);
    string DecryptFromBase64(string cipherBase64);
    byte[] Encrypt(byte[] plainBytes);
    byte[] Decrypt(byte[] cipherBytes);
}