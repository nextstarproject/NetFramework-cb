using System.Text;
using Nsp.Framework.Security.Asymmetric;

namespace Nsp.Framework.Security.DigitalSignature;

public class EcdsaSha512Signature : EcDsaProvider, IEcdsaShaSignature
{
    public HashAlgorithmName HashAlgorithmNameSetting => HashAlgorithmName.SHA512;
    
    public EcdsaSha512Signature() : base(ECCurve.NamedCurves.nistP521)
    {
    }

    public EcdsaSha512Signature(ECParameters ecParameters) : base(ecParameters)
    {
    }

    public EcdsaSha512Signature(string base64PrivateKey, string base64PublicKey) : base(base64PrivateKey,
        base64PublicKey)
    {
    }
    
    public string GenerateSignature(string plainText)
    {
        var bytes = Encoding.UTF8.GetBytes(plainText);
        return Encoding.UTF8.GetString(GenerateSignature(bytes));
    }

    public bool VerifySignature(string plainText, string signatureText)
    {
        var textBytes = Encoding.UTF8.GetBytes(plainText);
        var signatureBytes = Encoding.UTF8.GetBytes(signatureText);
        return VerifySignature(textBytes, signatureBytes);
    }

    public string GenerateSignatureToHex(string plainText)
    {
        var bytes = Encoding.UTF8.GetBytes(plainText);
        return SecurityUtil.BytesToHexString(GenerateSignature(bytes));
    }

    public bool VerifySignatureFromHex(string plainText, string hexText)
    {
        var textBytes = Convert.FromBase64String(plainText);
        var signatureBytes = SecurityUtil.HexStringToByte(hexText);
        return VerifySignature(textBytes, signatureBytes);
    }

    public string GenerateSignatureToBase64(string plainText)
    {
        var bytes = Encoding.UTF8.GetBytes(plainText);
        return Convert.ToBase64String(GenerateSignature(bytes));
    }

    public bool VerifySignatureFromBase64(string plainText, string base64Text)
    {
        var textBytes = Convert.FromBase64String(plainText);
        var signatureBytes = Convert.FromBase64String(base64Text);
        return VerifySignature(textBytes, signatureBytes);
    }

    public byte[] GenerateSignature(byte[] plainBytes)
    {
        return _ecDsa.SignData(plainBytes, HashAlgorithmNameSetting);
    }

    public bool VerifySignature(byte[] plainBytes, byte[] signatureBytes)
    {
        return _ecDsa.VerifyData(plainBytes, signatureBytes, HashAlgorithmNameSetting);
    }
}