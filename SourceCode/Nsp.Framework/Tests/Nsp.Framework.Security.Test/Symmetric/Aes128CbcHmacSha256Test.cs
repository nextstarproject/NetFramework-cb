using Nsp.Framework.Core;
using Nsp.Framework.Security.Symmetric;

namespace Nsp.Framework.Security.Test.Symmetric;

[TestClass]
public class Aes128CbcHmacSha256Test
{
    [TestMethod]
    [Description("Test empty constructors")]
    public void Aes128CbcHmacSha256MainTest()
    {
        var aes = new Aes128CbcHmacSha256();
        var aesImport = new Aes128CbcHmacSha256(aes.AesKeyBase64, aes.HmacKeyBase64);
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
    public void Aes128CbcHmacSha256WithHmacTest(string password)
    {
        var aesKey = password.FillRepeatBytes(Aes128CbcHmacSha256.KeyByteSize);
        var hmacKey = RandomStringUtil.Alphanumeric().FillRepeatBytes(Aes128CbcHmacSha256.HmacKeyByteSize);
        var aes = new Aes128CbcHmacSha256(aesKey, hmacKey);
        var aesImport = new Aes128CbcHmacSha256(aes.AesKeyBase64, aes.HmacKeyBase64);
        var str = RandomStringUtil.Alphanumeric();
        var encrypt1 = aes.EncryptToBase64(str);
        var decrypt1 = aesImport.DecryptFromBase64(encrypt1);
        Assert.AreEqual(str, decrypt1);

        var encrypt2 = aesImport.EncryptToHex(str);
        var decrypt2 = aes.DecryptFromHex(encrypt2);
        Assert.AreEqual(str, decrypt2);
        
        var aesImportNew = new Aes128CbcHmacSha256(aesKey, hmacKey);
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
    public void Aes128CbcHmacSha256WithoutHmacTest(string password)
    {
        var aesKey = password.FillRepeatBytes(Aes128CbcHmacSha256.KeyByteSize);
        var aes = new Aes128CbcHmacSha256(aesKey);
        var aesImport = new Aes128CbcHmacSha256(aes.AesKeyBase64, aes.HmacKeyBase64);
        var str = RandomStringUtil.Alphanumeric();
        var encrypt1 = aes.EncryptToBase64(str);
        var decrypt1 = aesImport.DecryptFromBase64(encrypt1);
        Assert.AreEqual(str, decrypt1);

        var encrypt2 = aesImport.EncryptToHex(str);
        var decrypt2 = aes.DecryptFromHex(encrypt2);
        Assert.AreEqual(str, decrypt2);
        
        var aesImportNew = new Aes128CbcHmacSha256(aesKey, aes.HmacKey);
        var encrypt3 = aes.EncryptToBase64(str);
        var decrypt3 = aesImportNew.DecryptFromBase64(encrypt3);
        var encrypt4 = aesImportNew.EncryptToBase64(str);
        var decrypt4 = aesImport.DecryptFromBase64(encrypt4);
        Assert.AreEqual(str, decrypt3);
        Assert.AreEqual(str, decrypt4);
    }
}