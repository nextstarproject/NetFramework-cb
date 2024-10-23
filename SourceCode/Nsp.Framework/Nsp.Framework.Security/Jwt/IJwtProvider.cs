using System.Security.Claims;

namespace Nsp.Framework.Security.Jwt;

public interface IJwtProvider
{
    SigningCredentials Credentials { get; }

    /// <summary>
    /// 请注意：其中sub和jti是使用<see cref="Guid.NewGuid().ToString()"/>分别进行填充
    /// issuer默认：nextstar
    /// audience默认：user
    /// </summary>
    /// <param name="notBefore">最好使用UTC时间</param>
    /// <param name="expires">最好使用UTC时间</param>
    /// <param name="customClaimsDic"></param>
    /// <returns></returns>
    string GenerateToken(DateTime notBefore, DateTime expires, Dictionary<string, string>? customClaimsDic = null);

    /// <summary>
    /// 请注意：其中sub是使用<see cref="Guid.NewGuid().ToString()"/>进行填充
    /// issuer默认：nextstar
    /// audience默认：user
    /// </summary>
    /// <param name="notBefore">最好使用UTC时间</param>
    /// <param name="expires">最好使用UTC时间</param>
    /// <param name="uniqueIdentityId"></param>
    /// <param name="customClaimsDic"></param>
    /// <returns></returns>
    string GenerateToken(DateTime notBefore, DateTime expires, string uniqueIdentityId,
        Dictionary<string, string>? customClaimsDic = null);

    /// <summary>
    /// issuer默认：nextstar
    /// audience默认：user
    /// </summary>
    /// <param name="subject"></param>
    /// <param name="notBefore">最好使用UTC时间</param>
    /// <param name="expires">最好使用UTC时间</param>
    /// <param name="uniqueIdentityId"></param>
    /// <param name="customClaimsDic"></param>
    /// <returns></returns>
    string GenerateToken(string subject, DateTime notBefore, DateTime expires, string uniqueIdentityId,
        Dictionary<string, string>? customClaimsDic = null);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="subject"></param>
    /// <param name="notBefore">最好使用UTC时间</param>
    /// <param name="expires">最好使用UTC时间</param>
    /// <param name="uniqueIdentityId"></param>
    /// <param name="issuer"></param>
    /// <param name="audience"></param>
    /// <param name="customClaimsDic"></param>
    /// <returns></returns>
    string GenerateToken(string subject, DateTime notBefore, DateTime expires, string uniqueIdentityId,string issuer, string audience,
        Dictionary<string, string>? customClaimsDic = null);

    /// <summary>
    /// issuer默认：nextstar
    /// audience默认：user
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    bool ValidateToken(string token);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="token"></param>
    /// <param name="issuer"></param>
    /// <param name="audience"></param>
    /// <returns></returns>
    bool ValidateToken(string token, string issuer, string audience);

    /// <summary>
    /// issuer默认：nextstar
    /// audience默认：user
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<bool> ValidateTokenAsync(string token);
    Task<bool> ValidateTokenAsync(string token, string issuer, string audience);
    Dictionary<string, string> GetAllClaims(string token);
}