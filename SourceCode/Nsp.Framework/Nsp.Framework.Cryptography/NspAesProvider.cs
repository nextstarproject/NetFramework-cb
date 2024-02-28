using System.Text;
using Nsp.Framework.Core;

namespace Nsp.Framework.Cryptography;

public class NspAesProvider
{
    /// <summary>
    /// 16长度
    /// </summary>
    public byte[] AesIV { get; private set; }
    public string Password { get; private set; }

    public NspAesProvider() : this("nextstar")
    {
        
    }

    public NspAesProvider(string password)
    {
        AesIV = RandomStringUtil.CreateRandomKey(16);
        Password = password;
    }
    
    public NspAesProvider(string password, string base64IV)
    {
        AesIV = Convert.FromBase64String(base64IV);
        if (AesIV.Length != 16) throw new ArgumentException("IV bytes must length 16");
        Password = password;
    }

    public string ExportBase64IV()
    {
        return Convert.ToBase64String(AesIV);
    }
    
    public string Encrypt(string data)
    {
        var passwordBytes = GenerateKey(Password);
        var encryptedBytes = EncryptStringToBytes_Aes(data, passwordBytes, AesIV);
        return Convert.ToBase64String(encryptedBytes);
    }

    public string Decrypt(string data)
    {
        var passwordBytes = GenerateKey(Password);
        var encryptedBytes = Convert.FromBase64String(data);
        var roundtrip = DecryptStringFromBytes_Aes(encryptedBytes, passwordBytes, AesIV);
        return roundtrip;
    }

    static byte[] GenerateKey(string password)
    {
        var newPassword = password + StringConst.TempSecret32;
        var passwordBytes = Encoding.UTF8.GetBytes(newPassword);
        return passwordBytes[..16];
    }

    static byte[] EncryptStringToBytes_Aes(string plainText, byte[] Key, byte[] IV)
    {
        // Check arguments.
        if (plainText is not {Length: > 0})
            throw new ArgumentNullException("plainText");
        if (Key is not {Length: > 0})
            throw new ArgumentNullException("Key");
        if (IV is not {Length: > 0})
            throw new ArgumentNullException("IV");
        byte[] encrypted;

        // Create an Aes object
        // with the specified key and IV.
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Key;
            aesAlg.IV = IV;

            // Create an encryptor to perform the stream transform.
            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            // Create the streams used for encryption.
            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        //Write all data to the stream.
                        swEncrypt.Write(plainText);
                    }

                    encrypted = msEncrypt.ToArray();
                }
            }
        }

        // Return the encrypted bytes from the memory stream.
        return encrypted;
    }

    static string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] Key, byte[] IV)
    {
        // Check arguments.
        if (cipherText == null || cipherText.Length <= 0)
            throw new ArgumentNullException("cipherText");
        if (Key == null || Key.Length <= 0)
            throw new ArgumentNullException("Key");
        if (IV == null || IV.Length <= 0)
            throw new ArgumentNullException("IV");

        // Declare the string used to hold
        // the decrypted text.
        string plaintext = null;

        // Create an Aes object
        // with the specified key and IV.
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Key;
            aesAlg.IV = IV;

            // Create a decryptor to perform the stream transform.
            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            // Create the streams used for decryption.
            using (MemoryStream msDecrypt = new MemoryStream(cipherText))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {
                        // Read the decrypted bytes from the decrypting stream
                        // and place them in a string.
                        plaintext = srDecrypt.ReadToEnd();
                    }
                }
            }
        }

        return plaintext;
    }
}