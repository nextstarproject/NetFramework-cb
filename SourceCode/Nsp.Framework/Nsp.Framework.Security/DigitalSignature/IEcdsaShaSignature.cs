using Nsp.Framework.Security.Asymmetric;

namespace Nsp.Framework.Security.DigitalSignature;

public interface IEcdsaShaSignature : IEcDsaProvider, ISignatureAlgorithm
{
    public HashAlgorithmName HashAlgorithmNameSetting { get; }
}