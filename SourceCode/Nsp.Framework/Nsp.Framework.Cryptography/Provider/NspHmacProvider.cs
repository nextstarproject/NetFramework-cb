using System.Text;
using Microsoft.IdentityModel.Tokens;
using Nsp.Framework.Core;

namespace Nsp.Framework.Cryptography;

public class NspHmacProvider : IDisposable
{
    private readonly NspSecurityAlgorithms _algorithms = NspSecurityAlgorithms.SHA256;
    private readonly byte[] _secretKey;
    private readonly HMAC _hmac;

    public NspHmacProvider(string secretKey)
    {
        _secretKey = Encoding.UTF8.GetBytes(secretKey);
        _algorithms = NspSecurityAlgorithms.SHA256;
        _hmac = ConvertHmac();
    }

    public NspHmacProvider(string secretKey, NspSecurityAlgorithms algorithms)
    {
        _secretKey = Encoding.UTF8.GetBytes(secretKey);
        _algorithms = algorithms;
        _hmac = ConvertHmac();
    }

    public string SignData([NotNull] string data)
    {
        var dataToSign = Encoding.UTF8.GetBytes(data);
        var hashBytes = _hmac.ComputeHash(dataToSign);
        var signature = Convert.ToBase64String(hashBytes);
        return signature;
    }

    public bool VerifyData([NotNull] string data, [NotNull] string signature)
    {
        var dataToSign = Encoding.UTF8.GetBytes(data);
        var hashBytes = _hmac.ComputeHash(dataToSign);
        var currentSignature = Convert.ToBase64String(hashBytes);
        return string.Equals(currentSignature, signature);
    }

    public (SymmetricSecurityKey hmacSecurityKey, string hmacAlgorithms) ExportSecurityKey(string? keyId = null)
    {
        var securityKey = new SymmetricSecurityKey(GenerateKey());
        return (securityKey, _algorithms.ConvertHmacSecurityAlgorithms());
    }
    
    public void Dispose()
    {
        _hmac?.Dispose();
    }

    private byte[] GenerateKey()
    {
        var tempSecret = Encoding.UTF8.GetBytes(StringConst.TempSecret512);
        var newBytes = _secretKey.Concat(tempSecret).ToArray();
        return newBytes[..512];
    }
    
    private HMAC ConvertHmac()
    {
        return _algorithms switch
        {
            NspSecurityAlgorithms.SHA384 => new HMACSHA384(_secretKey),
            NspSecurityAlgorithms.SHA512 => new HMACSHA512(_secretKey),
            _ => new HMACSHA256(_secretKey)
        };
    }
}