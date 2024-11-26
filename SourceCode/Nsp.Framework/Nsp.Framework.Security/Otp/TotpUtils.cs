using Nsp.Framework.Core;

namespace Nsp.Framework.Security.Otp;

public static class TotpUtils
{
    public static string GenerateSecretKey()
    {
        return Otp.GenerateSecretKey();
    }

    /// <summary>
    /// 根据输入的base64格式secret生成6位随机code
    /// </summary>
    /// <param name="base32Secret"></param>
    /// <returns></returns>
    public static string GenerateCode(string base32Secret)
    {
        var otpCalc = new Otp(base32Secret);
        return otpCalc.GenerateCode();
    }

    public static string GenerateCode(string base32Secret, int step = 30, int otpSize = 6, int previous = 0,
        int future = 0)
    {
        var otpCalc = new Otp(base32Secret, OtpHashMode.Sha1, step, otpSize, new VerificationWindow(previous, future));
        return otpCalc.GenerateCode();
    }

    /// <summary>
    /// 验证Opt code是否有效
    /// </summary>
    /// <param name="code"></param>
    /// <param name="base32Secret"></param>
    /// <returns></returns>
    public static bool VerifyTotp(string code, string base32Secret)
    {
        var otpCalc = new Otp(base32Secret);
        return otpCalc.VerifyCode(code);
    }

    public static bool VerifyTotp(string code, string base32Secret, int step = 30, int otpSize = 6, int previous = 0,
        int future = 0)
    {
        var otpCalc = new Otp(base32Secret, OtpHashMode.Sha1, step, otpSize, new VerificationWindow(previous, future));
        return otpCalc.VerifyCode(code);
    }

    /// <summary>
    /// 生成otp的地址，用于生成二维码进行扫描
    /// </summary>
    /// <param name="base32Secret">密钥</param>
    /// <param name="identifier">身份信息，一般都为邮箱</param>
    /// <param name="issuer">发行人，一般为站点</param>
    /// <returns></returns>
    public static string GenerateOtpAuthUrl(string base32Secret, string identifier = "admin@nextstar.space",
        string issuer = "NextStar")
    {
        var provisionUrl = $"otpauth://totp/{issuer}:{identifier}?secret={base32Secret}&issuer={issuer}";
        return provisionUrl;
    }
}