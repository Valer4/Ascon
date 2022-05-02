using CommonHelpers.CryptoProvider.Interfaces;
using CryptoPro.Sharpei;
using System;
using System.Collections.Generic;
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace CommonHelpers.CryptoProvider
{
    public class CryptoProvider : ICryptoProvider
    {
        private CfgCryptoProvider _cfgCryptoProvider;
        public CryptoProvider(CfgCryptoProvider cfgCryptoProvider) => _cfgCryptoProvider = cfgCryptoProvider;

        public X509Certificate2 GetCertificate()
        {
            ICollection<string> errors = null;

            // true - MachineKeyStore; false - UserKeyStore.
            Gost3410_2012_256CryptoServiceProvider provider = BuildCryptoProviderByKeyStore(false, ref errors) ?? BuildCryptoProviderByKeyStore(true, ref errors);

            if (null == provider)
                _cfgCryptoProvider._NoticeError.Throw(string.Format(_cfgCryptoProvider.CatchMsg_CreateCryptoProvider, string.Join(" ", errors).Trim()));

            CheckCertificatePassword(provider);
            return GetX509Certificate2(provider);
        }

        private Gost3410_2012_256CryptoServiceProvider BuildCryptoProviderByKeyStore(bool isMachineKeyStore, ref ICollection<string> errors)
        {
            CspParameters cspParameters = BuildCertificateCspParameters(isMachineKeyStore); // Открываем контейнер.
            try
            {
                var provider = new Gost3410_2012_256CryptoServiceProvider(cspParameters);

                return provider;
            }
            catch (Exception ex)
            {
                if (null == errors)
                    errors = new List<string>();
                errors.Add(ex.Message);

                return null; // Крипто-провайдер не создан.
            }
        }

        /// <summary>
        /// Конфигурация для Gost3410_2012_256CryptoServiceProvider.
        /// </summary>
        /// <param name="isMachineKeyStore"></param>
        /// <returns></returns>
        private CspParameters BuildCertificateCspParameters(bool isMachineKeyStore)
        {
            var cspParameters = new CspParameters();

            cspParameters.KeyContainerName = _cfgCryptoProvider.KeyContainerName;
            // cspParameters.KeyNumber = 1;
            cspParameters.ProviderName = _cfgCryptoProvider.ProviderName;
            cspParameters.ProviderType = _cfgCryptoProvider.ProviderType; // Номер Crypto-Pro GOST R 34.10-2012 Cryptographic Service Provider.

            cspParameters.Flags = isMachineKeyStore
                                      ? (CspProviderFlags.NoPrompt | CspProviderFlags.UseExistingKey | CspProviderFlags.UseMachineKeyStore)
                                      : (CspProviderFlags.NoPrompt | CspProviderFlags.UseExistingKey);
            cspParameters.KeyPassword = BuildCspParametersPassword();

            return cspParameters;
        }

        /// <summary>
        /// Присвоение пароля.
        /// </summary>
        /// <returns></returns>
        private SecureString BuildCspParametersPassword()
        {
            var secure = new SecureString();
            string password = _cfgCryptoProvider.KeyPassword;

            foreach (char charPass in password)
                secure.AppendChar(charPass);

            return secure;
        }

        /// <summary>
        /// Проверка пароля контейнера.
        /// </summary>
        /// <param name="provider"></param>
        private void CheckCertificatePassword(Gost3410_2012_256CryptoServiceProvider provider)
        {
            var dummyHash = new byte[32];
            try
            {
                provider.SignHash(dummyHash); // Если ошибка - пароль не верный.
            }
            catch (Exception ex)
            {
                _cfgCryptoProvider._NoticeError.Throw(string.Format(_cfgCryptoProvider.CatchMsg_KeyPassword, ex.Message));
            }
        }

        /// <summary>
        /// Получение сертификата из контейнера.
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        private X509Certificate2 GetX509Certificate2(Gost3410_2012_256CryptoServiceProvider provider)
        {
            X509Certificate2 certificate2 = provider.ContainerCertificate;
            if (null == certificate2)
                _cfgCryptoProvider._NoticeError.Throw(_cfgCryptoProvider.ErrorMsg_GetCertificateFromContainer);

            return certificate2;
        }
    }
}
