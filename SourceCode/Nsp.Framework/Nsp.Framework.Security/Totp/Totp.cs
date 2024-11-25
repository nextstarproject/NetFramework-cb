namespace Nsp.Framework.Security.Totp;

/// <summary>
/// An abstract class that contains common OTP calculations
/// </summary>
/// <remarks>
/// https://tools.ietf.org/html/rfc4226
/// </remarks>
internal class Totp
{
    /// <summary>
    /// Secret key
    /// </summary>
    protected readonly string SecretKey;

    /// <summary>
    /// The hash mode to use
    /// </summary>
    protected readonly OtpHashMode HashMode;

    /// <summary>
    /// Constructor for the abstract class using an explicit secret key
    /// </summary>
    /// <param name="secretKey">32位长度Key</param>
    /// <param name="hashMode">The hash mode to use</param>
    public Totp(string secretKey, OtpHashMode hashMode)
    {
        SecretKey = secretKey;
        HashMode = hashMode;
    }
}