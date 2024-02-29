using Nsp.Framework.Cryptography;

namespace Launch.EncryptTest;

public class EncryptTest
{
    public static void RsaPssTest()
    {
        using (var rsa = new NspRsaPssProvider())
        {
            var publicKey = rsa.GetBase64PublicKey();
            var privateKey = rsa.GetBase64PrivateKey();
            var xmlKey = rsa.ExportXmlPublicAndPrivate();
            Console.WriteLine(publicKey);
            Console.WriteLine(privateKey);
            var x509Cer1 = rsa.ExportX509Certificate2();
            var aaa = rsa.SignData("Hello Word");
            Console.WriteLine(aaa);
            var bbb = rsa.VerifyData("Hello Word", aaa);
            Console.WriteLine(bbb);
            
            using (var rsa2 = new NspRsaPssProvider(privateKey, publicKey))
            {
                var publicKey2 = rsa2.GetBase64PublicKey();
                var privateKey2 = rsa2.GetBase64PrivateKey();
                Console.WriteLine(publicKey2);
                Console.WriteLine(privateKey2);
                var aaa2 = rsa2.SignData("Hello Word");
                Console.WriteLine(aaa2);
                var bbb2 = rsa2.VerifyData("Hello Word", aaa2);
                var bbb3 = rsa2.VerifyData("Hello Word", aaa);
                Console.WriteLine(bbb2);
                Console.WriteLine(bbb3);
            }
            using (var rsa3 = new NspRsaPssProvider(xmlKey))
            {
                var publicKey2 = rsa3.GetBase64PublicKey();
                var privateKey2 = rsa3.GetBase64PrivateKey();
                Console.WriteLine(publicKey2);
                Console.WriteLine(privateKey2);
                var x509Cer2 = rsa.ExportX509Certificate2();
                var aaa2 = rsa3.SignData("Hello Word");
                Console.WriteLine(aaa2);
                var bbb2 = rsa3.VerifyData("Hello Word", aaa2);
                var bbb3 = rsa3.VerifyData("Hello Word", aaa);
                Console.WriteLine(bbb2);
                Console.WriteLine(bbb3);
            }
        }
    }
    public static void RsaTest()
    {
        using (var rsa = new NspRsaProvider())
        {
            var publicKey = rsa.GetBase64PublicKey();
            var privateKey = rsa.GetBase64PrivateKey();
            var xmlKey = rsa.ExportXmlPublicAndPrivate();
            Console.WriteLine(publicKey);
            Console.WriteLine(privateKey);
            var x509Cer1 = rsa.ExportX509Certificate2();
            var aaa = rsa.SignData("Hello Word");
            Console.WriteLine(aaa);
            var bbb = rsa.VerifyData("Hello Word", aaa);
            Console.WriteLine(bbb);

            var ccc = rsa.Encrypt("111");
            Console.WriteLine(ccc);
            var ddd = rsa.Decrypt(ccc);
            Console.WriteLine(ddd);
            
            using (var rsa2 = new NspRsaProvider(privateKey, publicKey))
            {
                var publicKey2 = rsa2.GetBase64PublicKey();
                var privateKey2 = rsa2.GetBase64PrivateKey();
                Console.WriteLine(publicKey2);
                Console.WriteLine(privateKey2);
                var aaa2 = rsa2.SignData("Hello Word");
                Console.WriteLine(aaa2);
                var bbb2 = rsa2.VerifyData("Hello Word", aaa2);
                var bbb3 = rsa2.VerifyData("Hello Word", aaa);
                Console.WriteLine(bbb2);
                Console.WriteLine(bbb3);
                
                var ccc1 = rsa.Encrypt("111");
                Console.WriteLine(ccc);
                var ddd1 = rsa.Decrypt(ccc);
                var ddd2 = rsa.Decrypt(ccc1);
                Console.WriteLine(ddd1);
                Console.WriteLine(ddd2);
            }
            using (var rsa3 = new NspRsaProvider(xmlKey))
            {
                var publicKey2 = rsa3.GetBase64PublicKey();
                var privateKey2 = rsa3.GetBase64PrivateKey();
                Console.WriteLine(publicKey2);
                Console.WriteLine(privateKey2);
                var x509Cer2 = rsa.ExportX509Certificate2();
                var aaa2 = rsa3.SignData("Hello Word");
                Console.WriteLine(aaa2);
                var bbb2 = rsa3.VerifyData("Hello Word", aaa2);
                var bbb3 = rsa3.VerifyData("Hello Word", aaa);
                Console.WriteLine(bbb2);
                Console.WriteLine(bbb3);
                
                var ccc1 = rsa.Encrypt("111");
                Console.WriteLine(ccc);
                var ddd1 = rsa.Decrypt(ccc);
                var ddd2 = rsa.Decrypt(ccc1);
                Console.WriteLine(ddd1);
                Console.WriteLine(ddd2);
            }
        }
    }

    public static void ECDsaTest()
    {
        using (var ecdsa = new NspECDsaProvider(NspSecurityAlgorithms.SHA512))
        {
            var publicKey = ecdsa.GetBase64PublicKey();
            var privateKey = ecdsa.GetBase64PrivateKey();
            Console.WriteLine(publicKey);
            Console.WriteLine(privateKey);
            var x509Cer1 = ecdsa.ExportX509Certificate2();
            var aaa = ecdsa.SignData("Hello Word");
            Console.WriteLine(aaa);
            var bbb = ecdsa.VerifyData("Hello Word", aaa);
            Console.WriteLine(bbb);

            using (var ecdsa2 = new NspECDsaProvider(privateKey, publicKey, NspSecurityAlgorithms.SHA512))
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