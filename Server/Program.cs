using BusinessLogicLayer;
using BusinessLogicLayer.DataAccessInterfaces.Repositories.ConcreteDefinitions;
using DataAccessLayer.DataAccessClasses.Repositories.ConcreteDefinitions;
using System;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            new Configurator(new ConnectInfoClientService("localhost", 10000));
            Configurator._Container.RegisterType<IDetailRelationRepository, DetailRelationRepository>(
                constructorParams: new ConnectInfoDataAccess("localhost", "Kuznetsov", "msroot", "msroot", 1433).ConnectionString);

            Console.ReadKey();
        }
    }
}
