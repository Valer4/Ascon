using BusinessLogicLayer;
using BusinessLogicLayer.Services.Print;
using BusinessLogicLayer.Services.Repositories.Interfaces.ConcreteDefinitions;
using System;
using System.Collections.Generic;
using UserInterfaceLayer.Clients.Print;
using UserInterfaceLayer.Clients.Repositories.Classes.ConcreteDefinitions;
using UserInterfaceLayer.Clients.Repositories.Interfaces.ConcreteDefinitions;

namespace UserInterfaceLayer
{
    internal class Configurator
    {
        internal static Container Container;
        internal static UserInfo UserInfo;
        internal static ConnectInfoClientService ConnectInfo;
        internal static Dictionary<Type, string> HostNames;

        internal Configurator(ConnectInfoClientService connectInfo)
        {
            Container = new Container();
            ConnectInfo = connectInfo;
            UserInfo = new UserInfo("LoginX", "PasswordX");
            HostNames = new Dictionary<Type, string>();

            RegisterTypes();
            FillHostNamesDictionary(GetContractsServicesInterfacesTypes());
        }

        private void RegisterTypes()
        {
            Container.RegisterType<IDetailRelationRepositoryClient, DetailRelationRepositoryClient>();
            Container.RegisterType<IPrintClient, PrintClient>();
        }

        private IEnumerable<Type> GetContractsServicesInterfacesTypes()
        {
            return
                new List<Type>()
                {
                    typeof(IDetailRelationRepositoryService),
                    typeof(IPrintService)
                };
        }

        internal void FillHostNamesDictionary(IEnumerable<Type> contractsServicesInterfacesTypes)
        {
            foreach(Type contractServiceInterfaceType in contractsServicesInterfacesTypes)
                HostNames.Add(contractServiceInterfaceType, contractServiceInterfaceType.Name);
        }
    }
}
