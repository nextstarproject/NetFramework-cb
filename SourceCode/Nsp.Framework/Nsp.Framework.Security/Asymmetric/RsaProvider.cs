namespace Nsp.Framework.Security.Asymmetric;

public class RsaProvider : IRsaProvider,IDisposable
{
    public RSACryptoServiceProvider Rsa => _rsa;
    protected readonly int _size;
    protected readonly RSACryptoServiceProvider _rsa;

    public RsaProvider(int size = 2048)
    {
        if (IRsaProvider.KeySizeCollection.Contains(size))
        {
            _size = size;
            _rsa = IRsaProvider.Create(size);
        }
        else
        {
            throw new ArgumentException("size must 1024,2048,3072,4096", nameof(size));
        }
    }
    
    public RsaProvider(string xmlPrivateAndPublic)
    {
        var rsa = new RSACryptoServiceProvider();
        rsa.FromXmlString(xmlPrivateAndPublic);
        _rsa = rsa;
    }

    public RsaProvider(string base64PrivateKey, string base64PublicKey)
    {
        var privateKeyBytes = Convert.FromBase64String(base64PrivateKey);
        var rsaPrivate = new RSACryptoServiceProvider();
        var publicKeyBytes = Convert.FromBase64String(base64PublicKey);
        var rsa = new RSACryptoServiceProvider();
        rsaPrivate.ImportPkcs8PrivateKey(privateKeyBytes, out _);
        rsaPrivate.ImportSubjectPublicKeyInfo(publicKeyBytes, out _);
        _rsa = rsa;
    }
    
    public string ExportPublicToPem()
    {
        return IRsaProvider.ExportPublicKeyToPem(_rsa);
    }

    public string ExportPrivateToPem()
    {
        return IRsaProvider.ExportPrivateKeyToPem(_rsa);
    }

    public string ExportPublicToBase64()
    {
        return IRsaProvider.ExportPublicToBase64(_rsa);
    }

    public string ExportPrivateToBase64()
    {
        return IRsaProvider.ExportPrivateToBase64(_rsa);
    }

    public string ExportXmlPublicAndPrivate(bool isIncludePrivate = true)
    {
        return IRsaProvider.ExportXmlPublicAndPrivate(_rsa, isIncludePrivate);
    }
    
    public void Dispose()
    {
        _rsa?.Dispose();
    }
}