using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using ESRI.ArcGIS.Geodatabase;

namespace GeoServicesSharp.XML
{    
    /// <summary>
    /// Obtiene Conexiones a una base de datos SDE a partir de la configuración especificada en Config.xml
    /// </summary>
    class XMLWorkspaceGetter : XMLGetter
    {
        /// <summary>
        /// Obtiene una lista de propiedades de conexiones
        /// </summary>
        public List<SDE.WorkspaceConnection> GetAllConnections()
        {
            List<SDE.WorkspaceConnection> connections = new List<SDE.WorkspaceConnection>();

            try
            {
                foreach (XmlElement node in getSingleXMLElement("sdeconnections").ChildNodes)
                    connections.Add(new SDE.WorkspaceConnection(node.GetAttribute("username"), node.GetAttribute("password"), node.GetAttribute("server"), node.GetAttribute("instance"), node.GetAttribute("database"), node.GetAttribute("sdeversion")));
            }
            catch (Exception ex )
            {
                Logger.Logger.Error(ex);
            }

            return connections;
        }

        /// <summary>
        /// Obtiene una lista de elementos que conforman las propiedades de conexión necesarias para una Base de Datos SDE
        /// </summary>
        public SDE.WorkspaceConnection GetSingleConnection(int index = 0) 
        {
            return this.GetAllConnections()[index];
        }
        
        /// <summary>
        /// Obtiene la conexion a una base de datos SDE
        /// </summary>
        /// <returns>Retorna un SdeWorkspace</returns>
        public IWorkspace GetSingleWorkspace(int index = 0)
        {
            return this.GetSingleConnection(index).GetWorkspace();
        }
        
        /// <summary>
        /// Obtiene una lista de workspaces a partir de las conexiones especificadas
        /// </summary>
        public List<IWorkspace> GetAllWorkspaces()
        {
            List<IWorkspace> workspaces = new List<IWorkspace>();

            foreach (SDE.WorkspaceConnection conn in this.GetAllConnections())
                workspaces.Add(conn.GetWorkspace());

            return workspaces;
        }
    }
}