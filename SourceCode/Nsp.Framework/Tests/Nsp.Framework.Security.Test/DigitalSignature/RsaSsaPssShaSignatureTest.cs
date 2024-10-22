using Nsp.Framework.Security.DigitalSignature;

namespace Nsp.Framework.Security.Test.DigitalSignature;

[TestClass]
public class RsaSsaPssShaSignatureTest
{
    [TestMethod]
    public void RsaSsaPssSha256SignatureTest()
    {
        var rsa = new RsaSsaPssSha256Signature();
        var text = "RsaSsaPssSha256Signature";
        var encrypt1 = rsa.GenerateSignatureToHex(text);
        var encrypt2 = rsa.GenerateSignatureToBase64(text);
        var publicKey = rsa.ExportPublicToBase64();
        var privateKey = rsa.ExportPrivateToBase64();

        var rsa2 = new RsaSsaPssSha256Signature(privateKey, publicKey);
        var verify1 = rsa2.VerifySignatureFromHex(text, encrypt1);
        var verify2 = rsa2.VerifySignatureFromBase64(text, encrypt2);
        Assert.IsTrue(verify1);
        Assert.IsTrue(verify2);
    }
    
    [TestMethod]
    public void RsaSsaPssSha256SignatureParameterTest()
    {
        var rsa = new RsaSsaPssSha256Signature();
        var text = "RsaSsaPssSha256Signature";
        var encrypt1 = rsa.GenerateSignatureToHex(text);
        var encrypt2 = rsa.GenerateSignatureToBase64(text);
        var parameters = rsa.ExportParameters();

        var rsa2 = new RsaSsaPssSha256Signature(parameters);
        var verify1 = rsa2.VerifySignatureFromHex(text, encrypt1);
        var verify2 = rsa2.VerifySignatureFromBase64(text, encrypt2);
        Assert.IsTrue(verify1);
        Assert.IsTrue(verify2);
    }
    
    [TestMethod]
    public void RsaSsaPssSha384SignatureTest()
    {
        var rsa = new RsaSsaPssSha384Signature();
        var text = "RsaSsaPssSha384Signature";
        var encrypt1 = rsa.GenerateSignatureToHex(text);
        var encrypt2 = rsa.GenerateSignatureToBase64(text);
        var publicKey = rsa.ExportPublicToBase64();
        var privateKey = rsa.ExportPrivateToBase64();

        var rsa2 = new RsaSsaPssSha384Signature(privateKey, publicKey);
        var verify1 = rsa2.VerifySignatureFromHex(text, encrypt1);
        var verify2 = rsa2.VerifySignatureFromBase64(text, encrypt2);
        Assert.IsTrue(verify1);
        Assert.IsTrue(verify2);
    }
    
    [TestMethod]
    public void RsaSsaPssSha384SignatureParameterTest()
    {
        var rsa = new RsaSsaPssSha384Signature();
        var text = "RsaSsaPssSha384Signature";
        var encrypt1 = rsa.GenerateSignatureToHex(text);
        var encrypt2 = rsa.GenerateSignatureToBase64(text);
        var parameters = rsa.ExportParameters();

        var rsa2 = new RsaSsaPssSha384Signature(parameters);
        var verify1 = rsa2.VerifySignatureFromHex(text, encrypt1);
        var verify2 = rsa2.VerifySignatureFromBase64(text, encrypt2);
        Assert.IsTrue(verify1);
        Assert.IsTrue(verify2);
    }
    
    [TestMethod]
    public void RsaSsaPssSha512SignatureTest()
    {
        var rsa = new RsaSsaPssSha512Signature();
        var text = "RsaSsaPssSha512Signature";
        var encrypt1 = rsa.GenerateSignatureToHex(text);
        var encrypt2 = rsa.GenerateSignatureToBase64(text);
        var publicKey = rsa.ExportPublicToBase64();
        var privateKey = rsa.ExportPrivateToBase64();

        var rsa2 = new RsaSsaPssSha512Signature(privateKey, publicKey);
        var verify1 = rsa2.VerifySignatureFromHex(text, encrypt1);
        var verify2 = rsa2.VerifySignatureFromBase64(text, encrypt2);
        Assert.IsTrue(verify1);
        Assert.IsTrue(verify2);
    }
    
    [TestMethod]
    public void RsaSsaPssSha512SignatureParameterTest()
    {
        var rsa = new RsaSsaPssSha512Signature();
        var text = "RsaSsaPssSha512Signature";
        var encrypt1 = rsa.GenerateSignatureToHex(text);
        var encrypt2 = rsa.GenerateSignatureToBase64(text);
        var parameters = rsa.ExportParameters();

        var rsa2 = new RsaSsaPssSha512Signature(parameters);
        var verify1 = rsa2.VerifySignatureFromHex(text, encrypt1);
        var verify2 = rsa2.VerifySignatureFromBase64(text, encrypt2);
        Assert.IsTrue(verify1);
        Assert.IsTrue(verify2);
    }
}