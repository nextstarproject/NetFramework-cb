namespace Nsp.Framework.Core;

public static class StringConst
{
    /// <summary>
    /// 省略号 <code>...</code>
    /// </summary>
    public const string Ellipsis = "...";

    /// <summary>
    /// 下划线分割 <code>_</code>
    /// </summary>
    public const string Underline = "_";

    /// <summary>
    /// 连字符串 <code>-</code>
    /// </summary>
    public const string Hyphens = "-";

    /// <summary>
    /// 点符号 <code>.</code>
    /// </summary>
    public const string Dot = ".";

    /// <summary>
    /// 斜杠 <code>/</code>
    /// </summary>
    public const string Slash = "/";

    /// <summary>
    /// 空格 <code> </code>
    /// </summary>
    public const string Space = " ";

    /// <summary>
    /// 引用 <code>"</code>
    /// </summary>
    public const string Quote = "\"";

    /// <summary>
    /// 加密字符
    /// </summary>
    public const string EncryptionChar = "*";
    
    /// <summary>
    /// 加密字符
    /// </summary>
    public const string Encryption8Char = "********";
    
    /// <summary>
    /// 加密字符
    /// </summary>
    public const string Encryption16Char = "****************";

    /// <summary>
    /// 加密字符重复
    /// </summary>
    /// <param name="length">重复个数</param>
    /// <returns></returns>
    public static string EncryptionRepeat(int length) => string.Concat(Enumerable.Repeat(EncryptionChar, length));

    /// <summary>
    /// 临时密钥,测试密钥
    /// </summary>
    public const string TempSecret16 = "Z7Xt2NY42IDweRVY";
    
    /// <summary>
    /// 临时密钥,测试密钥
    /// </summary>
    public const string TempSecret24 = "vysFfqHoOWCBvocIpTdjC9xy";
    
    /// <summary>
    /// 临时密钥,测试密钥
    /// </summary>
    public const string TempSecret32 = "g14jWiNdiBG3A5DhaEKuMJznL7IbnTqz";
    
    /// <summary>
    /// 临时密钥,测试密钥
    /// </summary>
    public const string TempSecret64 = "AIfHbxiL6l3kqs7YEy740ZPO2UZJYbLDjOJfLaIagNp9PomtNZD77ar47OFB8MFu";
    
    /// <summary>
    /// 临时密钥,测试密钥
    /// </summary>
    public const string TempSecret128 = "qgkhc1MnXEcrFjXE3IndQPLCjFXZ64p4CiUhC4i8DOm90yqzdtrhMysdh1ZO0UuTn8APN8FungPN3xPtkTZkyJqKBA7BkfVeIjyz5mP62QlRJzEAJ3J0EgCu1tzO1vUv";

    /// <summary>
    /// 临时密钥,测试密钥
    /// </summary>
    public const string TempSecret192 =
        "XrgYlBNr4RrqXbPBUAyyZ95fdWcDufAW0HDJQtJ9gZlpmJm8jg9qYJNtMkNkvMNIsMYyxqyRwNXxyZvulgmEVd6acGgp90QJuRsiwizR6xe4VCGVUCGMWD7UgXN0HbDpZSkqGaBXZarBfPWaKzCxb9Nepu3j1gspNBEvXHV4x7oZ97qen6UKq4VXSCiAYrKB";

    /// <summary>
    /// 临时密钥,测试密钥
    /// </summary>
    public const string TempSecret256 =
        "Wz5obMfFrCsql5rbLvMTbWQsp65pN3ZiI0JR50nCj07Pomye9KpU2i9qADKemRJ9lGVBkwKJc1Kx8HchJ4blN2qMqrvXcnFejiJijncuaBChHMj5htY87XFXmwo8uuxHSDTazl4k3SIgsl2OR8dteZ4S3NiC2qpLxc1Id5a5QMgLWti9lUMYiFBvLORz8AXT2SoP5yQgB126ZAc3PAVDyWQMgHmAPnDAGEq3G3nvN5MVS86h6odjJFDzsXOsePiq";

    /// <summary>
    /// 临时密钥,测试密钥
    /// </summary>
    public const string TempSecret384 = "UnHh3L2lzjNrwkF1owmb6qsvYhU3Z9GzVFAF6R6phzyHl05StdLUZWoZ3ldaRBSUzpbIJe0wGHLhBe2w2IVbb4kkuponDxtFGdjeC6pxjZT9aZ1RPVtIcucLj2RGEarxcynlk5cCjalpiLMtODmeDTAcZ1yo3i2tqrfsUmEN9GI8SaDNupd43ssPNMzYUpmAt6RDrnkp8Rxr1lEBzRDdyA7l4uMEUhXwbkFA9WpkVgmeQOAS1VreaNAGZKxhNgDxu68Bzj4Bj1Ae1e6Ty3UiV6kouAfVDUdk9bvbUjE9T0iCqRzLqLtNmWFmGnB11FbiAkuOItxRC4StQuYwLQD1a8T1oa4XjjVNUHVtdSAwryMqnCT64MCzMkZR1wo8hti4";
    
    /// <summary>
    /// 临时密钥,测试密钥
    /// </summary>
    public const string TempSecret512 = "v0rrcc9pdKT8GzDFlSAOAuYJqQstZ29LFppN1xKdZAKjH1IXr7XW7FRg27BTSS9T1QtGKfij24fP0Xw2Xfe6l8fZ8bvVgFNtuHPcls2s4RxXCodHjwaortDdXwkGn3HSUAdshp3tM9ZaeGAkteGkhS6o68XNTEQTlV3wg7hgb6GFbBD8s2JyeRqE8gkYARvwr12kkkkBopTVnTjT9WFWvf6guMtqaTEc2i4a6QNitRAWBpui4Nqx0g2hQNbJB6luTa4Xtw7Od0bSwkIO9NC51khC1F0RyGKQTDL1dAuuGpFSGcWmmzSQtTNr89h7El6hyquz15KgNLnNWb9I9TRnrZcf8QsLx3J7VRcz0dsAIaQDJd1js4uwaBGTN4Gc26qAGNuvEcyFEXqfMcxuDGn77Aq3XmoN0KygrnwVUjzXVE6GuZ0dMEyqFUcplrgLK37XxaHJw5Xm2H4yRhJMFYdoCnAW8583u6irrj0xK5MXbfdNozJr94586Np81TbP1uJV";
}