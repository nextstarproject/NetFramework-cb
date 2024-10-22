using System.Security.Cryptography.X509Certificates;
using Nsp.Framework.Security.DigitalSignature;

namespace Nsp.Framework.Security.Test.DigitalSignature;

[TestClass]
public class RsaShaSignatureTest
{
    [TestMethod]
    public void RsaSha256SignatureTest()
    {
        var rsa = new RsaSha256Signature();
        var text = "RsaSha256Signature";
        var encrypt1 = rsa.GenerateSignatureToHex(text);
        var encrypt2 = rsa.GenerateSignatureToBase64(text);
        var publicKey = rsa.ExportPublicToBase64();
        var privateKey = rsa.ExportPrivateToBase64();

        var rsa2 = new RsaSha256Signature(privateKey, publicKey);
        var verify1 = rsa2.VerifySignatureFromHex(text, encrypt1);
        var verify2 = rsa2.VerifySignatureFromBase64(text, encrypt2);
        
        var encrypt3 = rsa2.GenerateSignatureToHex(text);
        var verify3 = rsa2.VerifySignatureFromHex(text, encrypt3);
        Assert.IsTrue(verify1);
        Assert.IsTrue(verify2);
        Assert.IsTrue(verify3);
    }
    
    [TestMethod]
    public void RsaSha256SignatureParameterTest()
    {
        var rsa = new RsaSha256Signature();
        var text = "RsaSha256Signature";
        var encrypt1 = rsa.GenerateSignatureToHex(text);
        var encrypt2 = rsa.GenerateSignatureToBase64(text);
        var parameters = rsa.ExportParameters();

        var rsa2 = new RsaSha256Signature(parameters);
        var verify1 = rsa2.VerifySignatureFromHex(text, encrypt1);
        var verify2 = rsa2.VerifySignatureFromBase64(text, encrypt2);
        
        var encrypt3 = rsa2.GenerateSignatureToHex(text);
        var verify3 = rsa2.VerifySignatureFromHex(text, encrypt3);
        Assert.IsTrue(verify1);
        Assert.IsTrue(verify2);
        Assert.IsTrue(verify3);
    }
    
    [TestMethod]
    public void RsaSha256SignatureX509Certificate2Test()
    {
        var rsa = new RsaSha256Signature();
        var text = "RsaSha256Signature";
        var encrypt1 = rsa.GenerateSignatureToHex(text);
        var encrypt2 = rsa.GenerateSignatureToBase64(text);
        var certificate2 = rsa.ExportX509Certificate2();
        
        var rsa2 = new RsaSha256Signature(certificate2);
        var verify1 = rsa2.VerifySignatureFromHex(text, encrypt1);
        var verify2 = rsa2.VerifySignatureFromBase64(text, encrypt2);
        
        var encrypt3 = rsa2.GenerateSignatureToHex(text);
        var verify3 = rsa2.VerifySignatureFromHex(text, encrypt3);
        Assert.IsTrue(verify1);
        Assert.IsTrue(verify2);
        Assert.IsTrue(verify3);
    }
    
    [TestMethod]
    public void RsaSha384SignatureTest()
    {
        var rsa = new RsaSha384Signature();
        var text = "RsaSha384Signature";
        var encrypt1 = rsa.GenerateSignatureToHex(text);
        var encrypt2 = rsa.GenerateSignatureToBase64(text);
        var publicKey = rsa.ExportPublicToBase64();
        var privateKey = rsa.ExportPrivateToBase64();

        var rsa2 = new RsaSha384Signature(privateKey, publicKey);
        var verify1 = rsa2.VerifySignatureFromHex(text, encrypt1);
        var verify2 = rsa2.VerifySignatureFromBase64(text, encrypt2);
        
        var encrypt3 = rsa2.GenerateSignatureToHex(text);
        var verify3 = rsa2.VerifySignatureFromHex(text, encrypt3);
        Assert.IsTrue(verify1);
        Assert.IsTrue(verify2);
        Assert.IsTrue(verify3);
    }
    
    [TestMethod]
    public void RsaSha384SignatureParameterTest()
    {
        var rsa = new RsaSha384Signature();
        var text = "RsaSha384Signature";
        var encrypt1 = rsa.GenerateSignatureToHex(text);
        var encrypt2 = rsa.GenerateSignatureToBase64(text);
        var parameters = rsa.ExportParameters();

        var rsa2 = new RsaSha384Signature(parameters);
        var verify1 = rsa2.VerifySignatureFromHex(text, encrypt1);
        var verify2 = rsa2.VerifySignatureFromBase64(text, encrypt2);
        
        var encrypt3 = rsa2.GenerateSignatureToHex(text);
        var verify3 = rsa2.VerifySignatureFromHex(text, encrypt3);
        Assert.IsTrue(verify1);
        Assert.IsTrue(verify2);
        Assert.IsTrue(verify3);
    }
    
    [TestMethod]
    public void RsaSha384SignatureX509Certificate2Test()
    {
        var rsa = new RsaSha384Signature();
        var text = "RsaSha384Signature";
        var encrypt1 = rsa.GenerateSignatureToHex(text);
        var encrypt2 = rsa.GenerateSignatureToBase64(text);
        var certificate2 = rsa.ExportX509Certificate2();
        
        var rsa2 = new RsaSha384Signature(certificate2);
        var verify1 = rsa2.VerifySignatureFromHex(text, encrypt1);
        var verify2 = rsa2.VerifySignatureFromBase64(text, encrypt2);
        
        var encrypt3 = rsa2.GenerateSignatureToHex(text);
        var verify3 = rsa2.VerifySignatureFromHex(text, encrypt3);
        Assert.IsTrue(verify1);
        Assert.IsTrue(verify2);
        Assert.IsTrue(verify3);
    }
    
    [TestMethod]
    public void RsaSha512SignatureTest()
    {
        var rsa = new RsaSha512Signature();
        var text = "RsaSha512Signature";
        var encrypt1 = rsa.GenerateSignatureToHex(text);
        var encrypt2 = rsa.GenerateSignatureToBase64(text);
        var publicKey = rsa.ExportPublicToBase64();
        var privateKey = rsa.ExportPrivateToBase64();

        var rsa2 = new RsaSha512Signature(privateKey, publicKey);
        var verify1 = rsa2.VerifySignatureFromHex(text, encrypt1);
        var verify2 = rsa2.VerifySignatureFromBase64(text, encrypt2);
        
        var encrypt3 = rsa2.GenerateSignatureToHex(text);
        var verify3 = rsa2.VerifySignatureFromHex(text, encrypt3);
        Assert.IsTrue(verify1);
        Assert.IsTrue(verify2);
        Assert.IsTrue(verify3);
    }
    
    [TestMethod]
    public void RsaSha512SignatureParameterTest()
    {
        var rsa = new RsaSha512Signature();
        var text = "RsaSha512Signature";
        var encrypt1 = rsa.GenerateSignatureToHex(text);
        var encrypt2 = rsa.GenerateSignatureToBase64(text);
        var parameters = rsa.ExportParameters();

        var rsa2 = new RsaSha512Signature(parameters);
        var verify1 = rsa2.VerifySignatureFromHex(text, encrypt1);
        var verify2 = rsa2.VerifySignatureFromBase64(text, encrypt2);
        
        var encrypt3 = rsa2.GenerateSignatureToHex(text);
        var verify3 = rsa2.VerifySignatureFromHex(text, encrypt3);
        Assert.IsTrue(verify1);
        Assert.IsTrue(verify2);
        Assert.IsTrue(verify3);
    }
    
    [TestMethod]
    public void RsaSha512SignatureX509Certificate2Test()
    {
        var rsa = new RsaSha512Signature();
        var text = "RsaSha512Signature";
        var encrypt1 = rsa.GenerateSignatureToHex(text);
        var encrypt2 = rsa.GenerateSignatureToBase64(text);
        var certificate2 = rsa.ExportX509Certificate2();
        
        var rsa2 = new RsaSha512Signature(certificate2);
        var verify1 = rsa2.VerifySignatureFromHex(text, encrypt1);
        var verify2 = rsa2.VerifySignatureFromBase64(text, encrypt2);
        
        var encrypt3 = rsa2.GenerateSignatureToHex(text);
        var verify3 = rsa2.VerifySignatureFromHex(text, encrypt3);
        Assert.IsTrue(verify1);
        Assert.IsTrue(verify2);
        Assert.IsTrue(verify3);
    }
}