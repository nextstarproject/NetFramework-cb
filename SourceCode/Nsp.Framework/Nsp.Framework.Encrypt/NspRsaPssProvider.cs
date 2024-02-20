using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Nsp.Framework.Encrypt;

public class NspRsaPssProvider : IDisposable
{
    private int _size { get; init; } = 2048;
    private RSA? _rsa { get; init; } = null;
    private RSA? _rsaPrivate { get; init; } = null;
    private RSA? _rsaPublic { get; init; } = null;

    public NspRsaPssProvider()
    {
        _size = 2048;
        _rsa = Create();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="keySize">1024、2048、3096、</param>
    public NspRsaPssProvider(int keySize)
    {
        _size = keySize;
        _rsa = Create();
    }

    public NspRsaPssProvider(string xmlPrivateAndPublic)
    {
        var rsa = RSA.Create();
        rsa.FromXmlString(xmlPrivateAndPublic);
        _rsa = rsa;
    }

    public NspRsaPssProvider(string base64PrivateKey, string base64PublicKey)
    {
        var privateKeyBytes = Convert.FromBase64String(base64PrivateKey);
        var rsaPrivate = RSA.Create();
        rsaPrivate.ImportRSAPrivateKey(privateKeyBytes, out _);

        var publicKeyBytes = Convert.FromBase64String(base64PublicKey);
        var rsaPublic = RSA.Create();
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
    /// 
    /// </summary>
    /// <param name="notBefore">不设置默认现在</param>
    /// <param name="notAfter">不设置默认一年</param>
    /// <param name="name">设置CN名称{name}RSACertificate</param>
    /// <returns></returns>
    public X509Certificate2 ExportX509Certificate2(DateTimeOffset? notBefore = null, DateTimeOffset? notAfter = null, string name = "NextStar")
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

    private RSA Create()
    {
        var rsa = RSA.Create(_size);
        return rsa;
    }

    private byte[] SingData(RSA rsa, string data, SecurityAlgorithms algorithms)
    {
        // 将数据转换为字节数组
        var dataBytes = Encoding.UTF8.GetBytes(data);

        // 使用私钥进行签名
        var signatureBytes = rsa.SignData(dataBytes, ConvertAlgorithmName(algorithms), RSASignaturePadding.Pss);

        return signatureBytes;
    }

    private bool VerifySignature(RSA rsa, string data, string signature,
        SecurityAlgorithms algorithms)
    {
        // 将数据转换为字节数组
        var dataBytes = Encoding.UTF8.GetBytes(data);
        // 将Base64字符串的签名转换为字节数组
        var signatureBytes = Convert.FromBase64String(signature);

        // 使用私钥进行签名
        var isValid = rsa.VerifyData(dataBytes, signatureBytes, ConvertAlgorithmName(algorithms), RSASignaturePadding.Pss);
        return isValid;
    }
    
    private HashAlgorithmName ConvertAlgorithmName(SecurityAlgorithms algorithms)
    {
        return algorithms switch
        {
            SecurityAlgorithms.SHA256 => HashAlgorithmName.SHA256,
            SecurityAlgorithms.SHA384 => HashAlgorithmName.SHA384,
            SecurityAlgorithms.SHA512 => HashAlgorithmName.SHA512,
            _ => HashAlgorithmName.SHA256
        };
    }
}