using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EjemploGeoApp
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Inicializo el Logger
                GeoServices.Logger.Logger.initialize();

                // Puedo usar el Logger en cualquier lugar una vez inicializado
                GeoServices.Logger.Logger.Info("Empiezo a listar Feature Classes");

                // Hago una llamada a un WorkingComponent
                GeoServices.Worker.WorkingComponent worker = new EjemploWorkingComponent();
                worker.Execute();

                // Puedo usar el Logger en cualquier lugar una vez inicializado
                GeoServices.Logger.Logger.Info("Terminé =)");

                Console.ReadKey();
            }
            // Imito la estructura de captura de excepciones de la plantilla ServiceHandler
            catch (UnauthorizedAccessException ex)
            {
                GeoServices.Logger.Logger.Warn(ex);
            }
            catch (Exception ex)
            {
                GeoServices.Logger.Logger.Error(ex);
            }
        }
    }
}
