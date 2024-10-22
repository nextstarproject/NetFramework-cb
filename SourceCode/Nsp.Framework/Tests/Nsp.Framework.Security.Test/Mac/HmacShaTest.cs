using Nsp.Framework.Security.Mac;

namespace Nsp.Framework.Security.Test.Mac;

[TestClass]
public class HmacShaTest
{
    [TestMethod]
    [DataRow("nS123456")]
    [DataRow("123456789")]
    public void HmacSha256Test(string password)
    {
        var hmac = new HmacSha256(password);
        var hmac2 = new HmacSha256(password);
        var text = "HmacSha256Test";
        var encrypt1 = hmac.EncryptToHex(text);
        var compare1 = hmac2.CompareFromHex(text, encrypt1);
        Assert.IsTrue(compare1);

        var encrypt2 = hmac2.EncryptToBase64(text);
        var compare2 = hmac.CompareFromBase64(text, encrypt2);
        Assert.IsTrue(compare2);
    }
    
    [TestMethod]
    [DataRow("nS123456")]
    [DataRow("123456789")]
    public void HmacSha384Test(string password)
    {
        var hmac = new HmacSha384(password);
        var hmac2 = new HmacSha384(password);
        var text = "HmacSha384";
        var encrypt1 = hmac.EncryptToHex(text);
        var compare1 = hmac2.CompareFromHex(text, encrypt1);
        Assert.IsTrue(compare1);

        var encrypt2 = hmac2.EncryptToBase64(text);
        var compare2 = hmac.CompareFromBase64(text, encrypt2);
        Assert.IsTrue(compare2);
    }
    
    [TestMethod]
    [DataRow("nS123456")]
    [DataRow("123456789")]
    public void HmacSha512Test(string password)
    {
        var hmac = new HmacSha512(password);
        var hmac2 = new HmacSha512(password);
        var text = "HmacSha512";
        var encrypt1 = hmac.EncryptToHex(text);
        var compare1 = hmac2.CompareFromHex(text, encrypt1);
        Assert.IsTrue(compare1);

        var encrypt2 = hmac2.EncryptToBase64(text);
        var compare2 = hmac.CompareFromBase64(text, encrypt2);
        Assert.IsTrue(compare2);
    }
}