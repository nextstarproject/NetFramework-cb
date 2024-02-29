using Nsp.Framework.Cryptography;

namespace Nsp.Framework.Cryptography.Test;

[TestClass]
public class ECDsaProviderTest
{
    public ECDsaProviderTest()
    {
    }

    /// <summary>
    /// 基本的签名和校验
    /// </summary>
    [TestMethod]
    [DataRow(NspSecurityAlgorithms.SHA256)]
    [DataRow(NspSecurityAlgorithms.SHA384)]
    [DataRow(NspSecurityAlgorithms.SHA512)]
    public void BasicSignAndVerify(NspSecurityAlgorithms algorithms)
    {
        var firstEcDsaProvider = new NspECDsaProvider(algorithms);
        var x509Cer1 = firstEcDsaProvider.ExportX509Certificate2();
        Assert.IsNotNull(x509Cer1);
        var signData = firstEcDsaProvider.SignData("Hello Word");
        var verify = firstEcDsaProvider.VerifyData("Hello Word", signData);
        Assert.IsTrue(verify);
    }

    /// <summary>
    /// 导入key，通过Parameters出来的值
    /// </summary>
    [TestMethod]
    [DataRow(NspSecurityAlgorithms.SHA256)]
    [DataRow(NspSecurityAlgorithms.SHA384)]
    [DataRow(NspSecurityAlgorithms.SHA512)]
    public void ImportNewKey(NspSecurityAlgorithms algorithms)
    {
        var firstEcDsaProvider = new NspECDsaProvider(algorithms);
        var publicKey = firstEcDsaProvider.GetBase64PublicKey(true);
        var privateKey = firstEcDsaProvider.GetBase64PrivateKey(true);
        using (var ecdsa2 = new NspECDsaProvider(privateKey, publicKey, algorithms,true))
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
    [DataRow(NspSecurityAlgorithms.SHA256)]
    [DataRow(NspSecurityAlgorithms.SHA384)]
    [DataRow(NspSecurityAlgorithms.SHA512)]
    public void ImportSignAndVerify(NspSecurityAlgorithms algorithms)
    {
        var firstEcDsaProvider = new NspECDsaProvider(algorithms);
        var publicKey = firstEcDsaProvider.GetBase64PublicKey(true);
        var privateKey = firstEcDsaProvider.GetBase64PrivateKey(true);
        var signData = firstEcDsaProvider.SignData("Hello Word");
        using (var ecdsa2 = new NspECDsaProvider(privateKey, publicKey, algorithms,true))
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
    [DataRow(NspSecurityAlgorithms.SHA256)]
    [DataRow(NspSecurityAlgorithms.SHA384)]
    [DataRow(NspSecurityAlgorithms.SHA512)]
    public void NoParametersImportExport(NspSecurityAlgorithms algorithms)
    {
        var firstEcDsaProvider = new NspECDsaProvider(algorithms);
        var publicKey = firstEcDsaProvider.GetBase64PublicKey();
        var privateKey = firstEcDsaProvider.GetBase64PrivateKey();
        using (var ecdsa2 = new NspECDsaProvider(privateKey, publicKey, algorithms))
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
    [DataRow(NspSecurityAlgorithms.SHA256)]
    [DataRow(NspSecurityAlgorithms.SHA384)]
    [DataRow(NspSecurityAlgorithms.SHA512)]
    public void NoParametersImportSignAndVerify(NspSecurityAlgorithms algorithms)
    {
        var firstEcDsaProvider = new NspECDsaProvider(algorithms);
        var publicKey = firstEcDsaProvider.GetBase64PublicKey();
        var privateKey = firstEcDsaProvider.GetBase64PrivateKey();
        var signData = firstEcDsaProvider.SignData("Hello Word");
        using (var ecdsa2 = new NspECDsaProvider(privateKey, publicKey, algorithms))
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