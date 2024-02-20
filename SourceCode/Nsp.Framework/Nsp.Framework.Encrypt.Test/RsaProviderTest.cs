namespace Nsp.Framework.Encrypt.Test;

[TestClass]
public class RsaProviderTest
{
    private NspRsaProvider _firstRsaProvider;
    public RsaProviderTest()
    {
        _firstRsaProvider = new NspRsaProvider();
    }

    [TestMethod]
    public void BasicTest()
    {
        var publicKey = _firstRsaProvider.GetBase64PublicKey();
        var privateKey = _firstRsaProvider.GetBase64PrivateKey();
        var xmlKey = _firstRsaProvider.ExportXmlPublicAndPrivate();
        Console.WriteLine(publicKey);
        Console.WriteLine(privateKey);
        var x509Cer1 = _firstRsaProvider.ExportX509Certificate2();
        Assert.IsNotNull(x509Cer1);
        
        var str = Guid.NewGuid().ToString();
        var signData = _firstRsaProvider.SignData(str);
        var isValid = _firstRsaProvider.VerifyData(str, signData);
        Assert.IsTrue(isValid);
        
        var encryptData = _firstRsaProvider.Encrypt(str);
        var originData = _firstRsaProvider.Decrypt(encryptData);
        Assert.AreEqual(originData,str);
    }

    [TestMethod]
    public void SplitImportTest()
    {
        var publicKey = _firstRsaProvider.GetBase64PublicKey();
        var privateKey = _firstRsaProvider.GetBase64PrivateKey();
        var rsa2 = new NspRsaProvider(privateKey, publicKey);
        var publicKey2 = rsa2.GetBase64PublicKey();
        var privateKey2 = rsa2.GetBase64PrivateKey();
        Assert.AreEqual(publicKey2, publicKey);
        Assert.AreEqual(privateKey2, privateKey);
        
        //原始
        var str = Guid.NewGuid().ToString();
        var signData = _firstRsaProvider.SignData(str);
        var isValid = _firstRsaProvider.VerifyData(str, signData);
        Assert.IsTrue(isValid);
        
        var encryptData = _firstRsaProvider.Encrypt(str);
        var originData = _firstRsaProvider.Decrypt(encryptData);
        Assert.AreEqual(originData,str);
        
        
        var aaa2 = rsa2.SignData(str);
        var bbb2 = rsa2.VerifyData(str, aaa2);
        var bbb3 = rsa2.VerifyData(str, signData);
        Assert.IsTrue(bbb2);
        Assert.IsTrue(bbb3);
                
        var ccc1 = rsa2.Encrypt(str);
        var ddd1 = rsa2.Decrypt(encryptData);
        var ddd2 = rsa2.Decrypt(ccc1);
        Assert.AreEqual(ddd1,str);
        Assert.AreEqual(ddd2,str);
    }
    
    [TestMethod]
    public void XmlImportTest()
    {
        var xmlKey = _firstRsaProvider.ExportXmlPublicAndPrivate();
        var rsa2 = new NspRsaProvider(xmlKey);
        var xmlKey2 = rsa2.ExportXmlPublicAndPrivate();
        Assert.AreEqual(xmlKey2, xmlKey);
        
        var x509Cer1 = _firstRsaProvider.ExportX509Certificate2();
        Assert.IsNotNull(x509Cer1);
        var x509Cer2 = rsa2.ExportX509Certificate2();
        Assert.IsNotNull(x509Cer2);
        
        //原始
        var str = Guid.NewGuid().ToString();
        var signData = _firstRsaProvider.SignData(str);
        var isValid = _firstRsaProvider.VerifyData(str, signData);
        Assert.IsTrue(isValid);
        
        var encryptData = _firstRsaProvider.Encrypt(str);
        var originData = _firstRsaProvider.Decrypt(encryptData);
        Assert.AreEqual(originData,str);
        
        
        var aaa2 = rsa2.SignData(str);
        var bbb2 = rsa2.VerifyData(str, aaa2);
        var bbb3 = rsa2.VerifyData(str, signData);
        Assert.IsTrue(bbb2);
        Assert.IsTrue(bbb3);
                
        var ccc1 = rsa2.Encrypt(str);
        var ddd1 = rsa2.Decrypt(encryptData);
        var ddd2 = rsa2.Decrypt(ccc1);
        Assert.AreEqual(ddd1,str);
        Assert.AreEqual(ddd2,str);
    }
}