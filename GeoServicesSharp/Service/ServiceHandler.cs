using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Runtime.InteropServices;

namespace GeoServicesSharp
{
    /// <summary>
    /// Template para Servicios de ConsultoresGIS
    /// Debe crearse una clase que herede de la misma.
    /// Caso contrario... arroja excepción de acceso no autorizado
    /// </summary>
    /// <remarks>
    /// Esta clase no debe ser instanciada directamente
    /// Se debe Overridear doWork
    /// </remarks>
    public partial class ServiceHandler : ServiceBase
    {
        private Timer oTimer;

        public ServiceHandler()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            TimerCallback oCallback = start;
            oTimer = new Timer(oCallback, null, 60000, 60000);
        }

        protected override void OnStop()
        {
            try
            {
                License.LicenseInitializer.ReleaseLicense();
            }
            catch
            {
                //Si salta... es porque ya se habia cerrado... quiero EXPLICITAMENTE ignorar esto...
            }
        }

        /// <summary>
        /// En este método se debe invocar a la instancia adecuada que herede 
        /// de WorkingComponent para utilizar un GeoService o hacer una llamada
        /// a la clase que se considere apropiada en otro caso
        /// </summary>
        /// <remarks>Este método debe ser Overrideado para poder utilizar el template del Servicio</remarks>
        protected virtual void doWork()
        {
            throw new UnauthorizedAccessException("No se puede instanciar directamente esta clase... se debe heredar de ella");
        }

        /// <summary>
        /// En este método se debe invocar a la instancia adecuada que herede 
        /// de Logger para utilizar el logging
        /// Por defecto utiliza la herramienta de logging provista por GeoServices
        /// </summary>
        /// <remarks>Por defecto utiliza la herramienta de logging provista por GeoServices</remarks>
        protected virtual void InitializeLogger()
        {
            Logger.Logger.initialize();
        }

        private void start(object obj)
        {
            try
            {
                if (XML.XMLScheduleGetter.getSchedule().Contains(System.DateTime.Now.ToString("HH:mm")))
                {
                    this.oTimer.Change(60000, Timeout.Infinite);
                    this.InitializeLogger();
                    this.doWork();
                    Logger.Logger.Info("Finished");
                    this.oTimer.Change(60000, 60000);
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                Logger.Logger.Warn(ex, false);
                this.Stop();
            }
            catch (COMException ex)
            {
                Logger.Logger.Warn(ex, false);
                this.Stop();
            }
            catch (Exception ex)
            {
                Logger.Logger.Error(ex);
                this.Stop();
            }
        }
    }
}