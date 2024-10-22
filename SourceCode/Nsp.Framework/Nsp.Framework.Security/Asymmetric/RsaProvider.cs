namespace Nsp.Framework.Security.Asymmetric;

/// <summary>
/// 请使用 <see cref="RsaPKCS1"/> 或者 <see cref="RsaOAEP"/>
/// </summary>
public class RsaProvider : IRsaProvider, IDisposable
{
    public RSA Rsa => _rsa;
    protected readonly int _size;
    protected readonly RSA _rsa;

    public RsaProvider(int size = 2048)
    {
        if (IRsaProvider.KeySizeCollection.Contains(size))
        {
            _size = size;
            _rsa = IRsaProvider.Create(_size);
        }
        else
        {
            throw new ArgumentException("size must 1024,2048,3072,4096", nameof(size));
        }
    }

    public RsaProvider(string xmlPrivateAndPublic)
    {
        var rsa = RSA.Create();
        rsa.FromXmlString(xmlPrivateAndPublic);
        _rsa = rsa;
    }
    
    public RsaProvider(RSAParameters rsaParameters)
    {
        var rsa = RSA.Create();
        rsa.ImportParameters(rsaParameters);
        _rsa = rsa;
    }
    
    public RsaProvider(X509Certificate2 certificate2)
    {
        var rsaPrivateKey = certificate2.GetRSAPrivateKey();

        // 提取公钥
        var rsaPublicKey = certificate2.GetRSAPublicKey();

        var rsa = RSA.Create();
        if (rsaPublicKey != null)
        {
            rsa.ImportParameters(rsaPublicKey.ExportParameters(false));
        }
        
        if (rsaPrivateKey != null)
        {
            rsa.ImportParameters(rsaPrivateKey.ExportParameters(true));
        }
        _rsa = rsa;
    }

    public RsaProvider(string base64PrivateKey, string base64PublicKey)
    {
        var rsa = RSA.Create();
        if (!string.IsNullOrWhiteSpace(base64PublicKey))
        {
            var publicKeyBytes = Convert.FromBase64String(base64PublicKey);
            rsa.ImportSubjectPublicKeyInfo(publicKeyBytes, out _);
        }
        
        if (!string.IsNullOrWhiteSpace(base64PrivateKey))
        {
            var privateKeyBytes = Convert.FromBase64String(base64PrivateKey);
            rsa.ImportPkcs8PrivateKey(privateKeyBytes, out _);
        }
        
        _rsa = rsa;
    }

    public string ExportPublicToPem()
    {
        return _rsa.ExportSubjectPublicKeyInfoPem();
    }

    public string ExportPrivateToPem()
    {
        return _rsa.ExportPkcs8PrivateKeyPem();
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

    public AsymmetricKey ExportPublicAndPrivateToBase64()
    {
        return new AsymmetricKey()
        {
            PublicKey = ExportPublicToBase64(),
            PrivateKey = ExportPrivateToBase64()
        };
    }
    
    public RSAParameters ExportParameters(bool includePrivateParameters = true)
    {
        return _rsa.ExportParameters(includePrivateParameters);
    }

    public void Dispose()
    {
        _rsa?.Dispose();
    }
}