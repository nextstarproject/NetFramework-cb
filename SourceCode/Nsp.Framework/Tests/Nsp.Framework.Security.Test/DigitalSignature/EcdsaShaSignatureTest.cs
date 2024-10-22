using Nsp.Framework.Security.DigitalSignature;

namespace Nsp.Framework.Security.Test.DigitalSignature;

[TestClass]
public class EcdsaShaSignatureTest
{
    [TestMethod]
    public void EcdsaSha256SignatureTest()
    {
        var rsa = new EcdsaSha256Signature();
        var text = "EcdsaSha256Signature";
        var encrypt1 = rsa.GenerateSignatureToHex(text);
        var encrypt2 = rsa.GenerateSignatureToBase64(text);
        var publicKey = rsa.ExportPublicToBase64();
        var privateKey = rsa.ExportPrivateToBase64();

        var rsa2 = new EcdsaSha256Signature(privateKey, publicKey);
        var verify1 = rsa2.VerifySignatureFromHex(text, encrypt1);
        var verify2 = rsa2.VerifySignatureFromBase64(text, encrypt2);
        Assert.IsTrue(verify1);
        Assert.IsTrue(verify2);
    }
    
    [TestMethod]
    public void EcdsaSha256SignatureParameterTest()
    {
        var rsa = new EcdsaSha256Signature();
        var text = "EcdsaSha256Signature";
        var encrypt1 = rsa.GenerateSignatureToHex(text);
        var encrypt2 = rsa.GenerateSignatureToBase64(text);
        var parameters = rsa.ExportParameters();

        var rsa2 = new EcdsaSha256Signature(parameters);
        var verify1 = rsa2.VerifySignatureFromHex(text, encrypt1);
        var verify2 = rsa2.VerifySignatureFromBase64(text, encrypt2);
        Assert.IsTrue(verify1);
        Assert.IsTrue(verify2);
    }
    
    [TestMethod]
    public void EcdsaSha384SignatureTest()
    {
        var rsa = new EcdsaSha384Signature();
        var text = "EcdsaSha384Signature";
        var encrypt1 = rsa.GenerateSignatureToHex(text);
        var encrypt2 = rsa.GenerateSignatureToBase64(text);
        var publicKey = rsa.ExportPublicToBase64();
        var privateKey = rsa.ExportPrivateToBase64();

        var rsa2 = new EcdsaSha384Signature(privateKey, publicKey);
        var verify1 = rsa2.VerifySignatureFromHex(text, encrypt1);
        var verify2 = rsa2.VerifySignatureFromBase64(text, encrypt2);
        Assert.IsTrue(verify1);
        Assert.IsTrue(verify2);
    }
    
    [TestMethod]
    public void EcdsaSha384SignatureParameterTest()
    {
        var rsa = new EcdsaSha384Signature();
        var text = "EcdsaSha384Signature";
        var encrypt1 = rsa.GenerateSignatureToHex(text);
        var encrypt2 = rsa.GenerateSignatureToBase64(text);
        var parameters = rsa.ExportParameters();

        var rsa2 = new EcdsaSha384Signature(parameters);
        var verify1 = rsa2.VerifySignatureFromHex(text, encrypt1);
        var verify2 = rsa2.VerifySignatureFromBase64(text, encrypt2);
        Assert.IsTrue(verify1);
        Assert.IsTrue(verify2);
    }
    
    [TestMethod]
    public void EcdsaSha512SignatureTest()
    {
        var rsa = new EcdsaSha512Signature();
        var text = "EcdsaSha512Signature";
        var encrypt1 = rsa.GenerateSignatureToHex(text);
        var encrypt2 = rsa.GenerateSignatureToBase64(text);
        var publicKey = rsa.ExportPublicToBase64();
        var privateKey = rsa.ExportPrivateToBase64();

        var rsa2 = new EcdsaSha512Signature(privateKey, publicKey);
        var verify1 = rsa2.VerifySignatureFromHex(text, encrypt1);
        var verify2 = rsa2.VerifySignatureFromBase64(text, encrypt2);
        Assert.IsTrue(verify1);
        Assert.IsTrue(verify2);
    }
    
    [TestMethod]
    public void EcdsaSha512SignatureParameterTest()
    {
        var rsa = new EcdsaSha512Signature();
        var text = "EcdsaSha512Signature";
        var encrypt1 = rsa.GenerateSignatureToHex(text);
        var encrypt2 = rsa.GenerateSignatureToBase64(text);
        var parameters = rsa.ExportParameters();

        var rsa2 = new EcdsaSha512Signature(parameters);
        var verify1 = rsa2.VerifySignatureFromHex(text, encrypt1);
        var verify2 = rsa2.VerifySignatureFromBase64(text, encrypt2);
        Assert.IsTrue(verify1);
        Assert.IsTrue(verify2);
    }
}