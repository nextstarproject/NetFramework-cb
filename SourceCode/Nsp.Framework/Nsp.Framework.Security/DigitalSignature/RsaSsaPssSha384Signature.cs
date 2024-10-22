using System.Text;
using Nsp.Framework.Security.Asymmetric;

namespace Nsp.Framework.Security.DigitalSignature;

public class RsaSsaPssSha384Signature : RsaProvider, IRsaShaSignatureAlgorithm, ISignatureX509Algorithm
{
    public HashAlgorithmName HashAlgorithmNameSetting => HashAlgorithmName.SHA384;
    public RSASignaturePadding RSASignaturePaddingSetting => RSASignaturePadding.Pss;
    
    public RsaSsaPssSha384Signature(int size = 2048) : base(size)
    {
    }

    public RsaSsaPssSha384Signature(string xmlPrivateAndPublic) : base(xmlPrivateAndPublic)
    {
    }
    
    public RsaSsaPssSha384Signature(RSAParameters rsaParameters) : base(rsaParameters)
    {
    }

    public RsaSsaPssSha384Signature(string base64PrivateKey, string base64PublicKey) : base(base64PrivateKey, base64PublicKey)
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
        var textBytes = Encoding.UTF8.GetBytes(plainText);
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
        var textBytes = Encoding.UTF8.GetBytes(plainText);
        var signatureBytes = Convert.FromBase64String(base64Text);
        return VerifySignature(textBytes, signatureBytes);
    }

    public byte[] GenerateSignature(byte[] plainBytes)
    {
        return _rsa.SignData(plainBytes, HashAlgorithmNameSetting, RSASignaturePaddingSetting);
    }

    public bool VerifySignature(byte[] plainBytes, byte[] signatureBytes)
    {
        return _rsa.VerifyData(plainBytes, signatureBytes, HashAlgorithmNameSetting, RSASignaturePaddingSetting);
    }
    
    public X509Certificate2 ExportX509Certificate2(DateTimeOffset? notBefore = null, DateTimeOffset? notAfter = null,
        string distinguishedName = "CN=NSP")
    {
        ArgumentNullException.ThrowIfNull(_rsa);
        notBefore ??= DateTimeOffset.Now;

        notAfter ??= DateTimeOffset.Now.AddYears(1);

        // 创建 X.509 证书请求
        var certificateRequest = new CertificateRequest(
            new X500DistinguishedName(distinguishedName),
            _rsa,
            HashAlgorithmNameSetting,
            RSASignaturePaddingSetting
        );

        // 设置证书的有效期
        certificateRequest.CertificateExtensions.Add(
            new X509BasicConstraintsExtension(false, false, 0, false));
        var certificate =
            certificateRequest.CreateSelfSigned(notBefore.Value, notAfter.Value);
        return certificate;
    }
    
    public byte[] ExportPfxData(string password, DateTimeOffset? notBefore = null, DateTimeOffset? notAfter = null,
        string distinguishedName = "CN=NSP")
    {
        var certificate = ExportX509Certificate2(notBefore, notAfter, distinguishedName);
        return certificate.Export(X509ContentType.Pfx, password);
    }
    
    public byte[] ExportCerData(DateTimeOffset? notBefore = null, DateTimeOffset? notAfter = null,
        string distinguishedName = "CN=NSP")
    {
        var certificate = ExportX509Certificate2(notBefore, notAfter, distinguishedName);
        return certificate.Export(X509ContentType.Cert);
    }
}