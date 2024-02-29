namespace Nsp.Framework.Core.Test;

[TestClass]
public class RandomStringTest
{
    [TestMethod]
    [DataRow(10)]
    [DataRow(20)]
    [DataRow(30)]
    [DataRow(40)]
    [DataRow(50)]
    [DataRow(60)]
    [DataRow(70)]
    [DataRow(80)]
    public void MainRandom(int length)
    {
        var randomBase64 = RandomStringUtil.CreateBase64UniqueId(length);
        var randomHexUniqueId = RandomStringUtil.CreateHexUniqueId(length);
        var randomHex = RandomStringUtil.CreateRandomHexKey(length);
        Assert.AreEqual(length,randomBase64.Length);
        Assert.AreEqual(length,randomHexUniqueId.Length);
        Assert.AreEqual(length,randomHex.Length);
    }
    
    [TestMethod]
    [DataRow(10)]
    [DataRow(20)]
    [DataRow(30)]
    [DataRow(40)]
    [DataRow(50)]
    [DataRow(60)]
    [DataRow(70)]
    [DataRow(80)]
    public void TypeRandom(int length)
    {
        var urlSafe = RandomStringUtil.UrlSafe(length);
        var numeric = RandomStringUtil.Numeric(length);
        var distinguishable = RandomStringUtil.Distinguishable(length);
        var asciiPrintable = RandomStringUtil.AsciiPrintable(length);
        var alphanumeric = RandomStringUtil.Alphanumeric(length);
        Assert.AreEqual(length,urlSafe.Length);
        Assert.AreEqual(length,numeric.Length);
        Assert.AreEqual(length,distinguishable.Length);
        Assert.AreEqual(length,asciiPrintable.Length);
        Assert.AreEqual(length,alphanumeric.Length);
    }
}