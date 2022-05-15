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
				new WcfServiceConfigurator(new ConnectInfoClientService("localhost", 10000));

				WcfServiceConfigurator.DiContainer.RegisterType<ConnectInfoDataAccess>(DiLifetimeType.Transient, constructorParams: new object[] {
					"localhost",
					"Kuznetsov",
					"msroot",
					"msroot",
					(short)1433 });
				WcfServiceConfigurator.DiContainer.RegisterType<IDetailRelationRepository, DetailRelationRepository>(DiLifetimeType.Transient, constructorParams:
					WcfServiceConfigurator.DiContainer.Resolve<ConnectInfoDataAccess>().
						ConnectionString);

				WcfServiceConfigurator.DiContainer.RegisterType<IDetailRelationRepositoryManager, DetailRelationRepositoryManager>(typeof(IDetailRelationRepository),
					delegate() { return WcfServiceConfigurator.DiContainer.Resolve<IDetailRelationRepository>(); });
				WcfServiceConfigurator.DiContainer.RegisterType<IDetailRelationRepositoryPresenter, DetailRelationRepositoryPresenter>(typeof(IDetailRelationRepositoryManager),
					delegate() { return WcfServiceConfigurator.DiContainer.Resolve<IDetailRelationRepositoryManager>(); });
				WcfServiceConfigurator.DiContainer.RegisterType<IDetailRelationRepositoryService, DetailRelationRepositoryService>(typeof(IDetailRelationRepositoryPresenter),
					delegate() { return WcfServiceConfigurator.DiContainer.Resolve<IDetailRelationRepositoryPresenter>(); });

				WcfServiceConfigurator.DiContainer.RegisterType<IPrintManager, PrintManager>(typeof(IDetailRelationRepository),
					delegate() { return WcfServiceConfigurator.DiContainer.Resolve<IDetailRelationRepository>(); });
				WcfServiceConfigurator.DiContainer.RegisterType<IPrintPresenter, PrintPresenter>(typeof(IPrintManager),
					delegate() { return WcfServiceConfigurator.DiContainer.Resolve<IPrintManager>(); });
				WcfServiceConfigurator.DiContainer.RegisterType<IPrintService, PrintService>(typeof(IPrintPresenter),
					delegate() { return WcfServiceConfigurator.DiContainer.Resolve<IPrintPresenter>(); });

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
