using Nsp.Framework.Core;
using Nsp.Framework.Security.Mac;

namespace Nsp.Framework.Security.Otp;

/// <summary>
/// An abstract class that contains common OTP calculations
/// </summary>
/// <remarks>
/// https://tools.ietf.org/html/rfc4226
/// </remarks>
internal class Otp
{
    protected readonly byte[] SecretKeyBytes;

    /// <summary>
    /// The hash mode to use
    /// </summary>
    protected readonly OtpHashMode HashMode;

    /// <summary>
    /// 步长
    /// </summary>
    protected int Step { get; init; } = 30;

    /// <summary>
    /// 生成Code长度
    /// </summary>
    protected int OtpSize { get; init; } = 6;

    /// <summary>
    /// 验证窗口，默认前后各一个区域
    /// </summary>
    protected VerificationWindow VerificationWindow { get; init; } = VerificationWindow.RfcSpecifiedNetworkDelay;

    private IHmacShaAlgorithm HmacShaAlgorithm => CreateHmacHash(HashMode);

    #region Constructor

    public Otp(byte[] secretKeyBytes)
    {
        SecretKeyBytes = SecurityUtil.FillRepeatBytes(secretKeyBytes, 20);
    }

    public Otp(string secretKey) : this(Base32Utils.Decode(secretKey))
    {
    }

    /// <summary>
    /// Constructor for the abstract class using an explicit secret key
    /// </summary>
    /// <param name="secretKey">32位长度Key</param>
    /// <param name="hashMode">The hash mode to use</param>
    public Otp(string secretKey, OtpHashMode hashMode = OtpHashMode.Sha1) : this(secretKey)
    {
        HashMode = hashMode;
    }

    public Otp(string secretKey, OtpHashMode hashMode = OtpHashMode.Sha1, int step = 30) : this(secretKey, hashMode)
    {
        Step = step;
        VerifyParameters(Step, OtpSize);
    }

    public Otp(string secretKey, OtpHashMode hashMode = OtpHashMode.Sha1, int step = 30, int otpSize = 6) : this(secretKey, hashMode, step)
    {
        OtpSize = otpSize;
        VerifyParameters(Step, OtpSize);
    }

    public Otp(string secretKey, OtpHashMode hashMode = OtpHashMode.Sha1, int step = 30, int otpSize = 6, VerificationWindow? verificationWindow = null) :
        this(secretKey, hashMode, step, otpSize)
    {
        VerificationWindow = verificationWindow ?? VerificationWindow.RfcSpecifiedNetworkDelay;
        VerifyParameters(Step, OtpSize);
    }

    #endregion

    public long GenerateCode(DateTimeOffset? dateTimeOffset = null)
    {
        var time = (dateTimeOffset ?? DateTimeOffset.UtcNow).ToUnixTimeSeconds();
        var timeStepCounter = time / Step;
        
        return GenerateCodeFromCounter(timeStepCounter);
    }

    public bool VerifyCode(long code, DateTimeOffset? dateTimeOffset = null)
    {
        var currentDateTimeOffset = dateTimeOffset ?? DateTimeOffset.UtcNow;
        var time = currentDateTimeOffset.ToUnixTimeSeconds();
        var timeStepCounter = time / Step;

        return VerificationWindow.ValidationCandidates(timeStepCounter).Any(x =>
        {
            var timeCode = GenerateCodeFromCounter(x);
            return code == timeCode;
        });
    }

    public long GenerateCodeFromCounter(long timeStepCounter)
    {
        var hash = HmacShaAlgorithm.Encrypt(BitConverter.GetBytes(timeStepCounter));

        // 从哈希值中截取验证码
        long offset = hash[^1] & 0xf;
        long truncatedHash = (hash[offset] & 0x7f) << 24 | (hash[offset + 1] & 0xff) << 16 | (hash[offset + 2] & 0xff) << 8 | (hash[offset + 3] & 0xff);
        var code = truncatedHash % (long)Math.Pow(10, OtpSize);
        return code;
    }

    public static string GenerateSecretKey(int byteLength = 20)
    {
        if (byteLength is not (16 or 20))
            throw new ArgumentOutOfRangeException(nameof(byteLength));
        return Base32Utils.GenerateRandom(byteLength);
    }

    private static void VerifyParameters(int step, int otpSize)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(step);
        if (otpSize is <= 0 or > 10)
            throw new ArgumentOutOfRangeException(nameof(otpSize));
    }


    /// <summary>
    /// Create an HMAC object for the specified algorithm
    /// </summary>
    private IHmacShaAlgorithm CreateHmacHash(OtpHashMode otpHashMode)
    {
        return otpHashMode switch
        {
            OtpHashMode.Sha256 => new HmacSha256(SecretKeyBytes),
            OtpHashMode.Sha512 => new HmacSha512(SecretKeyBytes),
            _ => new HmacSha1(SecretKeyBytes)
        };
    }
}