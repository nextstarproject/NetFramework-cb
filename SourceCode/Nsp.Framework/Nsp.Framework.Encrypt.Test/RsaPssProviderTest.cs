using Nsp.Framework.Cryptography;

namespace Nsp.Framework.Encrypt.Test;

[TestClass]
public class RsaPssProviderTest
{
    private NspRsaPssProvider _firstRsaPssProvider;
    public RsaPssProviderTest()
    {
        _firstRsaPssProvider = new NspRsaPssProvider();
    }

    [TestMethod]
    public void BasicTest()
    {
        var publicKey = _firstRsaPssProvider.GetBase64PublicKey();
        var privateKey = _firstRsaPssProvider.GetBase64PrivateKey();
        Assert.IsNotNull(publicKey);
        Assert.IsNotNull(privateKey);
        var xmlKey = _firstRsaPssProvider.ExportXmlPublicAndPrivate();
        Assert.IsNotNull(xmlKey);
        var x509Cer1 = _firstRsaPssProvider.ExportX509Certificate2();
        Assert.IsNotNull(x509Cer1);
        
        var str = Guid.NewGuid().ToString();
        var signData = _firstRsaPssProvider.SignData(str);
        var isValid = _firstRsaPssProvider.VerifyData(str, signData);
        Assert.IsTrue(isValid);
    }

    [TestMethod]
    public void SplitImportTest()
    {
        var publicKey = _firstRsaPssProvider.GetBase64PublicKey();
        var privateKey = _firstRsaPssProvider.GetBase64PrivateKey();
        var rsa2 = new NspRsaPssProvider(privateKey, publicKey);
        var publicKey2 = rsa2.GetBase64PublicKey();
        var privateKey2 = rsa2.GetBase64PrivateKey();
        Assert.AreEqual(publicKey2, publicKey);
        Assert.AreEqual(privateKey2, privateKey);
        
        //原始
        var str = Guid.NewGuid().ToString();
        var signData = _firstRsaPssProvider.SignData(str);
        var isValid = _firstRsaPssProvider.VerifyData(str, signData);
        Assert.IsTrue(isValid);
        
        var aaa2 = rsa2.SignData(str);
        var bbb2 = rsa2.VerifyData(str, aaa2);
        var bbb3 = rsa2.VerifyData(str, signData);
        Assert.IsTrue(bbb2);
        Assert.IsTrue(bbb3);
    }
    
    [TestMethod]
    public void XmlImportTest()
    {
        var xmlKey = _firstRsaPssProvider.ExportXmlPublicAndPrivate();
        var rsa2 = new NspRsaPssProvider(xmlKey);
        var xmlKey2 = rsa2.ExportXmlPublicAndPrivate();
        Assert.AreEqual(xmlKey2, xmlKey);
        
        var x509Cer1 = _firstRsaPssProvider.ExportX509Certificate2();
        Assert.IsNotNull(x509Cer1);
        var x509Cer2 = rsa2.ExportX509Certificate2();
        Assert.IsNotNull(x509Cer2);
        
        //原始
        var str = Guid.NewGuid().ToString();
        var signData = _firstRsaPssProvider.SignData(str);
        var isValid = _firstRsaPssProvider.VerifyData(str, signData);
        Assert.IsTrue(isValid);
        
        var aaa2 = rsa2.SignData(str);
        var bbb2 = rsa2.VerifyData(str, aaa2);
        var bbb3 = rsa2.VerifyData(str, signData);
        Assert.IsTrue(bbb2);
        Assert.IsTrue(bbb3);
    }
}