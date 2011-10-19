using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeoServicesSharp.Exceptions;
using System.Runtime.InteropServices;

namespace GeoServicesSharp.Worker
{
    /// <summary>
    /// Wrapper de GeoService
    /// Inicializa licencia y captura errores
    /// Esta clase debe ser heredada y sus subclases llamadas desde el método doWork() de las subclases de ServiceHandler
    /// </summary>
    /// <remarks>
    /// Los errores generados por el usuario deben ser del tipo DataException
    /// Los errores generados por ESRI son del tipo COMException
    /// Dichas excepciones no interrumpirán el flujo de ejecución, 
    /// y son consideradas como intermedias (usuario) o graves (framework), 
    /// pero no detendrán la ejecución del servicio, ya que deben ser consideradas 
    /// como un error de configuración en el Config.xml
    /// 
    /// En caso de requerir otro tipo de excepción, ServiceHandler provee el manejo de excepciones
    /// consideradas como intermedias y graves que ameritan la detención del servicio.
    /// UnauthorizedAccessException es considerada intermedia y es utilizada para
    /// el aviso de una instanciación ilegal de una clase o la falta de licencia del producto.
    /// Exception es cualquier otro tipo de excepción (arrojada por el usuario) que sea considerada grave,
    /// la misma detendrá la ejecución del servicio.
    /// </remarks>
    public abstract class WorkingComponent
    {
        private const string LICENSE_EXCEPTION_MESSAGE = "ARCGIS VERSION NOT SPECIFIED";

        public void Execute()
        {
            try
            {
                License.LicenseInitializer.InitializeLicense();
                this.doExecute();
                License.LicenseInitializer.ReleaseLicense();
            }
            catch (GeoServicesException ex)
            {
                Logger.Logger.Warn(ex, false);
            }
            catch (COMException ex)
            {
                if (ex.Message.ToUpper().Contains(LICENSE_EXCEPTION_MESSAGE)) throw ex;
                Logger.Logger.Error(ex);
            }
        }

        /// <summary>
        /// En este método se debe especificar el flujo de ejecución del servicio
        /// </summary>
        /// <remarks>
        /// Como buena práctica se considera correcto el realizar unicamente delegaciones
        /// o llamadas a otros métodos desde este. Evitar los condicionales o bucles en este método
        /// permite una mayor limpieza en el código y mejora notablemente la mantenibilidad
        /// </remarks>
        protected abstract void doExecute();
    }
}