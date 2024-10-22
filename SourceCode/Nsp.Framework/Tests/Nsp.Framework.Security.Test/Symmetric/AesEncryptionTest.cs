using Nsp.Framework.Security.Symmetric;

namespace Nsp.Framework.Security.Test.Symmetric;

[TestClass]
public class AesEncryptionTest
{
    [TestMethod]
    [DataRow("nS123456")]
    [DataRow("123456789")]
    public void Aes128EncryptionTest(string password)
    {
        var aes = new Aes128Encryption(password, "Aes128Encryption");
        var aes2 = new Aes128Encryption(password, "Aes128Encryption");
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
    public void Aes192EncryptionTest(string password)
    {
        var aes = new Aes192Encryption(password, "Aes192Encryption");
        var aes2 = new Aes192Encryption(password, "Aes192Encryption");
        var str = "Hello word";
        var encrypt1 = aes2.EncryptToBase64(str);
        var decrypt1 = aes.DecryptFromBase64(encrypt1);
        Assert.AreEqual(str, decrypt1);

        var encrypt2 = aes.EncryptToHex(str);
        var decrypt2 = aes2.DecryptFromHex(encrypt2);
        Assert.AreEqual(str, decrypt2);
    }
    
    [TestMethod]
    [DataRow("nS123456")]
    [DataRow("123456789")]
    public void Aes256EncryptionTest(string password)
    {
        var aes = new Aes256Encryption(password, "Aes256Encryption");
        var aes2 = new Aes256Encryption(password, "Aes256Encryption");
        var str = "Hello word";
        var encrypt1 = aes.EncryptToBase64(str);
        var decrypt1 = aes2.DecryptFromBase64(encrypt1);
        Assert.AreEqual(str, decrypt1);

        var encrypt2 = aes.EncryptToHex(str);
        var decrypt2 = aes2.DecryptFromHex(encrypt2);
        Assert.AreEqual(str, decrypt2);
    }
}