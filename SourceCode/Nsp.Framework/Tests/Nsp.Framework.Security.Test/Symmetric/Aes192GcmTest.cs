using Nsp.Framework.Core;
using Nsp.Framework.Security.Symmetric;

namespace Nsp.Framework.Security.Test.Symmetric;

[TestClass]
public class Aes192GcmTest
{
    [TestMethod]
    [Description("Test empty constructors")]
    public void Aes192GcmMainTest()
    {
        var aes = new Aes192Gcm();
        var aesImport = new Aes192Gcm(aes.AesKeyBase64);
        var str = RandomStringUtil.Alphanumeric();
        var encrypt1 = aes.EncryptToBase64(str);
        var decrypt1 = aesImport.DecryptFromBase64(encrypt1);
        Assert.AreEqual(str, decrypt1);

        var encrypt2 = aesImport.EncryptToHex(str);
        var decrypt2 = aes.DecryptFromHex(encrypt2);
        Assert.AreEqual(str, decrypt2);
    }

    [TestMethod]
    [DataRow("nS123456")]
    [DataRow("123456789")]
    [Description("Test with key constructors")]
    public void Aes192GcmWithHmacTest(string password)
    {
        var aesKey = password.FillRepeatBytes(Aes192Gcm.KeyByteSize);
        var aes = new Aes192Gcm(aesKey);
        var aesImport = new Aes192Gcm(aes.AesKeyBase64);
        var str = RandomStringUtil.Alphanumeric();
        var encrypt1 = aes.EncryptToBase64(str);
        var decrypt1 = aesImport.DecryptFromBase64(encrypt1);
        Assert.AreEqual(str, decrypt1);

        var encrypt2 = aesImport.EncryptToHex(str);
        var decrypt2 = aes.DecryptFromHex(encrypt2);
        Assert.AreEqual(str, decrypt2);
        
        var aesImportNew = new Aes192Gcm(aes.AesKey);
        var encrypt3 = aes.EncryptToBase64(str);
        var decrypt3 = aesImportNew.DecryptFromBase64(encrypt3);
        var encrypt4 = aesImportNew.EncryptToBase64(str);
        var decrypt4 = aesImport.DecryptFromBase64(encrypt4);
        Assert.AreEqual(str, decrypt3);
        Assert.AreEqual(str, decrypt4);
    }
}