using BusinessLogicLayer;
using System;
using System.Windows.Forms;
using UserInterfaceLayer.Clients.Print;
using UserInterfaceLayer.Clients.Repositories.Interfaces.ConcreteDefinitions;
using UserInterfaceLayer.Forms.Presenters;
using UserInterfaceLayer.Forms.Views;

namespace UserInterfaceLayer
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                new Configurator(new ConnectInfoClientService("localhost", 10000));

                DetailEditor view = new DetailEditor();
                new DetailRelationRepositoryPresenter(view, Configurator.Container.Resolve<IDetailRelationRepositoryClient>());
                new PrintPresenter(view, Configurator.Container.Resolve<IPrintClient>());
                Application.Run(view);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
