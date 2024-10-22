using Nsp.Framework.Security.Asymmetric;

namespace Nsp.Framework.Security.Test.Asymmetric;

[TestClass]
public class RsaTest
{
    [TestMethod]
    public void RsaPKCS1Test()
    {
        var rsa = new RsaPKCS1();
        var text = "RsaPKCS1";
        var encrypt1 = rsa.EncryptToBase64(text);
        var encrypt2 = rsa.EncryptToHex(text);
        var publicKey = rsa.ExportPublicToBase64();
        var privateKey = rsa.ExportPrivateToBase64();

        var rsa2 = new RsaPKCS1(privateKey, publicKey);
        var decrypt1 = rsa2.DecryptFromBase64(encrypt1);
        var decrypt2 = rsa2.DecryptFromHex(encrypt2);
        Assert.AreEqual(text, decrypt1);
        Assert.AreEqual(text, decrypt2);
    }
    
    [TestMethod]
    public void RsaPKCS1ParameterTest()
    {
        var rsa = new RsaPKCS1();
        var text = "RsaPKCS1";
        var encrypt1 = rsa.EncryptToBase64(text);
        var encrypt2 = rsa.EncryptToHex(text);
        var parameters = rsa.ExportParameters();

        var rsa2 = new RsaPKCS1(parameters);
        var decrypt1 = rsa2.DecryptFromBase64(encrypt1);
        var decrypt2 = rsa2.DecryptFromHex(encrypt2);
        Assert.AreEqual(text, decrypt1);
        Assert.AreEqual(text, decrypt2);
    }
    
    [TestMethod]
    public void RsaOAEPTest()
    {
        var rsa = new RsaOAEP();
        var text = "RsaOAEP";
        var encrypt1 = rsa.EncryptToBase64(text);
        var encrypt2 = rsa.EncryptToHex(text);
        var publicKey = rsa.ExportPublicToBase64();
        var privateKey = rsa.ExportPrivateToBase64();

        var rsa2 = new RsaOAEP(privateKey, publicKey);
        var decrypt1 = rsa2.DecryptFromBase64(encrypt1);
        var decrypt2 = rsa2.DecryptFromHex(encrypt2);
        Assert.AreEqual(text, decrypt1);
        Assert.AreEqual(text, decrypt2);
    }
    
    [TestMethod]
    public void RsaOAEPParameterTest()
    {
        var rsa = new RsaOAEP();
        var text = "RsaOAEP";
        var encrypt1 = rsa.EncryptToBase64(text);
        var encrypt2 = rsa.EncryptToHex(text);
        var parameters = rsa.ExportParameters();

        var rsa2 = new RsaOAEP(parameters);
        var decrypt1 = rsa2.DecryptFromBase64(encrypt1);
        var decrypt2 = rsa2.DecryptFromHex(encrypt2);
        Assert.AreEqual(text, decrypt1);
        Assert.AreEqual(text, decrypt2);
    }
}