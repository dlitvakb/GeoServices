using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeoServicesSharp.Exceptions;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.DataSourcesGDB;

namespace GeoServicesSharp.SDE
{
        /// <summary>
    /// Permite obtener Workspaces a partir de los datos de conexión provistos a GeoServices
    /// </summary>
    /// <remarks>El password debe encontrarse encriptado localmente con la herramienta provista</remarks>
    class WorkspaceConnection
    {
        private string Username {get; set;}
        private string Password {get; set;}
        private string Server {get; set;}
        private string Instance {get; set;}
        private string Database {get; set;}
        private string Version {get; set;}

        public WorkspaceConnection(string username, string passwordEncriptado, string server, string instance, string database, string version)
        {
            if (passwordEncriptado == "") throw new GeoServicesException("El password no puede ser vacío");

            this.Username = username;
            this.Password = passwordEncriptado;
            this.Server = server;
            this.Instance = instance;
            this.Database = database;
            this.Version = version;
        }

        /// <summary>
        /// Obtiene el workspace asociado a la conexión
        /// </summary>
        public IWorkspace GetWorkspace()
        {
            try
            {
                IPropertySet2 properties = new PropertySetClass();
                properties.SetProperty("USER", this.Username);
                properties.SetProperty("PASSWORD", new Security.PrivateEncrypt().Decrypt(this.Password));
                properties.SetProperty("SERVER", this.Server);
                properties.SetProperty("INSTANCE", this.Instance);
                properties.SetProperty("DATABASE", this.Database);
                properties.SetProperty("VERSION", this.Version);

                return new SdeWorkspaceFactory().Open(properties, 0);
            }
            catch (Exception ex)
            {
                throw new GeoServicesException("No se pudo obtener el Workspace para la configuración especificada", ex);
            }
        }
    }
}