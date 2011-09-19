using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geodatabase;

namespace EjemploGeoService
{
    // Extiendo de WorkingComponent
    class EjemploWorkingComponent : GeoServices.Worker.WorkingComponent
    {
        protected override void doExecute()
        {
        // Aca va el flujo de ejecución del programa

        // Por ejemplo... Listar los nombres de los Feature Classes en el SDE
            this.ListarNombresFeatureClasses();
        }

        private void ListarNombresFeatureClasses()
        {
            // Por cada Feature Class en el SDE
            foreach(IFeatureClass pFClass in new GeoServices.SDE.FeatureClassesGateway().GetAll())
            {
            // Guardo su nombre en el log de salida
            GeoServices.Logger.Logger.Info(pFClass.AliasName);
            }
        }
    }
}
