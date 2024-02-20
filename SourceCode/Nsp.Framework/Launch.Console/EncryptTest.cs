using Nsp.Framework.Encrypt;

namespace Launch.EncryptTest;

public class EncryptTest
{
    public static void Main()
    {
        using (var ecdsa = new NspECDsaProvider(SecurityAlgorithms.SHA512))
        {
            var pa = ecdsa.ExportParameters();
            var publicKey = ecdsa.GetBase64PublicKey();
            var privateKey = ecdsa.GetBase64PrivateKey();
            Console.WriteLine(publicKey);
            Console.WriteLine(privateKey);
            var x509Cer1 = ecdsa.ExportX509Certificate2();
            var aaa = ecdsa.SignData("Hello Word");
            Console.WriteLine(aaa);
            var bbb = ecdsa.VerifyData("Hello Word", aaa);
            Console.WriteLine(bbb);

            using (var ecdsa2 = new NspECDsaProvider(privateKey, publicKey, SecurityAlgorithms.SHA512))
            {
                var publicKey2 = ecdsa2.GetBase64PublicKey();
                var privateKey2 = ecdsa2.GetBase64PrivateKey();
                Console.WriteLine(publicKey2);
                Console.WriteLine(privateKey2);
                var aaa2 = ecdsa2.SignData("Hello Word");
                Console.WriteLine(aaa2);
                var bbb2 = ecdsa2.VerifyData("Hello Word", aaa2);
                Console.WriteLine(bbb2);
            }
        }
    }
}