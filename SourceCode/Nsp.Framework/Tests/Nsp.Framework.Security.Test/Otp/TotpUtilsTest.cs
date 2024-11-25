using Nsp.Framework.Security.Otp;

namespace Nsp.Framework.Security.Test.Otp;

[TestClass]
public class TotpUtilsTest
{
    [TestMethod]
    public void Test1()
    {
        var key = TotpUtils.GenerateSecretKey();
        Console.WriteLine(key);
        var code = TotpUtils.GenerateCode(key);
        Console.WriteLine(code);
        var ver = TotpUtils.VerifyTotp(code, key);
        var a = TotpUtils.GenerateOtpAuthUrl(key);
        Console.WriteLine(a);
        Assert.IsTrue(ver);
    }
}