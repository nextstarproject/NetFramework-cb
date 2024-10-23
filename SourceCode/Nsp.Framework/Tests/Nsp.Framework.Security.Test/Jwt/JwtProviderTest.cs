using Nsp.Framework.Security.DigitalSignature;
using Nsp.Framework.Security.Jwt;
using System.IdentityModel.Tokens.Jwt;

namespace Nsp.Framework.Security.Test.Jwt;

[TestClass]
public class JwtProviderTest
{
    /// <summary>
    /// 生成Token并且验证Token，以及获取Token中的所有Claims并且验证
    /// </summary>
    [TestMethod]
    public void JwtProviderBaseTest()
    {
        var rsa = new RsaSha256Signature();
        var jwtProvider = new JwtProvider(rsa.ExportSigningCredentials(Guid.NewGuid().ToString()));
        
        var uniqueIdentityId = Guid.NewGuid().ToString();
        var subject = Guid.NewGuid().ToString();
        var issuer = "issuer";
        var audience = "audience";
        var token = jwtProvider.GenerateToken(subject,DateTime.UtcNow,DateTime.UtcNow.AddDays(1),uniqueIdentityId, issuer,audience, new Dictionary<string, string>()
        {
            {"name", "nextstar"},
            {"role", "user"}
        });
        var validateResult = jwtProvider.ValidateToken(token, issuer, audience);
        Assert.IsTrue(validateResult);
        var allClaims = jwtProvider.GetAllClaims(token);
        var jtiValue = allClaims.GetValueOrDefault(JwtRegisteredClaimNames.Jti, string.Empty);
        var subValue = allClaims.GetValueOrDefault(JwtRegisteredClaimNames.Sub, string.Empty);
        var issValue = allClaims.GetValueOrDefault(JwtRegisteredClaimNames.Iss, string.Empty);
        var audValue = allClaims.GetValueOrDefault(JwtRegisteredClaimNames.Aud, string.Empty);
        var nameValue = allClaims.GetValueOrDefault("name", string.Empty);
        var roleValue = allClaims.GetValueOrDefault("role", string.Empty);
        Assert.AreEqual(uniqueIdentityId, jtiValue);
        Assert.AreEqual(subject, subValue);
        Assert.AreEqual(issuer, issValue);
        Assert.AreEqual(audience, audValue);
        Assert.AreEqual("nextstar", nameValue);
        Assert.AreEqual("user", roleValue);
        Assert.IsNotNull(allClaims);
    }
    
    /// <summary>
    /// RSA导出后导入进行Token验证
    /// </summary>
    [TestMethod]
    public void JwtProviderExportAndImportTest()
    {
        var rsa = new RsaSha256Signature();
        var jwtProvider = new JwtProvider(rsa.ExportSigningCredentials(Guid.NewGuid().ToString()));
        
        var uniqueIdentityId = Guid.NewGuid().ToString();
        var subject = Guid.NewGuid().ToString();
        var issuer = "issuer";
        var audience = "audience";
        var token = jwtProvider.GenerateToken(subject,DateTime.UtcNow,DateTime.UtcNow.AddDays(1),uniqueIdentityId, issuer,audience, new Dictionary<string, string>()
        {
            {"name", "nextstar"},
            {"role", "user"}
        });
        
        var publicKey = rsa.ExportPublicToBase64();
        var privateKey = rsa.ExportPrivateToBase64();
        var rsa2 = new RsaSha256Signature(privateKey, publicKey);
        var jwtProvider2 = new JwtProvider(rsa2.ExportSigningCredentials(Guid.NewGuid().ToString()));
        
        var validateResult = jwtProvider2.ValidateToken(token, issuer, audience);
        Assert.IsTrue(validateResult);
        var allClaims = jwtProvider2.GetAllClaims(token);
        var jtiValue = allClaims.GetValueOrDefault(JwtRegisteredClaimNames.Jti, string.Empty);
        var subValue = allClaims.GetValueOrDefault(JwtRegisteredClaimNames.Sub, string.Empty);
        var issValue = allClaims.GetValueOrDefault(JwtRegisteredClaimNames.Iss, string.Empty);
        var audValue = allClaims.GetValueOrDefault(JwtRegisteredClaimNames.Aud, string.Empty);
        var nameValue = allClaims.GetValueOrDefault("name", string.Empty);
        var roleValue = allClaims.GetValueOrDefault("role", string.Empty);
        Assert.AreEqual(uniqueIdentityId, jtiValue);
        Assert.AreEqual(subject, subValue);
        Assert.AreEqual(issuer, issValue);
        Assert.AreEqual(audience, audValue);
        Assert.AreEqual("nextstar", nameValue);
        Assert.AreEqual("user", roleValue);
        Assert.IsNotNull(allClaims);
        
    }
}