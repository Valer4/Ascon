using BusinessLogicLayer;
using System;
using System.Collections.Generic;
using WcfService.Services.Print;
using WcfService.Services.Repositories.Interfaces.ConcreteDefinitions;

namespace WcfService
{
	public class WcfClientConfigurator
    {
        public Dictionary<Type, string> HostNames { get; }
        public ConnectInfoClientService ConnectInfo { get; }

        public UserInfo UserInfo { get; }

        public WcfClientConfigurator(ConnectInfoClientService connectInfo)
        {
            HostNames = new Dictionary<Type, string>();
            FillHostNamesDictionary(GetContractsServicesInterfacesTypes());

            ConnectInfo = connectInfo;

            UserInfo = new UserInfo(
                "UserNameX",
                "PasswordX"
            );
        }

        private IEnumerable<Type> GetContractsServicesInterfacesTypes() =>
            new List<Type>()
            {
                typeof(IDetailRelationRepositoryService),
                typeof(IPrintService)
            };

        private void FillHostNamesDictionary(IEnumerable<Type> contractsServicesInterfacesTypes)
        {
            foreach (Type contractServiceInterfaceType in contractsServicesInterfacesTypes)
                HostNames.Add(contractServiceInterfaceType, contractServiceInterfaceType.Name);
        }
    }
}
