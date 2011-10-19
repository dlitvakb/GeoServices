using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace GeoServicesSharp.XML
{
    /// <summary>
    /// Obtiene el itinerario del archivo Config.xml
    /// </summary>
    class XMLScheduleGetter : XMLGetter
    {
        /// <summary>
        /// Obtiene una lista de horarios para los cuales está configurado el servicio
        /// </summary>
        /// <returns>Lista de horarios en formato HH:mm</returns>
        public static List<string> getSchedule()
        {
            List<string> schedule = new List<string>();

            try
            {
                foreach (XmlElement nodo in getSingleXMLElement("schedule").GetElementsByTagName("time"))
                    schedule.Add(nodo.GetAttribute("start"));
            }
            catch (Exception ex)
            {
                Logger.Logger.Error(ex);
            }

            return schedule;
        }
    }
}