﻿using Nsp.Framework.Cryptography;

namespace Nsp.Framework.Cryptography.Test;

[TestClass]
public class RsaPssProviderTest
{
    public RsaPssProviderTest()
    {
    }

    [TestMethod]
    [DataRow(1024)]
    [DataRow(2048)]
    [DataRow(3072)]
    [DataRow(4096)]
    public void BasicTest(int keySize)
    {
        var firstEcDsaProvider = new NspRsaPssProvider(keySize);
        var publicKey = firstEcDsaProvider.GetBase64PublicKey();
        var privateKey = firstEcDsaProvider.GetBase64PrivateKey();
        Assert.IsNotNull(publicKey);
        Assert.IsNotNull(privateKey);
        var xmlKey = firstEcDsaProvider.ExportXmlPublicAndPrivate();
        Assert.IsNotNull(xmlKey);
        var x509Cer1 = firstEcDsaProvider.ExportX509Certificate2();
        Assert.IsNotNull(x509Cer1);

        var str = Guid.NewGuid().ToString();
        var signData = firstEcDsaProvider.SignData(str);
        var isValid = firstEcDsaProvider.VerifyData(str, signData);
        Assert.IsTrue(isValid);
    }

    [TestMethod]
    [DataRow(1024)]
    [DataRow(2048)]
    [DataRow(3072)]
    [DataRow(4096)]
    public void SplitImportTest(int keySize)
    {
        var firstEcDsaProvider = new NspRsaPssProvider(keySize);
        var publicKey = firstEcDsaProvider.GetBase64PublicKey();
        var privateKey = firstEcDsaProvider.GetBase64PrivateKey();
        var rsa2 = new NspRsaPssProvider(privateKey, publicKey);
        var publicKey2 = rsa2.GetBase64PublicKey();
        var privateKey2 = rsa2.GetBase64PrivateKey();
        Assert.AreEqual(publicKey2, publicKey);
        Assert.AreEqual(privateKey2, privateKey);

        //原始
        var str = Guid.NewGuid().ToString();
        var signData = firstEcDsaProvider.SignData(str);
        var isValid = firstEcDsaProvider.VerifyData(str, signData);
        Assert.IsTrue(isValid);

        var aaa2 = rsa2.SignData(str);
        var bbb2 = rsa2.VerifyData(str, aaa2);
        var bbb3 = rsa2.VerifyData(str, signData);
        Assert.IsTrue(bbb2);
        Assert.IsTrue(bbb3);
    }

    [TestMethod]
    [DataRow(1024)]
    [DataRow(2048)]
    [DataRow(3072)]
    [DataRow(4096)]
    public void XmlImportTest(int keySize)
    {
        var firstEcDsaProvider = new NspRsaPssProvider(keySize);
        var xmlKey = firstEcDsaProvider.ExportXmlPublicAndPrivate();
        var rsa2 = new NspRsaPssProvider(xmlKey);
        var xmlKey2 = rsa2.ExportXmlPublicAndPrivate();
        Assert.AreEqual(xmlKey2, xmlKey);

        var x509Cer1 = firstEcDsaProvider.ExportX509Certificate2();
        Assert.IsNotNull(x509Cer1);
        var x509Cer2 = rsa2.ExportX509Certificate2();
        Assert.IsNotNull(x509Cer2);

        //原始
        var str = Guid.NewGuid().ToString();
        var signData = firstEcDsaProvider.SignData(str);
        var isValid = firstEcDsaProvider.VerifyData(str, signData);
        Assert.IsTrue(isValid);

        var aaa2 = rsa2.SignData(str);
        var bbb2 = rsa2.VerifyData(str, aaa2);
        var bbb3 = rsa2.VerifyData(str, signData);
        Assert.IsTrue(bbb2);
        Assert.IsTrue(bbb3);
    }
}