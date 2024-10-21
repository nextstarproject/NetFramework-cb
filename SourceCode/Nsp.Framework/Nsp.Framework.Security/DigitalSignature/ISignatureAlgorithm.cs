namespace Nsp.Framework.Security.DigitalSignature;

public interface ISignatureAlgorithm
{
    string GenerateSignature(string plainText);
    bool VerifySignature(string plainText, string signatureText);
    string GenerateSignatureToHex(string plainText);
    bool VerifySignatureFromHex(string plainText, string hexText);
    string GenerateSignatureToBase64(string plainText);
    bool VerifySignatureFromBase64(string plainText, string base64Text);
    byte[] GenerateSignature(byte[] plainBytes);
    bool VerifySignature(byte[] plainBytes, byte[] signatureBytes);
}