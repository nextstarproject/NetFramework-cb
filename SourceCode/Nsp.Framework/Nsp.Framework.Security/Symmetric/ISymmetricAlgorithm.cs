namespace Nsp.Framework.Security.Symmetric;

public interface ISymmetricAlgorithm
{
    string Encrypt(string plainText);
    string Decrypt(string cipherText);
    string EncryptHex(string plainText);
    string DecryptHex(string cipherHexText);
    string EncryptBase64(string plainText);
    string DecryptBase64(string cipherBase64);
    byte[] Encrypt(byte[] plainBytes);
    byte[] Decrypt(byte[] cipherBytes);
}