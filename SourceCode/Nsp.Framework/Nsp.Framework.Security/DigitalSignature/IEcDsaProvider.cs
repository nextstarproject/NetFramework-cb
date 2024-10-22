using System.Text;
using Nsp.Framework.Security.Asymmetric;

namespace Nsp.Framework.Security.DigitalSignature;

public interface IEcDsaProvider
{
    ECDsa EcDsa { get; }
    ECCurve Curve { get; }
    string ExportPublicToPem();
    string ExportPrivateToPem();
    string ExportPublicToBase64();
    string ExportPrivateToBase64();
    ECParameters ExportParameters(bool isIncludePrivateKey = true);
    ECDsaSecurityKey ExportSecurityKey(string? keyId);

    public static ECDsa Create(ECCurve curve)
    {
        var ecdsa = ECDsa.Create(curve);
        return ecdsa;
    }

    public static string ExportPublicToBase64([NotNull] ECDsa ecDsa)
    {
        ArgumentNullException.ThrowIfNull(ecDsa);
        var publicKey = ecDsa.ExportSubjectPublicKeyInfo();
        var publicKeyString = Convert.ToBase64String(publicKey);
        return publicKeyString;
    }

    public static string ExportPrivateToBase64([NotNull] ECDsa ecDsa)
    {
        ArgumentNullException.ThrowIfNull(ecDsa);
        var privateKey = ecDsa.ExportPkcs8PrivateKey();
        var privateKeyString = Convert.ToBase64String(privateKey);
        return privateKeyString;
    }

    public static string ExportPublicKeyToPem([NotNull] ECDsa ecDsa)
    {
        ArgumentNullException.ThrowIfNull(ecDsa);
        var publicKeyBytes = ecDsa.ExportSubjectPublicKeyInfo();
        return ConvertToPem(publicKeyBytes, true);
    }

    public static string ExportPrivateKeyToPem([NotNull] ECDsa ecDsa)
    {
        ArgumentNullException.ThrowIfNull(ecDsa);
        var privateKeyBytes = ecDsa.ExportPkcs8PrivateKey();
        return ConvertToPem(privateKeyBytes, false);
    }

    public static string ExportXmlPublicAndPrivate([NotNull] ECDsa ecDsa, bool isIncludePrivate = true)
    {
        ArgumentNullException.ThrowIfNull(ecDsa);
        // 获取XML格式的私钥和公钥
        var privateKeyAndPublicKeyXml = ecDsa.ToXmlString(isIncludePrivate);
        return privateKeyAndPublicKeyXml;
    }

    public static string ConvertToPem(byte[] keyBytes, bool isPublic)
    {
        var base64Key = Convert.ToBase64String(keyBytes);
        var sb = new StringBuilder();
        sb.AppendLine(isPublic ? IAsymmetricAlgorithm.BeginPublicKey : IAsymmetricAlgorithm.BeginPrivateKey);

        for (int i = 0; i < base64Key.Length; i += 64)
        {
            sb.AppendLine(base64Key.Substring(i, Math.Min(64, base64Key.Length - i)));
        }

        sb.AppendLine(isPublic ? IAsymmetricAlgorithm.EndPublicKey : IAsymmetricAlgorithm.EndPrivateKey);

        return sb.ToString();
    }
}