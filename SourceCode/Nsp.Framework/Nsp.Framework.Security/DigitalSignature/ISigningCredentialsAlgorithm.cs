namespace Nsp.Framework.Security.DigitalSignature;

public interface ISigningCredentialsAlgorithm
{
    SigningCredentials ExportSigningCredentials(string? keyId);
}