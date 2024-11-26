using System.Text;
using Nsp.Framework.Security.Symmetric;

namespace Nsp.Framework.Security.Test.Symmetric;

[TestClass]
public class AesCbcHmacShaTest
{
    [TestMethod]
    [DataRow("nS123456")]
    [DataRow("123456789")]
    public void Aes128CbcHmacSha256WithHmacTest(string password)
    {
        var aesKey = password.FillRepeatBytes(Aes128CbcHmacSha256.KeyByteSize);
        var hmacKey = "Aes128CbcHmacSha256".FillRepeatBytes(Aes128CbcHmacSha256.HmacKeyByteSize);
        var aes = new Aes128CbcHmacSha256(aesKey, hmacKey);
        var aes2 = new Aes128CbcHmacSha256(aes.AesKeyBase64, aes.HmacKeyBase64);
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
    public void Aes128CbcHmacSha256WithoutHmacTest(string password)
    {
        var aesKey = password.FillRepeatBytes(Aes128CbcHmacSha256.KeyByteSize);
        var aes = new Aes128CbcHmacSha256(aesKey);
        var aes2 = new Aes128CbcHmacSha256(aes.AesKeyBase64, aes.HmacKeyBase64);
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
    public void Aes192CbcHmacSha384WithHmacTest(string password)
    {
        var aesKey = password.FillRepeatBytes(Aes192CbcHmacSha384.KeyByteSize);
        var hmacKey = "Aes192CbcHmacSha384".FillRepeatBytes(Aes192CbcHmacSha384.HmacKeyByteSize);
        var aes = new Aes192CbcHmacSha384(aesKey, hmacKey);
        var aes2 = new Aes192CbcHmacSha384(aes.AesKeyBase64, aes.HmacKeyBase64);
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
    public void Aes192CbcHmacSha384WithoutHmacTest(string password)
    {
        var aesKey = password.FillRepeatBytes(Aes192CbcHmacSha384.KeyByteSize);
        var aes = new Aes192CbcHmacSha384(aesKey);
        var aes2 = new Aes192CbcHmacSha384(aes.AesKeyBase64, aes.HmacKeyBase64);
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
    public void Aes256CbcHmacSha512WithHmacTest(string password)
    {
        var aesKey = password.FillRepeatBytes(Aes256CbcHmacSha512.KeyByteSize);
        var hmacKey = "Aes256CbcHmacSha512".FillRepeatBytes(Aes256CbcHmacSha512.HmacKeyByteSize);
        var aes = new Aes256CbcHmacSha512(aesKey, hmacKey);
        var aes2 = new Aes256CbcHmacSha512(aes.AesKeyBase64, aes.HmacKeyBase64);
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
    public void Aes256CbcHmacSha512WithoutHmacTest(string password)
    {
        var aesKey = password.FillRepeatBytes(Aes256CbcHmacSha512.KeyByteSize);
        var aes = new Aes256CbcHmacSha512(aesKey);
        var aes2 = new Aes256CbcHmacSha512(aes.AesKeyBase64, aes.HmacKeyBase64);
        var str = "Hello word";
        var encrypt1 = aes.EncryptToBase64(str);
        var decrypt1 = aes2.DecryptFromBase64(encrypt1);
        Assert.AreEqual(str, decrypt1);

        var encrypt2 = aes2.EncryptToHex(str);
        var decrypt2 = aes.DecryptFromHex(encrypt2);
        Assert.AreEqual(str, decrypt2);
    }
}