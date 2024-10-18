using System.Text;

namespace Nsp.Framework.Security;

public static class SecurityUtil
{
    /// <summary>
    /// Bytes 转 Hex 字符串
    /// </summary>
    /// <param name="bytes"></param>
    /// <returns></returns>
    public static string BytesToHexString(byte[] bytes)
    {
        return BitConverter.ToString(bytes).Replace("-", "").ToLowerInvariant();
    }
    
    /// <summary>
    /// Hex 转 Bytes 字符串
    /// </summary>
    /// <param name="hex"></param>
    /// <returns></returns>
    public static byte[] StringToByteArray(string hex)
    {
        var length = hex.Length;
        var bytes = new byte[length / 2];

        for (var i = 0; i < length; i += 2)
        {
            // 将每两个十六进制字符转换为一个字节
            bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
        }
        return bytes;
    }
    
    /// <summary>
    /// 获取指定长度的字节数组, 不足部分用 0 填充, 超出部分截断
    /// </summary>
    /// <param name="input"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    public static byte[] GetBytes(string input, int length)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(input);

        if (bytes.Length < length)
        {
            Array.Resize(ref bytes, length);
            for (int i = bytes.Length; i < length; i++)
            {
                bytes[i] = 0; // 用 0 字符填充
            }
        }
        else if (bytes.Length > length)
        {
            Array.Resize(ref bytes, length); // 截断多余部分
        }

        return bytes;
    }
}