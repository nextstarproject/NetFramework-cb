using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Nsp.Framework.Security.Jwt;

public class JwtProvider : IJwtProvider
{
    public SigningCredentials Credentials => _signingCredentials;

    private readonly SigningCredentials _signingCredentials;

    public JwtProvider(SigningCredentials signingCredentials)
    {
        // 加载证书
        _signingCredentials = signingCredentials;
    }

    #region GenerateToken

    public string GenerateToken(DateTime notBefore, DateTime expires,
        Dictionary<string, string>? customClaimsDic = null)
    {
        return GenerateToken(Guid.NewGuid().ToString(), notBefore, expires, Guid.NewGuid().ToString(), "nextstar",
            "user", customClaimsDic);
    }

    public string GenerateToken(DateTime notBefore, DateTime expires, string uniqueIdentityId,
        Dictionary<string, string>? customClaimsDic = null)
    {
        return GenerateToken(Guid.NewGuid().ToString(), notBefore, expires, uniqueIdentityId, "nextstar", "user",
            customClaimsDic);
    }

    public string GenerateToken(string subject, DateTime notBefore, DateTime expires, string uniqueIdentityId,
        Dictionary<string, string>? customClaimsDic = null)
    {
        return GenerateToken(subject, notBefore, expires, uniqueIdentityId, "nextstar", "user", customClaimsDic);
    }

    public string GenerateToken(string subject, DateTime notBefore, DateTime expires, string uniqueIdentityId,
        string issuer, string audience, Dictionary<string, string>? customClaimsDic = null)
    {
        if (_signingCredentials == null)
        {
            throw new ArgumentNullException(nameof(_signingCredentials), "must have a Credentials signingCredentials.");
        }

        // 创建一些声明
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, subject),
            new Claim(JwtRegisteredClaimNames.Jti, uniqueIdentityId),
            new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
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
            notBefore: notBefore,
            expires: expires,
            signingCredentials: _signingCredentials
        );

        var handler = new JwtSecurityTokenHandler();
        return handler.WriteToken(token);
    }

    #endregion

    #region ValidateToken

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

    #endregion

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
        if (_signingCredentials == null)
        {
            throw new ArgumentNullException(nameof(_signingCredentials), "must have a Credentials signingCredentials.");
        }

        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = issuer,
            ValidateAudience = true,
            ValidAudience = audience,
            ValidateLifetime = true,
            IssuerSigningKey = _signingCredentials.Key,
            ValidateIssuerSigningKey = true
        };
        return validationParameters;
    }
}