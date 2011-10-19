using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using GeoServicesSharp.Exceptions;

namespace GeoServicesSharp.Logger
{
    class FileEventLogger
    {
        public static void Warn(object message)
        {
            WriteFile(message, ErrorLevel.Warn);
        }

        public static void Error(object message)
        {
            WriteFile(message, ErrorLevel.Error);
        }

        public static void Info(object message)
        {
            WriteFile(message, ErrorLevel.Info);
        }

        protected static void WriteFile(object message, ErrorLevel err)
        {
            StreamWriter sw;
            string path = System.AppDomain.CurrentDomain.BaseDirectory + "\\" + XML.XMLLoggerGetter.GetName() + ".log";

            if (File.Exists(path))
                sw = new StreamWriter(path,true);
            else
                sw = new StreamWriter(path);

            sw.WriteLine(GetLoggingMessage(message, err));
            sw.Close();
        }

        protected enum ErrorLevel 
        {
            Info,
            Warn,
            Error,
        }

        protected static string GetErrorLevel(ErrorLevel err)
        {
            switch (err)
            {
                case ErrorLevel.Info:
                    return "INFO";
                case ErrorLevel.Warn:
                    return "WARNING";
                case ErrorLevel.Error:
                    return "ERROR";
            }
            throw new GeoServicesException("No se envío un valor válido como nivel de error");
        }

        protected static string GetTimeStamp()
        {
            return "[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "] ";
        }

        protected static string GetLoggingMessage(object message, ErrorLevel err)
        {
            return GetTimeStamp() + GetErrorLevel(err) + ": " + Convert.ToString(message);
        }
    }
}