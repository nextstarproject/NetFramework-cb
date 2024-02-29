using System.Text;

namespace Nsp.Framework.Cryptography;

public class NspRsaProvider : IDisposable
{
    private IReadOnlyCollection<int> keySizeCollection => new List<int>() {1024, 2048, 3072, 4096}.AsReadOnly();
    private int _size { get; init; } = 2048;
    private RSACryptoServiceProvider? _rsa { get; init; } = null;
    private RSACryptoServiceProvider? _rsaPrivate { get; init; } = null;
    private RSACryptoServiceProvider? _rsaPublic { get; init; } = null;

    public NspRsaProvider()
    {
        _size = 2048;
        _rsa = Create();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="keySize">1024、2048、3096、</param>
    public NspRsaProvider(int keySize)
    {
        if (!keySizeCollection.Contains(keySize))
        {
            throw new ArgumentException("keySize must in 1024,2048,3072,4096");
        }

        _size = keySize;
        _rsa = Create();
    }

    public NspRsaProvider(string xmlPrivateAndPublic)
    {
        var rsa = new RSACryptoServiceProvider();
        rsa.FromXmlString(xmlPrivateAndPublic);
        _rsa = rsa;
    }

    public NspRsaProvider(string base64PrivateKey, string base64PublicKey)
    {
        var privateKeyBytes = Convert.FromBase64String(base64PrivateKey);
        var rsaPrivate = new RSACryptoServiceProvider();
        rsaPrivate.ImportRSAPrivateKey(privateKeyBytes, out _);

        var publicKeyBytes = Convert.FromBase64String(base64PublicKey);
        var rsaPublic = new RSACryptoServiceProvider();
        rsaPublic.ImportRSAPublicKey(publicKeyBytes, out _);

        _rsaPrivate = rsaPrivate;
        _rsaPublic = rsaPublic;
    }

    public string ExportXmlPublicAndPrivate()
    {
        ArgumentNullException.ThrowIfNull(_rsa);
        // 获取XML格式的私钥和公钥
        var privateKeyAndPublicKeyXml = _rsa.ToXmlString(true);
        return privateKeyAndPublicKeyXml;
    }

    public string GetBase64PrivateKey()
    {
        if (_rsa != null)
        {
            var privateKey = _rsa.ExportRSAPrivateKey();
            var privateKeyString = Convert.ToBase64String(privateKey);
            return privateKeyString;
        }

        if (_rsaPrivate != null)
        {
            var privateKey = _rsaPrivate.ExportRSAPrivateKey();
            var privateKeyString = Convert.ToBase64String(privateKey);
            return privateKeyString;
        }

        throw new ArgumentNullException(nameof(_rsa), "_rsa and _rsaPrivate is null");
    }

    public string GetBase64PublicKey()
    {
        if (_rsa != null)
        {
            var privateKey = _rsa.ExportRSAPublicKey();
            var privateKeyString = Convert.ToBase64String(privateKey);
            return privateKeyString;
        }

        if (_rsaPublic != null)
        {
            var publicKey = _rsaPublic.ExportRSAPublicKey();
            var publicKeyString = Convert.ToBase64String(publicKey);
            return publicKeyString;
        }

        throw new ArgumentNullException(nameof(_rsa), "_rsa and _rsaPrivate is null");
    }

    public string SignData([NotNull] string data, SecurityAlgorithms algorithms = SecurityAlgorithms.SHA256)
    {
        byte[] signatureBytes;
        var dataToSign = Encoding.UTF8.GetBytes(data);
        if (_rsa != null)
        {
            signatureBytes = SingData(_rsa, data, algorithms);
        }
        else if (_rsaPrivate != null)
        {
            signatureBytes = SingData(_rsaPrivate, data, algorithms);
        }
        else
        {
            throw new ArgumentNullException(nameof(_rsa), "_rsa and _rsaPrivate is null");
        }

        return Convert.ToBase64String(signatureBytes);
    }

    public bool VerifyData(string data, string signature, SecurityAlgorithms algorithms = SecurityAlgorithms.SHA256)
    {
        var isValid = false;
        if (_rsa != null)
        {
            isValid = VerifySignature(_rsa, data, signature, algorithms);
        }
        else if (_rsaPublic != null)
        {
            isValid = VerifySignature(_rsaPublic, data, signature, algorithms);
        }
        else
        {
            throw new ArgumentNullException(nameof(_rsa), "_rsa and _rsaPublic is null");
        }

        return isValid;
    }

    /// <summary>
    /// 公钥加密
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public string Encrypt(string data)
    {
        // 将原始数据转换为字节数组
        var originalBytes = Encoding.UTF8.GetBytes(data);

        byte[] encryptedBytes;
        if (_rsa != null)
        {
            encryptedBytes = _rsa.Encrypt(originalBytes, false);
        }
        else if (_rsaPublic != null)
        {
            encryptedBytes = _rsaPublic.Encrypt(originalBytes, false);
        }
        else
        {
            throw new ArgumentNullException(nameof(_rsa), "_rsa and _rsaPublic is null");
        }

        return Convert.ToBase64String(encryptedBytes);
    }

    /// <summary>
    /// 私钥解密
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public string Decrypt(string data)
    {
        var contentBytes = Convert.FromBase64String(data);

        byte[] decryptedBytes;
        if (_rsa != null)
        {
            decryptedBytes = _rsa.Decrypt(contentBytes, false);
        }
        else if (_rsaPrivate != null)
        {
            decryptedBytes = _rsaPrivate.Decrypt(contentBytes, false);
        }
        else
        {
            throw new ArgumentNullException(nameof(_rsa), "_rsa and _rsaPublic is null");
        }

        return Encoding.UTF8.GetString(decryptedBytes);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="notBefore">不设置默认现在</param>
    /// <param name="notAfter">不设置默认一年</param>
    /// <param name="name">设置CN名称{name}RSACertificate</param>
    /// <returns></returns>
    public X509Certificate2 ExportX509Certificate2(DateTimeOffset? notBefore = null, DateTimeOffset? notAfter = null,
        string name = "NextStar")
    {
        ArgumentNullException.ThrowIfNull(_rsa);
        notBefore ??= DateTimeOffset.Now;

        notAfter ??= DateTimeOffset.Now.AddYears(1);

        // 创建 X.509 证书请求
        var certificateRequest = new CertificateRequest(
            new X500DistinguishedName($"CN={name}RSACertificate"),
            _rsa,
            HashAlgorithmName.SHA256,
            RSASignaturePadding.Pkcs1
        );

        // 设置证书的有效期
        certificateRequest.CertificateExtensions.Add(
            new X509BasicConstraintsExtension(false, false, 0, false));
        var certificate =
            certificateRequest.CreateSelfSigned(notBefore.Value, notAfter.Value);
        return certificate;
    }

    public void Dispose()
    {
        _rsa?.Dispose();
        _rsaPrivate?.Dispose();
        _rsaPublic?.Dispose();
    }

    private RSACryptoServiceProvider Create()
    {
        var rsa = new RSACryptoServiceProvider(_size);
        return rsa;
    }

    private byte[] SingData(RSACryptoServiceProvider rsa, string data, SecurityAlgorithms algorithms)
    {
        // 将数据转换为字节数组
        var dataBytes = Encoding.UTF8.GetBytes(data);

        var hash = algorithms switch
        {
            SecurityAlgorithms.SHA384 => SHA384.Create().ComputeHash(dataBytes),
            SecurityAlgorithms.SHA512 => SHA512.Create().ComputeHash(dataBytes),
            _ => SHA256.Create().ComputeHash(dataBytes)
        };

        // 使用私钥进行签名
        var signatureBytes = rsa.SignHash(hash, CryptoConfig.MapNameToOID(ConvertAlgorithmName(algorithms)));

        return signatureBytes;
    }

    private bool VerifySignature(RSACryptoServiceProvider rsa, string data, string signature,
        SecurityAlgorithms algorithms)
    {
        // 将数据转换为字节数组
        var dataBytes = Encoding.UTF8.GetBytes(data);

        var hash = algorithms switch
        {
            SecurityAlgorithms.SHA384 => SHA384.Create().ComputeHash(dataBytes),
            SecurityAlgorithms.SHA512 => SHA512.Create().ComputeHash(dataBytes),
            _ => SHA256.Create().ComputeHash(dataBytes)
        };

        // 将Base64字符串的签名转换为字节数组
        var signatureBytes = Convert.FromBase64String(signature);

        // 使用私钥进行签名
        var isValid = rsa.VerifyHash(hash, CryptoConfig.MapNameToOID(ConvertAlgorithmName(algorithms)), signatureBytes);

        return isValid;
    }

    private string ConvertAlgorithmName(SecurityAlgorithms algorithms)
    {
        return algorithms switch
        {
            SecurityAlgorithms.SHA384 => "SHA384",
            SecurityAlgorithms.SHA512 => "SHA512",
            _ => "SHA256"
        };
    }
}