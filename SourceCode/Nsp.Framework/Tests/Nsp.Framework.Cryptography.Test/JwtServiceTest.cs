using Microsoft.IdentityModel.Tokens;

namespace Nsp.Framework.Cryptography.Test;

[TestClass]
public class JwtServiceTest
{
    [TestMethod]
    [DataRow(1024)]
    [DataRow(2048)]
    [DataRow(3072)]
    [DataRow(4096)]
    public void RsaSha256NormalTest(int keySize)
    {
        var firstEcDsaProvider = new NspRsaProvider(keySize);
        var x509Cer1 = firstEcDsaProvider.ExportSecurityKey();
        var jwt = new JwtService(x509Cer1);
        var token = jwt.GenerateToken(DateTime.Now, DateTime.Now.AddDays(1), new Dictionary<string, string>()
        {
            {
                "data", "一些测试数据"
            }
        });
        var valida = jwt.ValidateToken(token);
        Assert.IsTrue(valida);
        var claims = jwt.GetAllClaims(token);
        var keyExist = claims.ContainsKey("data");
        var value = claims.GetValueOrDefault("data");
        Assert.IsTrue(keyExist);
        Assert.AreEqual("一些测试数据", value);
    }
    
    [TestMethod]
    [DataRow(1024)]
    [DataRow(2048)]
    [DataRow(3072)]
    [DataRow(4096)]
    public void RsaSha384NormalTest(int keySize)
    {
        var firstEcDsaProvider = new NspRsaProvider(keySize);
        var x509Cer1 = firstEcDsaProvider.ExportSecurityKey();
        var jwt = new JwtService(x509Cer1, SecurityAlgorithms.RsaSha384);
        var token = jwt.GenerateToken(DateTime.Now, DateTime.Now.AddDays(1), new Dictionary<string, string>()
        {
            {
                "data", "一些测试数据"
            }
        });
        var valida = jwt.ValidateToken(token);
        Assert.IsTrue(valida);
        var claims = jwt.GetAllClaims(token);
        var keyExist = claims.ContainsKey("data");
        var value = claims.GetValueOrDefault("data");
        Assert.IsTrue(keyExist);
        Assert.AreEqual("一些测试数据", value);
    }
    
    [TestMethod]
    [DataRow(1024)]
    [DataRow(2048)]
    [DataRow(3072)]
    [DataRow(4096)]
    public void RsaSha512NormalTest(int keySize)
    {
        var firstEcDsaProvider = new NspRsaProvider(keySize);
        var x509Cer1 = firstEcDsaProvider.ExportSecurityKey();
        var jwt = new JwtService(x509Cer1, SecurityAlgorithms.RsaSha512);
        var token = jwt.GenerateToken(DateTime.Now, DateTime.Now.AddDays(1), new Dictionary<string, string>()
        {
            {
                "data", "一些测试数据"
            }
        });
        var valida = jwt.ValidateToken(token);
        Assert.IsTrue(valida);
        var claims = jwt.GetAllClaims(token);
        var keyExist = claims.ContainsKey("data");
        var value = claims.GetValueOrDefault("data");
        Assert.IsTrue(keyExist);
        Assert.AreEqual("一些测试数据", value);
    }
    
    [TestMethod]
    [DataRow(NspSecurityAlgorithms.SHA256)]
    [DataRow(NspSecurityAlgorithms.SHA384)]
    [DataRow(NspSecurityAlgorithms.SHA512)]
    public void ECDsaNormalTest(NspSecurityAlgorithms algorithms)
    {
        var firstEcDsaProvider = new NspECDsaProvider(algorithms);
        var x509Cer1 = firstEcDsaProvider.ExportSecurityKey();
        var jwt = new JwtService(x509Cer1);
        var token = jwt.GenerateToken(DateTime.Now, DateTime.Now.AddDays(1), new Dictionary<string, string>()
        {
            {
                "data", "一些测试数据"
            }
        });
        var valida = jwt.ValidateToken(token);
        Assert.IsTrue(valida);
        var claims = jwt.GetAllClaims(token);
        var keyExist = claims.ContainsKey("data");
        var value = claims.GetValueOrDefault("data");
        Assert.IsTrue(keyExist);
        Assert.AreEqual("一些测试数据", value);
    }
    
    [TestMethod]
    [DataRow(1024)]
    [DataRow(2048)]
    [DataRow(3072)]
    [DataRow(4096)]
    public void RsaPssSha256NormalTest(int keySize)
    {
        var firstEcDsaProvider = new NspRsaPssProvider(keySize);
        var x509Cer1 = firstEcDsaProvider.ExportSecurityKey();
        var jwt = new JwtService(x509Cer1);
        var token = jwt.GenerateToken(DateTime.Now, DateTime.Now.AddDays(1), new Dictionary<string, string>()
        {
            {
                "data", "一些测试数据"
            }
        });
        var valida = jwt.ValidateToken(token);
        Assert.IsTrue(valida);
        var claims = jwt.GetAllClaims(token);
        var keyExist = claims.ContainsKey("data");
        var value = claims.GetValueOrDefault("data");
        Assert.IsTrue(keyExist);
        Assert.AreEqual("一些测试数据", value);
    }
    
    [TestMethod]
    [DataRow(1024)]
    [DataRow(2048)]
    [DataRow(3072)]
    [DataRow(4096)]
    public void RsaPssSha384NormalTest(int keySize)
    {
        var firstEcDsaProvider = new NspRsaPssProvider(keySize);
        var x509Cer1 = firstEcDsaProvider.ExportSecurityKey();
        var jwt = new JwtService(x509Cer1, SecurityAlgorithms.RsaSha384);
        var token = jwt.GenerateToken(DateTime.Now, DateTime.Now.AddDays(1), new Dictionary<string, string>()
        {
            {
                "data", "一些测试数据"
            }
        });
        var valida = jwt.ValidateToken(token);
        Assert.IsTrue(valida);
        var claims = jwt.GetAllClaims(token);
        var keyExist = claims.ContainsKey("data");
        var value = claims.GetValueOrDefault("data");
        Assert.IsTrue(keyExist);
        Assert.AreEqual("一些测试数据", value);
    }
    
    [TestMethod]
    [DataRow(1024)]
    [DataRow(2048)]
    [DataRow(3072)]
    [DataRow(4096)]
    public void RsaPssSha512NormalTest(int keySize)
    {
        var firstEcDsaProvider = new NspRsaPssProvider(keySize);
        var x509Cer1 = firstEcDsaProvider.ExportSecurityKey();
        var jwt = new JwtService(x509Cer1, SecurityAlgorithms.RsaSha512);
        var token = jwt.GenerateToken(DateTime.Now, DateTime.Now.AddDays(1), new Dictionary<string, string>()
        {
            {
                "data", "一些测试数据"
            }
        });
        var valida = jwt.ValidateToken(token);
        Assert.IsTrue(valida);
        var claims = jwt.GetAllClaims(token);
        var keyExist = claims.ContainsKey("data");
        var value = claims.GetValueOrDefault("data");
        Assert.IsTrue(keyExist);
        Assert.AreEqual("一些测试数据", value);
    }
}