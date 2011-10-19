using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace GeoServicesSharp.Logger
{
    /// <summary>
    /// Provee la interfaz para el event viewer
    /// </summary>
    /// <remarks>Se debe especificar el nombre de la aplicación en Config.xml</remarks>
    class ServiceEventWriter
    {
        private static string _source = XML.XMLLoggerGetter.GetName() + "Events";
        private static string _log = XML.XMLLoggerGetter.GetName() + "Log";

        private static EventLog checkCreate()
        {
            if (!EventLog.SourceExists(_source)) EventLog.CreateEventSource(_source, _log);
            return new EventLog(_log, System.Environment.MachineName, _source);
        }

        public static void Warn(object message)
        {
            checkCreate().WriteEntry(message.ToString(), EventLogEntryType.Warning);
        }

        public static void Error(object message)
        {
            checkCreate().WriteEntry(message.ToString(), EventLogEntryType.Error);
        }

        public static void Info(object message)
        {
            checkCreate().WriteEntry(message.ToString(), EventLogEntryType.Information);
        }
    }
}