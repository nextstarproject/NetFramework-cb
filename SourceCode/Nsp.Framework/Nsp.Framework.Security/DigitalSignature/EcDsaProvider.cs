namespace Nsp.Framework.Security.DigitalSignature;

public class EcDsaProvider : IEcDsaProvider
{
    public ECDsa EcDsa => _ecDsa;
    public ECCurve Curve => _curve;
    
    public static IReadOnlyCollection<ECCurve> KeyCurves => new List<ECCurve>()
        {ECCurve.NamedCurves.nistP256, ECCurve.NamedCurves.nistP384, ECCurve.NamedCurves.nistP521}.AsReadOnly();

    protected readonly ECDsa _ecDsa;
    protected readonly ECCurve _curve;
    
    public EcDsaProvider(ECCurve ecCurve)
    {
        if (KeyCurves.Contains(ecCurve))
        {
            _curve = ecCurve;
            _ecDsa = IEcDsaProvider.Create(_curve);
        }
        else
        {
            throw new ArgumentException("size must nistP256, nistP384, nistP521", nameof(ecCurve));
        }
    }

    public EcDsaProvider(ECParameters ecParameters)
    {
        var ecdsa = ECDsa.Create();
        ecdsa.ImportParameters(ecParameters);
        _ecDsa = ecdsa;
    }

    public EcDsaProvider(string base64PrivateKey, string base64PublicKey)
    {
        var privateKeyBytes = Convert.FromBase64String(base64PrivateKey);
        var publicKeyBytes = Convert.FromBase64String(base64PublicKey);
        var ecdsa = ECDsa.Create();
        ecdsa.ImportPkcs8PrivateKey(privateKeyBytes, out _);
        ecdsa.ImportSubjectPublicKeyInfo(publicKeyBytes, out _);
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

    public ECParameters ExportPublicAndPrivateToBase64(bool isIncludePrivateKey = true)
    {
        return _ecDsa.ExportParameters(isIncludePrivateKey);
    }
}