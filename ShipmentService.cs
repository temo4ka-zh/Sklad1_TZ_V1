using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklad1.Helpers
{
    /// <summary>
    /// Общий логгер для всего приложения
    /// </summary>
    public static class AppLogger
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public static void Error(Exception ex, string message)
        {
            Logger.Error(ex, message);
        }

        public static void Info(string message)
        {
            Logger.Info(message);
        }

        public static void Warning(string message)
        {
            Logger.Warn(message);
        }
    }
}
