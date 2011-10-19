using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS;
using ESRI.ArcGIS.esriSystem;

namespace GeoServicesSharp.License
{
    /// <summary>
    /// Inicializador de licencias
    /// </summary>
    /// <remarks>Es utilizado para comprobar que las licencias activas tienen permisos de edición sobre el SDE</remarks>
    public class LicenseInitializer
    {
        private const string ARCSERVER_ERROR = "FAILED TO BIND TO AN ARCGIS SERVER RUNTIME";

        /// <summary>
        /// Inicializa la licencia, para que sea exitoso, se debe tener una de las siguientes 4 licencias (las cuales tienen permisos sobre SDE) 
        /// y seran buscadas en el siguiente orden:
        /// 1) ArcServer
        /// 2) ArcEngine Geodatabase Extension
        /// 3) ArcEditor
        /// 4) ArcInfo
        /// </summary>
        public static void InitializeLicense()
        {
            try
            {
                RuntimeManager.BindLicense(ProductCode.Server, LicenseLevel.GeodatabaseUpdate);
            }
            catch (RuntimeManagerException ex) 
            {
                if (ex.Message.ToUpper().Contains(ARCSERVER_ERROR)) 
                    RuntimeManager.BindLicense(ProductCode.EngineOrDesktop, LicenseLevel.GeodatabaseUpdate);
                else
                    throw ex;
            }
        }

        /// <summary>
        /// Libera la licencia
        /// </summary>
        public static void ReleaseLicense()
        {
            AoInitialize license = new AoInitialize();
            license.Shutdown();
        }
    }
}
