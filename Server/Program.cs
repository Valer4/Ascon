using BusinessLogicLayer;
using BusinessLogicLayer.DataAccessInterfaces.Repositories.ConcreteDefinitions;
using BusinessLogicLayer.Managers.EntityManagers.Classes.ConcreteDefinitions;
using BusinessLogicLayer.Managers.Print;
using BusinessLogicLayer.Services.Print;
using BusinessLogicLayer.Services.Repositories.Classes.ConcreteDefinitions;
using BusinessLogicLayer.Services.Repositories.Interfaces.ConcreteDefinitions;
using DataAccessLayer.DataAccessClasses.Repositories.ConcreteDefinitions;
using System;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                new Configurator(new ConnectInfoClientService("localhost", 10000));

                Configurator._Container.RegisterType<IDetailRelationRepository, DetailRelationRepository>(
                    constructorParams: new ConnectInfoDataAccess("localhost", "Kuznetsov", "msroot", "msroot", 1433).
                        ConnectionString);

                Configurator._Container.RegisterType<IDetailRelationRepositoryService, DetailRelationRepositoryService>(
                    constructorParams: new DetailRelationRepositoryManager(
                        Configurator._Container.Resolve<IDetailRelationRepository>()));

                Configurator._Container.RegisterType<IPrintService, PrintService>(
                    constructorParams: new PrintManager(
                        Configurator._Container.Resolve<IDetailRelationRepository>()));

                Console.ReadKey();
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error: {0}", ex.Message);
                Console.ReadKey();
            }
        }
    }
}
