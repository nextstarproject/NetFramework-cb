using Nsp.Framework.Security.Otp;

namespace Nsp.Framework.Security.Test.Otp;

[TestClass]
public class TotpUtilsTest
{
    [TestMethod]
    public void Test1()
    {
        var key = TotpUtils.GenerateSecretKey();
        var code = TotpUtils.GenerateCode(key);
        var verify = TotpUtils.VerifyTotp(code, key);
        Assert.IsTrue(verify);
    }

    [TestMethod]
    public void Test2()
    {
        var key = TotpUtils.GenerateSecretKey();
        var code = TotpUtils.GenerateCode(key, 30, 8, 30, 30);
        Assert.AreEqual(8, code.Length);
        var verify = TotpUtils.VerifyTotp(code, key, 30, 8, 30, 30);
        Assert.IsTrue(verify);
    }
}