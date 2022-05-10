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
using WcfClient;
using WcfService;
using IRestDetail = RestClient.Repositories.Interfaces.ConcreteDefinitions;
using IWcfDetail = WcfClient.Repositories.Interfaces.ConcreteDefinitions;
using RestAuthorization = RestClient.Authorization;
using RestDetail = RestClient.Repositories.Classes.ConcreteDefinitions;
using RestPrint = RestClient.Print;
using WcfAuthorization = WcfClient.Authorization;
using WcfDetail = WcfClient.Repositories.Classes.ConcreteDefinitions;
using WcfPrint = WcfClient.Print;

namespace UserInterfaceLayer
{
	internal static class Program
    {
        internal static ChannelType ChannelType = ChannelType.Rest;
        internal static IDiContainer DiContainer = new DiContainer();
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

                DiContainer.RegisterType<INoticeError, NoticeError>(DiLifetimeType.Transient);
                DiContainer.RegisterType<IStreamHelper, StreamHelper>(DiLifetimeType.Transient);

                DiContainer.RegisterType<CfgCryptoProvider>(DiLifetimeType.Transient, constructorParams: new object[] {
                    DiContainer.Resolve<INoticeError>(),

                    /*keyContainerName*/ string.Empty,
                    /*containerPassword*/ string.Empty,

                    /*providerName*/ string.Empty,
                    /*providerType*/ 0,

                    /*catchMsg_CreateCryptoProvider*/ string.Empty,
                    /*catchMsg_KeyPassword*/ string.Empty,
                    /*errorMsg_GetCertificateFromContainer*/ string.Empty});

                DiContainer.RegisterType<ICryptoProvider, CryptoProvider>(DiLifetimeType.Transient, constructorParams:
                    DiContainer.Resolve<CfgCryptoProvider>());

                DiContainer.RegisterType<IRestApi, RestApi>(DiLifetimeType.Transient, constructorParams: new object[]{
                    DiContainer.Resolve<IStreamHelper>(),
                    DiContainer.Resolve<INoticeError>(),
                    DiContainer.Resolve<ICryptoProvider>()});

                DiContainer.RegisterType<WcfClientConfigurator>(DiLifetimeType.Transient, constructorParams: new object[] {
                    new ConnectInfoClientService("localhost", 10000)});

                DiContainer.RegisterType<RestClientConfigurator>(DiLifetimeType.Transient, constructorParams: new object[] {
                    DiContainer.Resolve<IRestApi>(),
                    new ConnectInfoClientService("localhost", 44331)});

                DiContainer.RegisterType<ChannelsManager>(DiLifetimeType.Transient, constructorParams: new object[] {
                    DiContainer.Resolve<WcfClientConfigurator>()});

                DiContainer.RegisterType<IWcfDetail.IDetailRelationRepositoryClient, WcfDetail.DetailRelationRepositoryClient>(
                    DiLifetimeType.Transient,
                    constructorParams: new object[] {
                        DiContainer.Resolve<ChannelsManager>()});
                DiContainer.RegisterType<IRestDetail.IDetailRelationRepositoryClient, RestDetail.DetailRelationRepositoryClient>(
                    DiLifetimeType.Transient,
                    constructorParams: new object[] {
                        DiContainer.Resolve<RestClientConfigurator>(),
                        @"api/detail"});
                DiContainer.RegisterType<IDetailRelationRepositoryClient, DetailRelationRepositoryClient>(
                    DiLifetimeType.Transient,
                    constructorParams: new object[]{
                        DiContainer.Resolve<IWcfDetail.IDetailRelationRepositoryClient>(),
                        DiContainer.Resolve<IRestDetail.IDetailRelationRepositoryClient>()});

                DiContainer.RegisterType<WcfPrint.IPrintClient, WcfPrint.PrintClient>(
                    DiLifetimeType.Transient,
                    constructorParams: new object[] {
                        DiContainer.Resolve<ChannelsManager>()});
                DiContainer.RegisterType<RestPrint.IPrintClient, RestPrint.PrintClient>(
                    DiLifetimeType.Transient,
                    constructorParams: new object[] {
                        DiContainer.Resolve<RestClientConfigurator>(),
                        @"api/print"});
                DiContainer.RegisterType<IPrintClient, PrintClient>(
                    DiLifetimeType.Transient,
                    constructorParams: new object[]{
                        DiContainer.Resolve<WcfPrint.IPrintClient>(),
                        DiContainer.Resolve<RestPrint.IPrintClient>()});

                DiContainer.RegisterType<WcfAuthorization.IAuthorizationClient, WcfAuthorization.AuthorizationClient>(
                    DiLifetimeType.Transient,
                    constructorParams: new object[] {
                        DiContainer.Resolve<ChannelsManager>()});
                DiContainer.RegisterType<RestAuthorization.IAuthorizationClient, RestAuthorization.AuthorizationClient>(
                    DiLifetimeType.Transient,
                    constructorParams: new object[] {
                        DiContainer.Resolve<RestClientConfigurator>(),
                        @"api/authorization"});
                DiContainer.RegisterType<IAuthorizationClient, AuthorizationClient>(
                    DiLifetimeType.Transient,
                    constructorParams: new object[]{
                        DiContainer.Resolve<WcfAuthorization.IAuthorizationClient>(),
                        DiContainer.Resolve<RestAuthorization.IAuthorizationClient>()});

                DiContainer.RegisterType<DetailsEditor>(DiLifetimeType.Transient, constructorParams: new object[]{
                    DiContainer.Resolve<IDetailRelationRepositoryClient>(),
                    DiContainer.Resolve<IPrintClient>(),
                    DiContainer.Resolve<IAuthorizationClient>()});

                Application.Run(DiContainer.Resolve<DetailsEditor>());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
