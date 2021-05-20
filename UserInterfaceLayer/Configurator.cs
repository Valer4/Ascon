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
        internal static Container _Container;
        internal static UserInfo _UserInfo;
        internal static ConnectInfoClientService _ConnectInfo;
        internal static Dictionary<Type, string> _HostNames;

        internal Configurator(ConnectInfoClientService connectInfo)
        {
            _Container = new Container();
            _ConnectInfo = connectInfo;
            _UserInfo = new UserInfo("LoginX", "PasswordX");
            _HostNames = new Dictionary<Type, string>();

            RegisterTypes();
            FillHostNamesDictionary(GetContractsServicesInterfacesTypes());
        }

        private void RegisterTypes()
        {
            _Container.RegisterType<IDetailRelationRepositoryClient, DetailRelationRepositoryClient>();
            _Container.RegisterType<IPrintClient, PrintClient>();
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
                _HostNames.Add(contractServiceInterfaceType, contractServiceInterfaceType.Name);
        }
    }
}
