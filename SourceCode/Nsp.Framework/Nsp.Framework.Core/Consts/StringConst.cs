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
}