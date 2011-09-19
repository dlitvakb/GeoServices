using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geodatabase;

namespace EjemploGeoApp
{
    // Extiendo de WorkingComponent
    class EjemploWorkingComponent : GeoServices.Worker.WorkingComponent
    {
        protected override void doExecute()
        {
            // Aca va el flujo de ejecución

            // Muestro todos los Feature Class por pantalla
            this.ListarFeatureClasses();
        }

        private void ListarFeatureClasses()
        {
            // Por cada Feature Class en el SDE
            foreach (IFeatureClass pFClass in new GeoServices.XML.XMLGeoGetter().GetFeatureClasses())
                // Muestro el nombre por pantalla
                Console.WriteLine(pFClass.AliasName);
        }
    }
}
