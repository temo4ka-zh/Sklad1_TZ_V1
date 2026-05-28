using NLog;
using Sklad1.Forms;

namespace Sklad1
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        [STAThread]
        static void Main()
        {
            var config = new NLog.Config.XmlLoggingConfiguration("NLog.config");
            LogManager.Configuration = config;

            Logger.Info("Приложение запущено");


            ApplicationConfiguration.Initialize();
            Application.Run(new FormLogin());
        }
    }
}
