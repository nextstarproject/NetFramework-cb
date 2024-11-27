using Nsp.Framework.Core;
using Nsp.Framework.Security.Symmetric;

namespace Nsp.Framework.Security.Test.Symmetric;

[TestClass]
public class Aes192CbcHmacSha384Test
{
    [TestMethod]
    [Description("Test empty constructors")]
    public void Aes192CbcHmacSha384MainTest()
    {
        var aes = new Aes192CbcHmacSha384();
        var aesImport = new Aes192CbcHmacSha384(aes.AesKeyBase64, aes.HmacKeyBase64);
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
    [Description("Test with hmac key constructors")]
    public void Aes192CbcHmacSha384WithHmacTest(string password)
    {
        var aesKey = password.FillRepeatBytes(Aes192CbcHmacSha384.KeyByteSize);
        var hmacKey = RandomStringUtil.Alphanumeric().FillRepeatBytes(Aes192CbcHmacSha384.HmacKeyByteSize);
        var aes = new Aes192CbcHmacSha384(aesKey, hmacKey);
        var aesImport = new Aes192CbcHmacSha384(aes.AesKeyBase64, aes.HmacKeyBase64);
        var str = RandomStringUtil.Alphanumeric();
        var encrypt1 = aes.EncryptToBase64(str);
        var decrypt1 = aesImport.DecryptFromBase64(encrypt1);
        Assert.AreEqual(str, decrypt1);

        var encrypt2 = aesImport.EncryptToHex(str);
        var decrypt2 = aes.DecryptFromHex(encrypt2);
        Assert.AreEqual(str, decrypt2);
        
        var aesImportNew = new Aes192CbcHmacSha384(aesKey, hmacKey);
        var encrypt3 = aes.EncryptToBase64(str);
        var decrypt3 = aesImportNew.DecryptFromBase64(encrypt3);
        var encrypt4 = aesImportNew.EncryptToBase64(str);
        var decrypt4 = aesImport.DecryptFromBase64(encrypt4);
        Assert.AreEqual(str, decrypt3);
        Assert.AreEqual(str, decrypt4);
    }

    [TestMethod]
    [DataRow("nS123456")]
    [DataRow("123456789")]
    [Description("Test without hmac key constructors")]
    public void Aes192CbcHmacSha384WithoutHmacTest(string password)
    {
        var aesKey = password.FillRepeatBytes(Aes192CbcHmacSha384.KeyByteSize);
        var aes = new Aes192CbcHmacSha384(aesKey);
        var aesImport = new Aes192CbcHmacSha384(aes.AesKeyBase64, aes.HmacKeyBase64);
        var str = RandomStringUtil.Alphanumeric();
        var encrypt1 = aes.EncryptToBase64(str);
        var decrypt1 = aesImport.DecryptFromBase64(encrypt1);
        Assert.AreEqual(str, decrypt1);

        var encrypt2 = aesImport.EncryptToHex(str);
        var decrypt2 = aes.DecryptFromHex(encrypt2);
        Assert.AreEqual(str, decrypt2);
        
        var aesImportNew = new Aes192CbcHmacSha384(aesKey, aes.HmacKey);
        var encrypt3 = aes.EncryptToBase64(str);
        var decrypt3 = aesImportNew.DecryptFromBase64(encrypt3);
        var encrypt4 = aesImportNew.EncryptToBase64(str);
        var decrypt4 = aesImport.DecryptFromBase64(encrypt4);
        Assert.AreEqual(str, decrypt3);
        Assert.AreEqual(str, decrypt4);
    }
}