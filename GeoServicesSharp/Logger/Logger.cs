using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoServicesSharp.Logger
{
    /// <summary>
    /// Wrapper para logging de servicios con acceso al event viewer y archivos log de texto
    /// </summary>
    /// <remarks>Se debe especificar el nombre de la aplicación en el Config.xml</remarks>
    public class Logger
    {
        public static void initialize()
        {
            Logger.Info("Program Started");
        }

        public static void Info(object message)
        {
            FileEventLogger.Info(message);
        }

        public static void Warn(object message, bool LogFileOnly = true)
        {
            FileEventLogger.Warn(message);
            if (!LogFileOnly) ServiceEventWriter.Warn(message);
        }

        public static void Error(object message)
        {
            FileEventLogger.Error(message);
            ServiceEventWriter.Error(message);
        }
    }
}