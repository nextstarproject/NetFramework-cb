using Nsp.Framework.Core;

namespace Nsp.Framework.Cryptography.Test;

[TestClass]
public class HmacProviderTest
{
    [TestMethod]
    public void SignDataAndVerifyData()
    {
        var hamc = new NspHmacProvider(RandomStringUtil.CreateRandomHexKey(10));
        var signData = hamc.SignData("Hello word");
        var result = hamc.VerifyData("Hello word", signData);
        Assert.IsTrue(result);
    }
    
    [TestMethod]
    [DataRow("123456")]
    [DataRow("ab456789")]
    public void DynamicPassword(string password)
    {
        var hamc = new NspHmacProvider(password);
        var signData = hamc.SignData("Hello word");
        var result = hamc.VerifyData("Hello word", signData);
        Assert.IsTrue(result);
    }
    
    [TestMethod]
    [DataRow(NspSecurityAlgorithms.SHA256)]
    [DataRow(NspSecurityAlgorithms.SHA384)]
    [DataRow(NspSecurityAlgorithms.SHA512)]
    public void DynamicAlgorithms(NspSecurityAlgorithms algorithms)
    {
        var hamc = new NspHmacProvider("password", algorithms);
        var signData = hamc.SignData("Hello word");
        var result = hamc.VerifyData("Hello word", signData);
        Assert.IsTrue(result);
    }
}