using BusinessLogicLayer;
using BusinessLogicLayer.DiContainer;
using System;
using System.Collections.Generic;
using WcfService.Services.Print;
using WcfService.Services.Repositories.Interfaces.ConcreteDefinitions;

namespace UserInterfaceLayer
{
    internal class Configurator
    {
        internal static IDiContainer DiContainer;
        internal static UserInfo UserInfo;
        internal static ConnectInfoClientService ConnectInfo;
        internal static Dictionary<Type, string> HostNames;

        internal Configurator(ConnectInfoClientService connectInfo)
        {
            DiContainer = new DiContainer();
            ConnectInfo = connectInfo;

            UserInfo = new UserInfo(
                "UserNameX",
                "PasswordX");
            HostNames = new Dictionary<Type, string>();

            FillHostNamesDictionary(GetContractsServicesInterfacesTypes());
        }

        private IEnumerable<Type> GetContractsServicesInterfacesTypes() =>
            new List<Type>()
            {
                typeof(IDetailRelationRepositoryService),
                typeof(IPrintService)
            };

        internal void FillHostNamesDictionary(IEnumerable<Type> contractsServicesInterfacesTypes)
        {
            foreach (Type contractServiceInterfaceType in contractsServicesInterfacesTypes)
                HostNames.Add(contractServiceInterfaceType, contractServiceInterfaceType.Name);
        }
    }
}
