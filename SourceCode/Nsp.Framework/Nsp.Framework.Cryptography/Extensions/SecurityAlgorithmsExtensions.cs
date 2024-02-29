using Microsoft.IdentityModel.Tokens;

namespace Nsp.Framework.Cryptography;

public static class SecurityAlgorithmsExtensions
{
    [return:NotNull]
    public static HashAlgorithmName ConvertHashAlgorithmName(this NspSecurityAlgorithms algorithms)
    {
        return algorithms switch
        {
            NspSecurityAlgorithms.SHA384 => HashAlgorithmName.SHA384,
            NspSecurityAlgorithms.SHA512 => HashAlgorithmName.SHA512,
            _ => HashAlgorithmName.SHA256
        };
    }
    
    [return:NotNull]
    public static string ConvertHashAlgorithmNameString(this NspSecurityAlgorithms algorithms)
    {
        return algorithms switch
        {
            NspSecurityAlgorithms.SHA384 => "SHA384",
            NspSecurityAlgorithms.SHA512 => "SHA512",
            _ => "SHA256"
        };
    }
    
    /// <summary>
    /// Ecdsa 使用 <see cref="NspECDsaProvider"/>
    /// </summary>
    /// <param name="algorithms"></param>
    /// <returns></returns>
    [return:NotNull]
    public static string ConvertEcdsaSecurityAlgorithms(this NspSecurityAlgorithms algorithms)
    {
        return algorithms switch
        {
            NspSecurityAlgorithms.SHA384 => SecurityAlgorithms.EcdsaSha384,
            NspSecurityAlgorithms.SHA512 => SecurityAlgorithms.EcdsaSha512,
            _ => SecurityAlgorithms.EcdsaSha256,
        };
    }
    
    /// <summary>
    /// HMAC 使用 <see cref="NspHmacProvider"/>
    /// </summary>
    /// <param name="algorithms"></param>
    /// <returns></returns>
    [return:NotNull]
    public static string ConvertHmacSecurityAlgorithms(this NspSecurityAlgorithms algorithms)
    {
        return algorithms switch
        {
            NspSecurityAlgorithms.SHA384 => SecurityAlgorithms.HmacSha384,
            NspSecurityAlgorithms.SHA512 => SecurityAlgorithms.HmacSha512,
            _ => SecurityAlgorithms.HmacSha512
        };
    }
}