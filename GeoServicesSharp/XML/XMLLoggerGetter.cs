using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoServicesSharp.XML
{
    class XMLLoggerGetter : XMLGetter
    {
        public static string GetName()
        {
            return getSingleXMLElement("application").GetAttribute("name");
        }
    }
}
