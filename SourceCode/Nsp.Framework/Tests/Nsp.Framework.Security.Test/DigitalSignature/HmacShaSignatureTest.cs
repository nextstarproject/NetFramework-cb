using Nsp.Framework.Security.DigitalSignature;

namespace Nsp.Framework.Security.Test.DigitalSignature;

[TestClass]
public class HmacShaSignatureTest
{
    [TestMethod]
    [DataRow("nS123456")]
    [DataRow("123456789")]
    public void HmacSha256SignatureTest(string password)
    {
        var hmac = new HmacSha256Signature(password);
        var hmac2 = new HmacSha256Signature(password);
        var text = "HmacSha256Signature";
        var encrypt1 = hmac.EncryptToHex(text);
        var compare1 = hmac2.VerifySignatureFromHex(text, encrypt1);
        Assert.IsTrue(compare1);

        var encrypt2 = hmac2.EncryptToBase64(text);
        var compare2 = hmac.VerifySignatureFromBase64(text, encrypt2);
        Assert.IsTrue(compare2);
    }
    
    [TestMethod]
    [DataRow("nS123456")]
    [DataRow("123456789")]
    public void HmacSha384SignatureTest(string password)
    {
        var hmac = new HmacSha384Signature(password);
        var hmac2 = new HmacSha384Signature(password);
        var text = "HmacSha384Signature";
        var encrypt1 = hmac.EncryptToHex(text);
        var compare1 = hmac2.VerifySignatureFromHex(text, encrypt1);
        Assert.IsTrue(compare1);

        var encrypt2 = hmac2.EncryptToBase64(text);
        var compare2 = hmac.VerifySignatureFromBase64(text, encrypt2);
        Assert.IsTrue(compare2);
    }
    
    [TestMethod]
    [DataRow("nS123456")]
    [DataRow("123456789")]
    public void HmacSha512SignatureTest(string password)
    {
        var hmac = new HmacSha512Signature(password);
        var hmac2 = new HmacSha512Signature(password);
        var text = "HmacSha512Signature";
        var encrypt1 = hmac.EncryptToHex(text);
        var compare1 = hmac2.VerifySignatureFromHex(text, encrypt1);
        Assert.IsTrue(compare1);

        var encrypt2 = hmac2.EncryptToBase64(text);
        var compare2 = hmac.VerifySignatureFromBase64(text, encrypt2);
        Assert.IsTrue(compare2);
    }
}