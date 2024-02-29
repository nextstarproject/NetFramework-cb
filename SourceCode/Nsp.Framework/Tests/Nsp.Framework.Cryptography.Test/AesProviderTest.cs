using Nsp.Framework.Core;

namespace Nsp.Framework.Cryptography.Test;

[TestClass]
public class AesProviderTest
{
    [TestMethod]
    public void AesTest()
    {
        var aes = new NspAesProvider();
        var iv = aes.ExportBase64IV();
        Assert.IsNotNull(iv);
        var str = "Hello word";
        var encrypt = aes.Encrypt(str);
        var decrypt = aes.Decrypt(encrypt);
        Assert.AreEqual(str, decrypt);
    }

    [TestMethod]
    [DataRow("nS123456")]
    [DataRow("123456789")]
    public void AesPasswordTest(string password)
    {
        var aes = new NspAesProvider(password);
        var iv = aes.ExportBase64IV();
        Assert.IsNotNull(iv);
        var str = "Hello word";
        var encrypt = aes.Encrypt(str);
        var decrypt = aes.Decrypt(encrypt);
        Assert.AreEqual(str, decrypt);
    }

    [TestMethod]
    [DataRow("nS123456")]
    [DataRow("123456789")]
    public void AesIVTest(string password)
    {
        var ivBytes = RandomStringUtil.CreateRandomKey(16);
        var oldIV = Convert.ToBase64String(ivBytes);
        var aes = new NspAesProvider(password, oldIV);
        var iv = aes.ExportBase64IV();
        Assert.IsNotNull(iv);
        Assert.AreEqual(oldIV, iv);
        var str = "Hello word";
        var encrypt = aes.Encrypt(str);
        var decrypt = aes.Decrypt(encrypt);
        Assert.AreEqual(str, decrypt);
    }
}