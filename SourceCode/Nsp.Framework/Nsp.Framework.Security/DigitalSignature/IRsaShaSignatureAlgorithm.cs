using Nsp.Framework.Security.Asymmetric;

namespace Nsp.Framework.Security.DigitalSignature;

public interface IRsaShaSignatureAlgorithm : IRsaProvider, ISignatureAlgorithm
{
    public HashAlgorithmName HashAlgorithmNameSetting { get; }
    public RSASignaturePadding RSASignaturePaddingSetting { get; }
}