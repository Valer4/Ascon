using System.Security.Cryptography.X509Certificates;

namespace CommonHelpers.CryptoProvider.Interfaces
{
    public interface ICryptoProvider
    {
        X509Certificate2 GetCertificate();
    }
}
