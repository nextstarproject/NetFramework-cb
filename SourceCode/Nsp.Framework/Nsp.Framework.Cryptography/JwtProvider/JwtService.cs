using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace Nsp.Framework.Cryptography;

public class JwtService
{
    private readonly SigningCredentials? _signingCredentials;
    private readonly X509Certificate2? _certificate;

    public JwtService(SigningCredentials signingCredentials)
    {
        // 加载证书
        _signingCredentials = signingCredentials;
    }
    
    public JwtService(ECDsaSecurityKey ecDsaSecurityKey)
    {
        _signingCredentials = new SigningCredentials(ecDsaSecurityKey, SecurityAlgorithms.EcdsaSha256);
    }
    
    public JwtService((ECDsaSecurityKey ecDsaSecurityKey, string ecdsaAlgorithms) ecDsaSecurity)
    {
        _signingCredentials = new SigningCredentials(ecDsaSecurity.ecDsaSecurityKey, ecDsaSecurity.ecdsaAlgorithms);
    }
    
    public JwtService((SymmetricSecurityKey securityKey, string algorithms) security)
    {
        _signingCredentials = new SigningCredentials(security.securityKey, security.algorithms);
    }
    
    public JwtService(RsaSecurityKey rsaSecurityKey)
    {
        _signingCredentials = new SigningCredentials(rsaSecurityKey, SecurityAlgorithms.RsaSha256);
    }
    
    public JwtService(RsaSecurityKey rsaSecurityKey, string rsaAlgorithms)
    {
        _signingCredentials = new SigningCredentials(rsaSecurityKey, rsaAlgorithms);
    }
    
    public JwtService(X509Certificate2 certificate)
    {
        // 加载证书
        _certificate = new X509Certificate2(certificate);
    }

    public JwtService(string certificatePath, string certificatePassword)
    {
        // 加载证书
        _certificate = new X509Certificate2(certificatePath, certificatePassword);
    }

    public string GenerateToken(DateTime validFrom, DateTime validTo,
        Dictionary<string, string>? customClaimsDic = null)
    {
        return GenerateToken("subject", validFrom, validTo, "nextstar", "user", Guid.NewGuid().ToString(),
            customClaimsDic);
    }

    public string GenerateToken(DateTime validFrom, DateTime validTo, string uniqueIdentityId,
        Dictionary<string, string>? customClaimsDic = null)
    {
        return GenerateToken("subject", validFrom, validTo, "nextstar", "user", uniqueIdentityId, customClaimsDic);
    }

    public string GenerateToken(string subject, DateTime validFrom, DateTime validTo, string uniqueIdentityId,
        Dictionary<string, string>? customClaimsDic = null)
    {
        return GenerateToken(subject, validFrom, validTo, "nextstar", "user", uniqueIdentityId, customClaimsDic);
    }

    public string GenerateToken(string subject, DateTime validFrom, DateTime validTo, string issuer, string audience,
        string uniqueIdentityId, Dictionary<string, string>? customClaimsDic = null)
    {
        if (_signingCredentials == null && _certificate == null)
        {
            throw new ArgumentNullException(nameof(_signingCredentials)+nameof(_certificate), "must signingCredentials or certificate");
        }
        
        // 创建一些声明
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, subject),
            new Claim(JwtRegisteredClaimNames.Jti, uniqueIdentityId),
            new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.Now.ToUnixTimeSeconds().ToString(),
                ClaimValueTypes.Integer64),
        };
        if (customClaimsDic != null)
        {
            var customClaims = customClaimsDic.Select(x => new Claim(x.Key, x.Value));
            claims = claims.Concat(customClaims).ToArray();
        }

        // 创建JWT token
        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            notBefore: validFrom,
            expires: validTo,
            signingCredentials: _signingCredentials ?? new X509SigningCredentials(_certificate)
        );

        var handler = new JwtSecurityTokenHandler();
        return handler.WriteToken(token);
    }

    public bool ValidateToken(string token)
    {
        return ValidateToken(token, "nextstar", "user");
    }

    public bool ValidateToken(string token, string issuer, string audience)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var validationParameters = GetTokenValidationParameters(issuer, audience);
        var result = tokenHandler.ValidateTokenAsync(token, validationParameters).GetAwaiter().GetResult();
        return result.IsValid;
    }

    public async Task<bool> ValidateTokenAsync(string token)
    {
        return await ValidateTokenAsync(token, "nextstar", "user");
    }

    public async Task<bool> ValidateTokenAsync(string token, string issuer, string audience)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var validationParameters = GetTokenValidationParameters(issuer, audience);
        var result = await tokenHandler.ValidateTokenAsync(token, validationParameters);
        return result.IsValid;
    }

    public Dictionary<string, string> GetAllClaims(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        if (tokenHandler.ReadToken(token) is JwtSecurityToken {Claims: not null} jsonToken)
        {
            // 获取所有的claims
            var claims = jsonToken.Claims;

            var dic = claims.ToDictionary(x => x.Type, x => x.Value);
            return dic;
        }

        throw new ArgumentException($"jwt token error not parser claims");
    }

    private TokenValidationParameters GetTokenValidationParameters(string issuer, string audience)
    {
        if (_signingCredentials == null && _certificate == null)
        {
            throw new ArgumentNullException(nameof(_signingCredentials)+nameof(_certificate), "must signingCredentials or certificate");
        }
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = issuer,
            ValidateAudience = true,
            ValidAudience = audience,
            ValidateLifetime = true,
            IssuerSigningKey = _signingCredentials?.Key ?? new X509SecurityKey(_certificate),
            ValidateIssuerSigningKey = true
        };
        return validationParameters;
    }
    
}