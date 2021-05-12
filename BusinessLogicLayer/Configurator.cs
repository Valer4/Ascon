using BusinessLogicLayer.Managers.EntityManagers.Classes.ConcreteDefinitions;
using BusinessLogicLayer.Managers.Print;
using BusinessLogicLayer.Managers.Repositories.Interfaces.ConcreteDefinitions;
using BusinessLogicLayer.Services.Print;
using BusinessLogicLayer.Services.Repositories.Classes.ConcreteDefinitions;
using BusinessLogicLayer.Services.Repositories.Interfaces.ConcreteDefinitions;
using System.Collections.Generic;

namespace BusinessLogicLayer
{
    public class Configurator
    {
        public static Container _Container;
        public static ConnectInfoClientService _ConnectInfo;

        public Configurator(ConnectInfoClientService connectInfo)
        {
            _Container = new Container();
            _ConnectInfo = connectInfo;

            RegisterTypes();
            CreateHosts(GetRelatedTypes());
        }

        private void RegisterTypes()
        {
            _Container.RegisterType<IDetailRelationRepositoryService, DetailRelationRepositoryService>(
                constructorParams: new DetailRelationRepositoryManager());

            _Container.RegisterType<IPrintService, PrintService>(
                constructorParams: new PrintManager());

            _Container.RegisterType<IDetailRelationRepositoryManager, DetailRelationRepositoryManager>();
            _Container.RegisterType<IPrintManager, PrintManager>();
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
                hostsManager.CreateHost(relatedTypes._Interface, relatedTypes._Class, relatedTypes._Interface.Name);
        }
    }
}
