namespace Nsp.Framework.Cryptography.Test;

[TestClass]
public class StringCryptographyTest
{
    [TestMethod]
    public void Md5HexTest()
    {
        var text = "hello word";
        var actual = "13574ef0d58b50fab38ec841efe39df4";
        var expected = text.ToMd5();
        Assert.AreEqual(expected,actual.ToUpper());
    }
    
    [TestMethod]
    public void Sha1HexTest()
    {
        var text = "hello word";
        var actual = "e0738b87e67bbfc9c5b77556665064446430e81c";
        var expected = text.ToSha1();
        Assert.AreEqual(expected,actual.ToUpper());
    }
    
    [TestMethod]
    public void Sha256HexTest()
    {
        var text = "hello word";
        var actual = "f0da559ea59ced68b4d657496bee9753c0447d70702af1a351c7577226d97723";
        var expected = text.ToSha256();
        Assert.AreEqual(expected,actual.ToUpper());
    }
    
    [TestMethod]
    public void Sha384HexTest()
    {
        var text = "hello word";
        var actual = "a58d27ee06211edc7a64f199b7da55fd0fe31d98b2c949f83fbb95bc7fc3114d7957ca5a3ec4b489a026356135681782";
        var expected = text.ToSha384();
        Assert.AreEqual(expected,actual.ToUpper());
    }
    
    [TestMethod]
    public void Sha512HexTest()
    {
        var text = "hello word";
        var actual = "86dfecbd488d84481bdfc5d54f52734fd40298ef68da014095a52889a35a596a3e64a9ea64f005caaa4b4d2b11d9a69f12214a31b79bbddc0872fa7561200bd2";
        var expected = text.ToSha512();
        Assert.AreEqual(expected,actual.ToUpper());
    }
    
    
    [TestMethod]
    public void Md5Base64Test()
    {
        var text = "hello word";
        var actual = "E1dO8NWLUPqzjshB7+Od9A==";
        var expected = text.ToMd5Base64();
        Assert.AreEqual(expected,actual);
    }
    
    [TestMethod]
    public void Sha1Base64Test()
    {
        var text = "hello word";
        var actual = "4HOLh+Z7v8nFt3VWZlBkRGQw6Bw=";
        var expected = text.ToSha1Base64();
        Assert.AreEqual(expected,actual);
    }
    
    [TestMethod]
    public void Sha256Base64Test()
    {
        var text = "hello word";
        var actual = "8NpVnqWc7Wi01ldJa+6XU8BEfXBwKvGjUcdXcibZdyM=";
        var expected = text.ToSha256Base64();
        Assert.AreEqual(expected,actual);
    }
    
    [TestMethod]
    public void Sha384Base64Test()
    {
        var text = "hello word";
        var actual = "pY0n7gYhHtx6ZPGZt9pV/Q/jHZiyyUn4P7uVvH/DEU15V8paPsS0iaAmNWE1aBeC";
        var expected = text.ToSha384Base64();
        Assert.AreEqual(expected,actual);
    }
    
    [TestMethod]
    public void Sha512Base64Test()
    {
        var text = "hello word";
        var actual = "ht/svUiNhEgb38XVT1JzT9QCmO9o2gFAlaUoiaNaWWo+ZKnqZPAFyqpLTSsR2aafEiFKMbebvdwIcvp1YSAL0g==";
        var expected = text.ToSha512Base64();
        Assert.AreEqual(expected,actual);
    }
}