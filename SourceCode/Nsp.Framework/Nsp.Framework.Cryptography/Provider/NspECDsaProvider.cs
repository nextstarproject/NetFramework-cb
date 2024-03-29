﻿using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Nsp.Framework.Cryptography;

/// <summary>
/// 默认使用ECParameters来导出和导入公私钥,可能无法直接用到其他地方
/// </summary>
public class NspECDsaProvider : IDisposable
{
    private ECDsa? _ecDsa { get; init; } = null;
    private ECDsa? _ecDsaPrivate { get; init; } = null;
    private ECDsa? _ecDsaPublic { get; init; } = null;
    private NspSecurityAlgorithms _algorithms { get; init; }

    public NspECDsaProvider()
    {
        _algorithms = NspSecurityAlgorithms.SHA256;
        _ecDsa = Create(NspSecurityAlgorithms.SHA256);
    }

    public NspECDsaProvider(NspSecurityAlgorithms algorithms)
    {
        _algorithms = algorithms;
        _ecDsa = Create(algorithms);
    }

    public NspECDsaProvider(string base64PrivateKey, string base64PublicKey, NspSecurityAlgorithms algorithms,
        bool isParameters = false)
    {
        // 解码Base64字符串为字节数组
        var publicKeyBytes = Convert.FromBase64String(base64PublicKey);
        var privateKeyBytes = Convert.FromBase64String(base64PrivateKey);

        if (isParameters)
        {
            var ecParameters = new ECParameters()
            {
                Q = new ECPoint()
                {
                    X = publicKeyBytes.Take(publicKeyBytes.Length / 2).ToArray(),
                    Y = publicKeyBytes.Skip(publicKeyBytes.Length / 2).ToArray(),
                },
                D = privateKeyBytes,
                Curve = ECCurve.NamedCurves.nistP256
            };

            ecParameters.Curve = algorithms switch
            {
                NspSecurityAlgorithms.SHA384 => ECCurve.NamedCurves.nistP384,
                NspSecurityAlgorithms.SHA512 => ECCurve.NamedCurves.nistP521,
                _ => ECCurve.NamedCurves.nistP256
            };

            var ecdsa = ECDsa.Create();
            ecdsa.ImportParameters(ecParameters);
            _ecDsa = ecdsa;
        }
        else
        {
            var ecdsaPrivate = ECDsa.Create();
            ecdsaPrivate.ImportPkcs8PrivateKey(privateKeyBytes, out _);
            var ecdsaPublic = ECDsa.Create();
            ecdsaPublic.ImportSubjectPublicKeyInfo(publicKeyBytes, out _);
            _ecDsaPrivate = ecdsaPrivate;
            _ecDsaPublic = ecdsaPublic;
        }

        _algorithms = algorithms;
    }

    public ECParameters ExportParameters()
    {
        if (_ecDsa == null) throw new ArgumentNullException(nameof(_ecDsa));
        return _ecDsa.ExportParameters(true);
    }

    public string GetBase64PrivateKey(bool isParameters = false)
    {
        if (isParameters)
        {
            ArgumentNullException.ThrowIfNull(_ecDsa);
            var ecParameters = _ecDsa.ExportParameters(true);
            return Convert.ToBase64String(ecParameters.D);
        }

        if (_ecDsa != null)
        {
            var privateKey = _ecDsa.ExportPkcs8PrivateKey();
            return Convert.ToBase64String(privateKey);
        }

        if (_ecDsaPrivate != null)
        {
            var privateKey = _ecDsaPrivate.ExportPkcs8PrivateKey();
            return Convert.ToBase64String(privateKey);
        }

        throw new ArgumentNullException(nameof(_ecDsa), "_ecDsa and _ecDsaPrivate is null");
    }

    public string GetBase64PublicKey(bool isParameters = false)
    {
        if (isParameters)
        {
            ArgumentNullException.ThrowIfNull(_ecDsa);
            var ecParameters = _ecDsa.ExportParameters(true);
            return Convert.ToBase64String(ecParameters.Q.X.Concat(ecParameters.Q.Y).ToArray());
        }

        if (_ecDsa != null)
        {
            var publicKey = _ecDsa.ExportSubjectPublicKeyInfo();
            return Convert.ToBase64String(publicKey);
        }

        if (_ecDsaPublic != null)
        {
            var publicKey = _ecDsaPublic.ExportSubjectPublicKeyInfo();
            return Convert.ToBase64String(publicKey);
        }

        throw new ArgumentNullException(nameof(_ecDsa), "_ecDsa and _ecDsaPublic is null");
    }

    public string SignData([NotNull] string data)
    {
        byte[] signature;
        var dataToSign = Encoding.UTF8.GetBytes(data);
        if (_ecDsa != null)
        {
            signature = _ecDsa.SignData(dataToSign, _algorithms.ConvertHashAlgorithmName());
        }
        else if (_ecDsaPrivate != null)
        {
            signature = _ecDsaPrivate.SignData(dataToSign, _algorithms.ConvertHashAlgorithmName());
        }
        else
        {
            throw new ArgumentNullException(nameof(_ecDsa), "_ecDsa and _ecDsaPrivate is null");
        }

        return Convert.ToBase64String(signature);
    }

    public bool VerifyData(string data, string signature)
    {
        var dataToSign = Encoding.UTF8.GetBytes(data);
        var signatureBytes = Convert.FromBase64String(signature);
        var isValid = false;
        if (_ecDsa != null)
        {
            isValid = _ecDsa.VerifyData(dataToSign, signatureBytes, _algorithms.ConvertHashAlgorithmName());
        }
        else if (_ecDsaPublic != null)
        {
            isValid = _ecDsaPublic.VerifyData(dataToSign, signatureBytes, _algorithms.ConvertHashAlgorithmName());
        }
        else
        {
            throw new ArgumentNullException(nameof(_ecDsa), "_ecDsa and _ecDsaPublic is null");
        }

        return isValid;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="notBefore">不设置默认现在</param>
    /// <param name="notAfter">不设置默认一年</param>
    /// <param name="name">设置CN名称{name}ECDsaCertificate</param>
    /// <returns></returns>
    [Obsolete("导出暂时有问题，请谨慎使用")]
    public X509Certificate2 ExportX509Certificate2(DateTimeOffset? notBefore = null, DateTimeOffset? notAfter = null,
        string name = "NextStar")
    {
        ArgumentNullException.ThrowIfNull(_ecDsa);
        notBefore ??= DateTimeOffset.Now;

        notAfter ??= DateTimeOffset.Now.AddYears(1);

        // 创建 X.509 证书请求
        var certificateRequest = new CertificateRequest(
            new X500DistinguishedName($"CN={name}ECDsaCertificate"),
            _ecDsa,
            _algorithms.ConvertHashAlgorithmName()
        );

        var certificate =
            certificateRequest.CreateSelfSigned(notBefore.Value, notAfter.Value);
        return certificate;
    }

    public (ECDsaSecurityKey ecDsaSecurityKey, string ecdsaAlgorithms) ExportSecurityKey(string? keyId = null)
    {
        ArgumentNullException.ThrowIfNull(_ecDsa);
        var ecdsaSecurityKey = new ECDsaSecurityKey(_ecDsa)
        {
            KeyId = keyId ?? Guid.NewGuid().ToString()
        };
        var ecdsaAlgorithms = _algorithms.ConvertEcdsaSecurityAlgorithms();
        return (ecdsaSecurityKey, ecdsaAlgorithms);
    }

    public void Dispose()
    {
        _ecDsa?.Dispose();
        _ecDsaPublic?.Dispose();
        _ecDsaPrivate?.Dispose();
    }

    #region Private Method

    private ECDsa Create(NspSecurityAlgorithms algorithms)
    {
        return algorithms switch
        {
            NspSecurityAlgorithms.SHA256 => ECDsa.Create(ECCurve.NamedCurves.nistP256),
            NspSecurityAlgorithms.SHA384 => ECDsa.Create(ECCurve.NamedCurves.nistP384),
            NspSecurityAlgorithms.SHA512 => ECDsa.Create(ECCurve.NamedCurves.nistP521),
            _ => ECDsa.Create(ECCurve.NamedCurves.nistP256)
        };
    }

    #endregion
}