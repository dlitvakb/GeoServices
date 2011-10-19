using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace GeoServicesSharp.XML
{
    public abstract class XMLGetter
    {
        /// <summary>
        /// Provee acceso al archivo Config.xml de forma transparente
        /// </summary>
        protected static XmlDocument getXml()
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(System.AppDomain.CurrentDomain.BaseDirectory + "\\Config.xml");
            return xml;
        }
        /// <summary>
        /// Provee acceso al primer elemento que tenga el nombre especificado
        /// </summary>
        /// <exception cref="XmlException">Si no existe el elemento arroja excepción</exception>
        protected static XmlElement getSingleXMLElement(string tagName, int index = 0) {
            return (XmlElement)getXml().GetElementsByTagName(tagName)[index];
        }
    }
}