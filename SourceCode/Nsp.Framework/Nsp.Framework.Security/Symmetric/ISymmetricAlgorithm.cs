namespace Nsp.Framework.Security.Symmetric;

public interface ISymmetricAlgorithm
{
    string Encrypt(string plainText);
    string Decrypt(string cipherText);
    byte[] Encrypt(byte[] plainBytes);
    byte[] Decrypt(byte[] cipherBytes);
}