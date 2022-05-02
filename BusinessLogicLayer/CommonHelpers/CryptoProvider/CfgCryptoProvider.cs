using CommonHelpers.Any.Interfaces;

namespace CommonHelpers.CryptoProvider
{
    public class CfgCryptoProvider
    {
        public INoticeError _NoticeError;

        public string KeyContainerName;
        public string KeyPassword;

        public string ProviderName;
        public int ProviderType;

        public string CatchMsg_CreateCryptoProvider;
        public string CatchMsg_KeyPassword;
        public string ErrorMsg_GetCertificateFromContainer;

        public CfgCryptoProvider(
            INoticeError noticeError,

            string keyContainerName,
            string containerPassword,

            string providerName,
            int providerType,

            string catchMsg_CreateCryptoProvider,
            string catchMsg_KeyPassword,
            string errorMsg_GetCertificateFromContainer)
        {
            _NoticeError = noticeError;

            KeyContainerName = keyContainerName;
            ProviderName = providerName;
            ProviderType = providerType;
            KeyPassword = containerPassword;

            CatchMsg_CreateCryptoProvider = catchMsg_CreateCryptoProvider;
            CatchMsg_KeyPassword = catchMsg_KeyPassword;
            ErrorMsg_GetCertificateFromContainer = errorMsg_GetCertificateFromContainer;
        }
    }
}
