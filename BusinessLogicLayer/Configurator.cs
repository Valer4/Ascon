using BusinessLogicLayer.Services.Print;
using BusinessLogicLayer.Services.Repositories.Classes.ConcreteDefinitions;
using BusinessLogicLayer.Services.Repositories.Interfaces.ConcreteDefinitions;
using System.Collections.Generic;

namespace BusinessLogicLayer
{
    public class Configurator
    {
        public static Container Container;
        public static ConnectInfoClientService ConnectInfo;

        public Configurator(ConnectInfoClientService connectInfo)
        {
            Container = new Container();
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
            foreach(RelatedTypes relatedTypes in relatedTypesList)
                hostsManager.CreateHost(relatedTypes.Interface, relatedTypes.Class, relatedTypes.Interface.Name);
        }
    }
}
