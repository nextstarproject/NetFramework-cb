using System.ComponentModel;

namespace Nsp.Framework.Security.Otp;

/// <summary>
/// Indicates which HMAC hashing algorithm should be used
/// </summary>
internal enum OtpHashMode
{
    /// <summary>
    /// Sha1 is used as the HMAC hashing algorithm
    /// </summary>
    [Description("SHA1")]
    Sha1,
    /// <summary>
    /// Sha256 is used as the HMAC hashing algorithm
    /// </summary>
    [Description("SHA256")]
    Sha256,
    /// <summary>
    /// Sha512 is used as the HMAC hashing algorithm
    /// </summary>
    [Description("SHA512")]
    Sha512
}