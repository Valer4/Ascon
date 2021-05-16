using BusinessLogicLayer;
using System;
using System.Windows.Forms;
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

                Application.Run(Configurator._Container.Resolve<DetailEditor>());
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
