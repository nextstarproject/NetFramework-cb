namespace Nsp.Framework.Security.Mac;

public interface IHmacShaSignature
{
    string GenerateSignature(string plainText);
    bool VerifySignature(string plainText, string hmacText);
    string GenerateSignatureToHex(string plainText);
    bool VerifySignatureFromHex(string plainText, string hexText);
    string GenerateSignatureToBase64(string plainText);
    bool VerifySignatureFromBase64(string plainText, string base64Text);
}