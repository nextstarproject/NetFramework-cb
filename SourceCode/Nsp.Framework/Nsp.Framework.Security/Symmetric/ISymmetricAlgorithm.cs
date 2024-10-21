namespace Nsp.Framework.Security.Symmetric;

public interface ISymmetricAlgorithm
{
    string Encrypt(string plainText);
    string Decrypt(string cipherText);
    string EncryptToHex(string plainText);
    string DecryptFromHex(string cipherHexText);
    string EncryptToBase64(string plainText);
    string DecryptFromBase64(string cipherBase64);
    byte[] Encrypt(byte[] plainBytes);
    byte[] Decrypt(byte[] cipherBytes);
}