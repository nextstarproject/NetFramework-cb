namespace Nsp.Framework.Security.DigitalSignature;

public class EcDsaProvider : IEcDsaProvider
{
    public ECDsa EcDsa => _ecDsa;
    public ECCurve Curve => _curve;

    protected readonly ECDsa _ecDsa;
    protected readonly ECCurve _curve;

    public EcDsaProvider(ECCurve ecCurve)
    {
        _curve = ecCurve;
        _ecDsa = IEcDsaProvider.Create(_curve);
    }

    public EcDsaProvider(ECParameters ecParameters)
    {
        var ecdsa = ECDsa.Create();
        ecdsa.ImportParameters(ecParameters);
        _ecDsa = ecdsa;
    }

    public EcDsaProvider(string base64PrivateKey, string base64PublicKey)
    {
        var ecdsa = ECDsa.Create();
        if (!string.IsNullOrWhiteSpace(base64PublicKey))
        {
            var publicKeyBytes = Convert.FromBase64String(base64PublicKey);
            ecdsa.ImportSubjectPublicKeyInfo(publicKeyBytes, out _);
        }

        if (!string.IsNullOrWhiteSpace(base64PrivateKey))
        {
            var privateKeyBytes = Convert.FromBase64String(base64PrivateKey);
            ecdsa.ImportPkcs8PrivateKey(privateKeyBytes, out _);
        }

        _ecDsa = ecdsa;
    }

    public string ExportPublicToPem()
    {
        return _ecDsa.ExportSubjectPublicKeyInfoPem();
    }

    public string ExportPrivateToPem()
    {
        return _ecDsa.ExportPkcs8PrivateKeyPem();
    }

    public string ExportPublicToBase64()
    {
        return IEcDsaProvider.ExportPublicToBase64(_ecDsa);
    }

    public string ExportPrivateToBase64()
    {
        return IEcDsaProvider.ExportPrivateToBase64(_ecDsa);
    }

    public ECParameters ExportParameters(bool isIncludePrivateKey = true)
    {
        return _ecDsa.ExportParameters(isIncludePrivateKey);
    }
}