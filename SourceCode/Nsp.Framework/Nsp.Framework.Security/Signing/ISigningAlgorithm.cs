namespace Nsp.Framework.Security.Signing;

public interface ISigningAlgorithm
{
    string Sign(string data, X509Certificate2 certificate);
}