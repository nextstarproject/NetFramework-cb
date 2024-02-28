using Nsp.Framework.Cryptography;

namespace Nsp.Framework.Cryptography.Test;

[TestClass]
public class ECDsaProviderTest
{
    private NspECDsaProvider _firstEcDsaProvider;
    public ECDsaProviderTest()
    {
        _firstEcDsaProvider = new NspECDsaProvider(SecurityAlgorithms.SHA512);
    }

    /// <summary>
    /// 基本的签名和校验
    /// </summary>
    [TestMethod]
    public void BasicSignAndVerify()
    {
        var x509Cer1 = _firstEcDsaProvider.ExportX509Certificate2();
        Assert.IsNotNull(x509Cer1);
        var signData = _firstEcDsaProvider.SignData("Hello Word");
        var verify = _firstEcDsaProvider.VerifyData("Hello Word", signData);
        Assert.IsTrue(verify);
    }

    /// <summary>
    /// 导入key，通过Parameters出来的值
    /// </summary>
    [TestMethod]
    public void ImportNewKey()
    {
        var publicKey = _firstEcDsaProvider.GetBase64PublicKey(true);
        var privateKey = _firstEcDsaProvider.GetBase64PrivateKey(true);
        using (var ecdsa2 = new NspECDsaProvider(privateKey, publicKey, SecurityAlgorithms.SHA512,true))
        {
            var publicKey2 = ecdsa2.GetBase64PublicKey(true);
            var privateKey2 = ecdsa2.GetBase64PrivateKey(true);
            Assert.AreEqual(publicKey, publicKey2);
            Assert.AreEqual(privateKey, privateKey2);
            var x509Cer2 = ecdsa2.ExportX509Certificate2();
            Assert.IsNotNull(x509Cer2);
        }
    }

    /// <summary>
    /// 导入key后签名和校验（现有和之前签名），通过Parameters出来的值
    /// </summary>
    [TestMethod]
    public void ImportSignAndVerify()
    {
        var publicKey = _firstEcDsaProvider.GetBase64PublicKey(true);
        var privateKey = _firstEcDsaProvider.GetBase64PrivateKey(true);
        var signData = _firstEcDsaProvider.SignData("Hello Word");
        using (var ecdsa2 = new NspECDsaProvider(privateKey, publicKey, SecurityAlgorithms.SHA512,true))
        {
            var publicKey2 = ecdsa2.GetBase64PublicKey(true);
            var privateKey2 = ecdsa2.GetBase64PrivateKey(true);
            Assert.AreEqual(publicKey, publicKey2);
            Assert.AreEqual(privateKey, privateKey2);
            var x509Cer2 = ecdsa2.ExportX509Certificate2();
            Assert.IsNotNull(x509Cer2);
            var aaa2 = ecdsa2.SignData("Hello Word");
            Assert.AreNotEqual(signData,aaa2);
            var bbb2 = ecdsa2.VerifyData("Hello Word", aaa2);
            var bbb3 = ecdsa2.VerifyData("Hello Word", signData);
            Assert.IsTrue(bbb2);
            Assert.IsTrue(bbb3);
            var bbb4 = ecdsa2.VerifyData("Hello Word!", signData);
            Assert.IsFalse(bbb4);
        }
    }

    /// <summary>
    /// 不使用Parameters，正常Pkcs8导出和导入，判断是否相等
    /// </summary>
    [TestMethod]
    public void NoParametersImportExport()
    {
        var publicKey = _firstEcDsaProvider.GetBase64PublicKey();
        var privateKey = _firstEcDsaProvider.GetBase64PrivateKey();
        using (var ecdsa2 = new NspECDsaProvider(privateKey, publicKey, SecurityAlgorithms.SHA512))
        {
            var publicKey2 = ecdsa2.GetBase64PublicKey();
            var privateKey2 = ecdsa2.GetBase64PrivateKey();
            Assert.AreEqual(publicKey, publicKey2);
            Assert.AreEqual(privateKey, privateKey2);
        }
    }
    
    /// <summary>
    /// 不使用Parameters，正常Pkcs8导出和导入，判断签名和校验是否正确
    /// </summary>
    [TestMethod]
    public void NoParametersImportSignAndVerify()
    {
        var publicKey = _firstEcDsaProvider.GetBase64PublicKey();
        var privateKey = _firstEcDsaProvider.GetBase64PrivateKey();
        var signData = _firstEcDsaProvider.SignData("Hello Word");
        using (var ecdsa2 = new NspECDsaProvider(privateKey, publicKey, SecurityAlgorithms.SHA512))
        {
            var publicKey2 = ecdsa2.GetBase64PublicKey();
            var privateKey2 = ecdsa2.GetBase64PrivateKey();
            Assert.AreEqual(publicKey, publicKey2);
            Assert.AreEqual(privateKey, privateKey2);
            var aaa2 = ecdsa2.SignData("Hello Word");
            Assert.AreNotEqual(signData,aaa2);
            var bbb2 = ecdsa2.VerifyData("Hello Word", aaa2);
            var bbb3 = ecdsa2.VerifyData("Hello Word", signData);
            Assert.IsTrue(bbb2);
            Assert.IsTrue(bbb3);
            var bbb4 = ecdsa2.VerifyData("Hello Word!", signData);
            Assert.IsFalse(bbb4);
        }
    }

}