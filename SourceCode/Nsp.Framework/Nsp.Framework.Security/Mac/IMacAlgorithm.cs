namespace Nsp.Framework.Security.Mac;

public interface IMacAlgorithm
{
    string Encrypt(string plainText);
    bool Compare(string plainText, string hmacText);
    string EncryptToHex(string plainText);
    bool CompareFromHex(string plainText, string hexText);
    string EncryptToBase64(string plainText);
    bool CompareFromBase64(string plainText, string base64Text);
    byte[] Encrypt(byte[] plainBytes);
    bool Compare(byte[] plainBytes, byte[] hmacBytes);

}