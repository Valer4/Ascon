using BusinessLogicLayer;
using BusinessLogicLayer.DiContainer;
using System.Collections.Generic;
using WcfService.Services.Print;
using WcfService.Services.Repositories.Classes.ConcreteDefinitions;
using WcfService.Services.Repositories.Interfaces.ConcreteDefinitions;

namespace WcfService
{
    public class WcfServiceConfigurator
    {
        public static IDiContainer DiContainer;
        public static ConnectInfoClientService ConnectInfo;

        public WcfServiceConfigurator(ConnectInfoClientService connectInfo)
        {
            DiContainer = new DiContainer();
            ConnectInfo = connectInfo;

            CreateHosts(GetRelatedTypes());
        }

        private IEnumerable<RelatedTypes> GetRelatedTypes()
        {
            return
                new List<RelatedTypes>()
                {
                    new RelatedTypes(typeof(IDetailRelationRepositoryService), typeof(DetailRelationRepositoryService)),
                    new RelatedTypes(typeof(IPrintService), typeof(PrintService))
                };
        }

        private void CreateHosts(IEnumerable<RelatedTypes> relatedTypesList)
        {
            HostsManager hostsManager = new HostsManager();
            foreach (RelatedTypes relatedTypes in relatedTypesList)
                hostsManager.CreateHost(relatedTypes.Interface, relatedTypes.Class, relatedTypes.Interface.Name);
        }
    }
}
