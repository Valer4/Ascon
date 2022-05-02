using BusinessLogicLayer;
using BusinessLogicLayer.Data.DataAccessInterfaces.Repositories.ConcreteDefinitions;
using BusinessLogicLayer.DiContainer;
using BusinessLogicLayer.LogicMain.Managers.Print;
using BusinessLogicLayer.LogicMain.Managers.Repositories.Classes.ConcreteDefinitions;
using BusinessLogicLayer.LogicMain.Managers.Repositories.Interfaces.ConcreteDefinitions;
using BusinessLogicLayer.LogicMain.Presenters.Interfaces.Repositories.ConcreteDefinitions;
using BusinessLogicLayer.LogicMain.Presenters.Print;
using BusinessLogicLayer.LogicMain.Presenters.Repositories.Classes.ConcreteDefinitions;
using DataAccessLayer.DataAccessClasses.Repositories.ConcreteDefinitions;
using System;
using WcfService.Services.Print;
using WcfService.Services.Repositories.Classes.ConcreteDefinitions;
using WcfService.Services.Repositories.Interfaces.ConcreteDefinitions;

namespace WcfService
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                new Configurator(new ConnectInfoClientService("localhost", 10000));

                Configurator.DiContainer.RegisterType<ConnectInfoDataAccess>(DiLifetimeType.Transient, constructorParams: new object[] {
                    "localhost",
                    "Kuznetsov",
                    "msroot",
                    "msroot",
                    (short)1433 });
                Configurator.DiContainer.RegisterType<IDetailRelationRepository, DetailRelationRepository>(DiLifetimeType.Transient, constructorParams:
                    Configurator.DiContainer.Resolve<ConnectInfoDataAccess>().
                        ConnectionString);

                Configurator.DiContainer.RegisterType<IDetailRelationRepositoryManager, DetailRelationRepositoryManager>(typeof(IDetailRelationRepository),
                    delegate() { return Configurator.DiContainer.Resolve<IDetailRelationRepository>(); });
                Configurator.DiContainer.RegisterType<IDetailRelationRepositoryPresenter, DetailRelationRepositoryPresenter>(typeof(IDetailRelationRepositoryManager),
                    delegate() { return Configurator.DiContainer.Resolve<IDetailRelationRepositoryManager>(); });
                Configurator.DiContainer.RegisterType<IDetailRelationRepositoryService, DetailRelationRepositoryService>(typeof(IDetailRelationRepositoryPresenter),
                    delegate() { return Configurator.DiContainer.Resolve<IDetailRelationRepositoryPresenter>(); });

                Configurator.DiContainer.RegisterType<IPrintManager, PrintManager>(typeof(IDetailRelationRepository),
                    delegate() { return Configurator.DiContainer.Resolve<IDetailRelationRepository>(); });
                Configurator.DiContainer.RegisterType<IPrintPresenter, PrintPresenter>(typeof(IPrintManager),
                    delegate() { return Configurator.DiContainer.Resolve<IPrintManager>(); });
                Configurator.DiContainer.RegisterType<IPrintService, PrintService>(typeof(IPrintPresenter),
                    delegate() { return Configurator.DiContainer.Resolve<IPrintPresenter>(); });

                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: {0}", ex.Message);
                Console.ReadKey();
            }
        }
    }
}
