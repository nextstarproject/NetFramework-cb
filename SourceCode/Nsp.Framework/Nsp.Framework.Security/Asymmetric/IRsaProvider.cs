using System.Text;

namespace Nsp.Framework.Security.Asymmetric;

public interface IRsaProvider
{
    RSA Rsa { get; }
    string ExportPublicToPem();
    string ExportPrivateToPem();
    string ExportPublicToBase64();
    string ExportPrivateToBase64();
    string ExportXmlPublicAndPrivate(bool isIncludePrivate = true);
    AsymmetricKey ExportPublicAndPrivateToBase64();
    RSAParameters ExportParameters(bool includePrivateParameters = true);
    static IReadOnlyCollection<int> KeySizeCollection => new List<int>() {1024, 2048, 3072, 4096}.AsReadOnly();

    public static RSA Create(int size = 2048)
    {
        var rsa = RSA.Create(size);
        return rsa;
    }

    public static string ExportPublicToBase64([NotNull] RSA rsa)
    {
        ArgumentNullException.ThrowIfNull(rsa);
        var publicKey = rsa.ExportSubjectPublicKeyInfo();
        var publicKeyString = Convert.ToBase64String(publicKey);
        return publicKeyString;
    }

    public static string ExportPrivateToBase64([NotNull] RSA rsa)
    {
        ArgumentNullException.ThrowIfNull(rsa);
        var privateKey = rsa.ExportPkcs8PrivateKey();
        var privateKeyString = Convert.ToBase64String(privateKey);
        return privateKeyString;
    }

    public static string ExportPublicKeyToPem([NotNull] RSA rsa)
    {
        ArgumentNullException.ThrowIfNull(rsa);
        var publicKeyBytes = rsa.ExportSubjectPublicKeyInfo();
        return ConvertToPem(publicKeyBytes, true);
    }

    public static string ExportPrivateKeyToPem([NotNull] RSA rsa)
    {
        ArgumentNullException.ThrowIfNull(rsa);
        var privateKeyBytes = rsa.ExportPkcs8PrivateKey();
        return ConvertToPem(privateKeyBytes, false);
    }

    public static string ExportXmlPublicAndPrivate([NotNull] RSA rsa, bool isIncludePrivate = true)
    {
        ArgumentNullException.ThrowIfNull(rsa);
        // 获取XML格式的私钥和公钥
        var privateKeyAndPublicKeyXml = rsa.ToXmlString(isIncludePrivate);
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