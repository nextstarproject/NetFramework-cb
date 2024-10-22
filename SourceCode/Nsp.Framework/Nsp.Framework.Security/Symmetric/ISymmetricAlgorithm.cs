namespace Nsp.Framework.Security.Symmetric;

public interface ISymmetricAlgorithm
{
    /// <summary>
    /// 不推荐使用，容易出现字节转字符问题，建议使用 <see cref="EncryptToBase64"/>
    /// </summary>
    /// <param name="plainText"></param>
    /// <returns></returns>
    [Obsolete("不推荐使用，容易出现字节转字符问题，建议使用 EncryptToBase64")]
    string Encrypt(string plainText);
    /// <summary>
    /// 不推荐使用，容易出现字节转字符问题，建议使用 <see cref="DecryptFromBase64"/>
    /// </summary>
    /// <param name="cipherText"></param>
    /// <returns></returns>
    [Obsolete("不推荐使用，容易出现字节转字符问题，建议使用 DecryptFromBase64")]
    string Decrypt(string cipherText);
    string EncryptToHex(string plainText);
    string DecryptFromHex(string cipherHexText);
    string EncryptToBase64(string plainText);
    string DecryptFromBase64(string cipherBase64);
    byte[] Encrypt(byte[] plainBytes);
    byte[] Decrypt(byte[] cipherBytes);
}