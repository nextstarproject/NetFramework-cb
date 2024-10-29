namespace Nsp.Framework.Security.DigitalSignature;

public interface ISignatureX509Algorithm : ISignatureAlgorithm
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="notBefore">不设置默认现在</param>
    /// <param name="notAfter">不设置默认一年</param>
    /// <param name="distinguishedName">设置CN名称</param>
    /// <example>distinguishedName="CN=NSP"</example>
    /// <returns></returns>
    X509Certificate2 ExportX509Certificate2(DateTimeOffset? notBefore = null, DateTimeOffset? notAfter = null,
        string distinguishedName = "CN=NSP");
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="password">证书PFX密码</param>
    /// <param name="notBefore">不设置默认现在</param>
    /// <param name="notAfter">不设置默认一年</param>
    /// <param name="distinguishedName">设置CN名称</param>
    /// <example>distinguishedName="CN=NSP"</example>
    /// <returns></returns>
    byte[] ExportPfxData(string? password = "", DateTimeOffset? notBefore = null, DateTimeOffset? notAfter = null,
        string distinguishedName = "CN=NSP");

    /// <summary>
    /// 这个只返回公钥Cert数据
    /// </summary>
    /// <param name="notBefore"></param>
    /// <param name="notAfter"></param>
    /// <param name="distinguishedName"></param>
    /// <returns></returns>
    byte[] ExportCerData(DateTimeOffset? notBefore = null, DateTimeOffset? notAfter = null,
        string distinguishedName = "CN=NSP");
}