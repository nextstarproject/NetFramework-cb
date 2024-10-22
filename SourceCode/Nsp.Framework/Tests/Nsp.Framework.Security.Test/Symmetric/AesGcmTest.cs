using Nsp.Framework.Security.Symmetric;

namespace Nsp.Framework.Security.Test.Symmetric;

[TestClass]
public class AesGcmTest
{
    [TestMethod]
    [DataRow("nS123456")]
    [DataRow("123456789")]
    public void Aes128GcmTest(string password)
    {
        var aes = new Aes128Gcm(password);
        var aes2 = new Aes128Gcm(password);
        var str = "Hello word";
        var encrypt1 = aes.EncryptToBase64(str);
        var decrypt1 = aes2.DecryptFromBase64(encrypt1);
        Assert.AreEqual(str, decrypt1);

        var encrypt2 = aes2.EncryptToHex(str);
        var decrypt2 = aes.DecryptFromHex(encrypt2);
        Assert.AreEqual(str, decrypt2);
    }
    
    [TestMethod]
    [DataRow("nS123456")]
    [DataRow("123456789")]
    public void Aes192GcmTest(string password)
    {
        var aes = new Aes192Gcm(password);
        var aes2 = new Aes192Gcm(password);
        var str = "Hello word";
        var encrypt1 = aes.EncryptToBase64(str);
        var decrypt1 = aes2.DecryptFromBase64(encrypt1);
        Assert.AreEqual(str, decrypt1);

        var encrypt2 = aes2.EncryptToHex(str);
        var decrypt2 = aes.DecryptFromHex(encrypt2);
        Assert.AreEqual(str, decrypt2);
    }
    
    [TestMethod]
    [DataRow("nS123456")]
    [DataRow("123456789")]
    public void Aes256GcmTest(string password)
    {
        var aes = new Aes256Gcm(password);
        var aes2 = new Aes256Gcm(password);
        var str = "Hello word";
        var encrypt1 = aes2.EncryptToBase64(str);
        var decrypt1 = aes.DecryptFromBase64(encrypt1);
        Assert.AreEqual(str, decrypt1);

        var encrypt2 = aes.EncryptToHex(str);
        var decrypt2 = aes2.DecryptFromHex(encrypt2);
        Assert.AreEqual(str, decrypt2);
    }
}