using Nsp.Framework.Core;
using Nsp.Framework.Security.Symmetric;

namespace Nsp.Framework.Security.Test.Symmetric;

[TestClass]
public class Aes256EncryptionTest
{
    [TestMethod]
    [Description("Test empty constructors")]
    public void Aes256EncryptionMainTest()
    {
        var aes = new Aes256Encryption();
        var aesImport = new Aes256Encryption(aes.AesKeyBase64, aes.AesIvBase64);
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
    [Description("Test with iv key constructors")]
    public void Aes256EncryptionWithIvTest(string password)
    {
        var aesKey = password.FillRepeatBytes(Aes256Encryption.KeyByteSize);
        var ivKey = RandomStringUtil.Alphanumeric().FillRepeatBytes(Aes256Encryption.IvByteSize);
        var aes = new Aes256Encryption(aesKey, ivKey);
        var aesImport = new Aes256Encryption(aes.AesKeyBase64, aes.AesIvBase64);
        var str = RandomStringUtil.Alphanumeric();
        var encrypt1 = aes.EncryptToBase64(str);
        var decrypt1 = aesImport.DecryptFromBase64(encrypt1);
        Assert.AreEqual(str, decrypt1);

        var encrypt2 = aesImport.EncryptToHex(str);
        var decrypt2 = aes.DecryptFromHex(encrypt2);
        Assert.AreEqual(str, decrypt2);
        
        var aesImportNew = new Aes256Encryption(aesKey, ivKey);
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
    [Description("Test without iv key constructors")]
    public void Aes256EncryptionWithoutIvTest(string password)
    {
        var aesKey = password.FillRepeatBytes(Aes256Encryption.KeyByteSize);
        var aes = new Aes256Encryption(aesKey);
        var aesImport = new Aes256Encryption(aes.AesKeyBase64, aes.AesIvBase64);
        var str = RandomStringUtil.Alphanumeric();
        var encrypt1 = aes.EncryptToBase64(str);
        var decrypt1 = aesImport.DecryptFromBase64(encrypt1);
        Assert.AreEqual(str, decrypt1);

        var encrypt2 = aesImport.EncryptToHex(str);
        var decrypt2 = aes.DecryptFromHex(encrypt2);
        Assert.AreEqual(str, decrypt2);
        
        var aesImportNew = new Aes256Encryption(aesKey, aes.AesIv);
        var encrypt3 = aes.EncryptToBase64(str);
        var decrypt3 = aesImportNew.DecryptFromBase64(encrypt3);
        var encrypt4 = aesImportNew.EncryptToBase64(str);
        var decrypt4 = aesImport.DecryptFromBase64(encrypt4);
        Assert.AreEqual(str, decrypt3);
        Assert.AreEqual(str, decrypt4);
    }
}