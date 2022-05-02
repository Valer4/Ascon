using BusinessLogicLayer;
using BusinessLogicLayer.DiContainer;
using CommonHelpers.Any;
using CommonHelpers.Any.Interfaces;
using CommonHelpers.CryptoProvider;
using CommonHelpers.CryptoProvider.Interfaces;
using System;
using System.Windows.Forms;
using UserInterfaceLayer.Clients.Authorization;
using UserInterfaceLayer.Clients.Print;
using UserInterfaceLayer.Clients.Repositories.Classes.ConcreteDefinitions;
using UserInterfaceLayer.Clients.Repositories.Interfaces.ConcreteDefinitions;
using UserInterfaceLayer.Forms.Views;

namespace UserInterfaceLayer
{
    internal static class Program
    {
        internal static ConnectInfoClientService ConnectInfo;
        internal static string AccessToken;

        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                new Configurator(new ConnectInfoClientService("localhost", 10000));

                ConnectInfo = new ConnectInfoClientService("localhost", 44331);

                {
                    Configurator.DiContainer.RegisterType<INoticeError, NoticeError>(DiLifetimeType.Transient);
                    Configurator.DiContainer.RegisterType<IStreamHelper, StreamHelper>(DiLifetimeType.Transient);

                    Configurator.DiContainer.RegisterType<CfgCryptoProvider>(DiLifetimeType.Transient, constructorParams: new object[] {
                        Configurator.DiContainer.Resolve<INoticeError>(),

                        /*keyContainerName*/ string.Empty,
                        /*containerPassword*/ string.Empty,

                        /*providerName*/ string.Empty,
                        /*providerType*/ 0,

                        /*catchMsg_CreateCryptoProvider*/ string.Empty,
                        /*catchMsg_KeyPassword*/ string.Empty,
                        /*errorMsg_GetCertificateFromContainer*/ string.Empty});

                    Configurator.DiContainer.RegisterType<ICryptoProvider, CryptoProvider>(DiLifetimeType.Transient, constructorParams:
                        Configurator.DiContainer.Resolve<CfgCryptoProvider>());

                    Configurator.DiContainer.RegisterType<IRestApi, RestApi>(DiLifetimeType.Transient, constructorParams: new object[]{
                        Configurator.DiContainer.Resolve<IStreamHelper>(),
                        Configurator.DiContainer.Resolve<INoticeError>(),
                        Configurator.DiContainer.Resolve<ICryptoProvider>()});

                    Configurator.DiContainer.RegisterType<IDetailRelationRepositoryClient, DetailRelationRepositoryClient>(DiLifetimeType.Transient, constructorParams: new object[]{
                        Configurator.DiContainer.Resolve<IRestApi>(),
                        Configurator.DiContainer.Resolve<IStreamHelper>(),
                        @"api/detail"});

                    Configurator.DiContainer.RegisterType<IPrintClient, PrintClient>(DiLifetimeType.Transient, constructorParams: new object[]{
                        Configurator.DiContainer.Resolve<IRestApi>(),
                        Configurator.DiContainer.Resolve<IStreamHelper>(),
                        @"api/print"});

                    Configurator.DiContainer.RegisterType<IAuthorizationClient, AuthorizationClient>(DiLifetimeType.Transient, constructorParams: new object[]{
                        Configurator.DiContainer.Resolve<IRestApi>(),
                        Configurator.DiContainer.Resolve<IStreamHelper>(),
                        @"api/authorization"});

                    Configurator.DiContainer.RegisterType<DetailsEditor>(DiLifetimeType.Transient, constructorParams: new object[]{
                        Configurator.DiContainer.Resolve<IDetailRelationRepositoryClient>(),
                        Configurator.DiContainer.Resolve<IPrintClient>(),
                        Configurator.DiContainer.Resolve<IAuthorizationClient>()});
                }

                Application.Run(Configurator.DiContainer.Resolve<DetailsEditor>());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
