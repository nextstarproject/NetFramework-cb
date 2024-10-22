using Nsp.Framework.Security.Symmetric;

namespace Nsp.Framework.Security.Test.Symmetric;

[TestClass]
public class AesCbcHmacShaTest
{
    [TestMethod]
    [DataRow("nS123456")]
    [DataRow("123456789")]
    public void Aes128CbcHmacSha256Test(string password)
    {
        var aes = new Aes128CbcHmacSha256(password, "Aes128CbcHmacSha256");
        var aes2 = new Aes128CbcHmacSha256(password, "Aes128CbcHmacSha256");
        var str = "Hello word";
        var encrypt1 = aes2.EncryptToBase64(str);
        var decrypt1 = aes.DecryptFromBase64(encrypt1);
        Assert.AreEqual(str, decrypt1);

        var encrypt2 = aes2.EncryptToHex(str);
        var decrypt2 = aes.DecryptFromHex(encrypt2);
        Assert.AreEqual(str, decrypt2);
    }
    
    [TestMethod]
    [DataRow("nS123456")]
    [DataRow("123456789")]
    public void Aes192CbcHmacSha384Test(string password)
    {
        var aes = new Aes192CbcHmacSha384(password, "Aes192CbcHmacSha384");
        var aes2 = new Aes192CbcHmacSha384(password, "Aes192CbcHmacSha384");
        var str = "Hello word";
        var encrypt1 = aes.EncryptToBase64(str);
        var decrypt1 = aes2.DecryptFromBase64(encrypt1);
        Assert.AreEqual(str, decrypt1);

        var encrypt2 = aes.EncryptToHex(str);
        var decrypt2 = aes2.DecryptFromHex(encrypt2);
        Assert.AreEqual(str, decrypt2);
    }
    
    [TestMethod]
    [DataRow("nS123456")]
    [DataRow("123456789")]
    public void Aes256CbcHmacSha512Test(string password)
    {
        var aes = new Aes256CbcHmacSha512(password, "Aes256CbcHmacSha512");
        var aes2 = new Aes256CbcHmacSha512(password, "Aes256CbcHmacSha512");
        var str = "Hello word";
        var encrypt1 = aes.EncryptToBase64(str);
        var decrypt1 = aes2.DecryptFromBase64(encrypt1);
        Assert.AreEqual(str, decrypt1);

        var encrypt2 = aes2.EncryptToHex(str);
        var decrypt2 = aes.DecryptFromHex(encrypt2);
        Assert.AreEqual(str, decrypt2);
    }
}