namespace Nsp.Framework.Security.Hashing;

public interface IHashingAlgorithm
{
    string Encrypt(string plainText);
    bool Compare(string plainText, string hmacText);
    string EncryptBase64(string plainText);
    bool CompareBase64(string plainText, string base64Text);
    byte[] Encrypt(byte[] plainBytes);
    bool Compare(byte[] plainBytes, byte[] hmacBytes);

}