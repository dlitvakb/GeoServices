using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace EjemploGeoService
{
    // Extiendo de ServiceHandler
    public partial class EjemploGeoService : GeoServices.ServiceHandler
    {
        protected override void doWork()
        {
        // Acá va el codigo de las llamadas a  WorkingComponents

        // En este caso, como la llamada es a un solo WorkingComponent 
        // no hace falta crear nuevos hilos de ejecución
        GeoServices.Worker.WorkingComponent worker = new EjemploWorkingComponent();
        worker.Execute();
        }
    }
}
