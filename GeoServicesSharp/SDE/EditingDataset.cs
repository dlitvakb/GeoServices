using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geodatabase;
using GeoServicesSharp.Exceptions;

namespace GeoServicesSharp.SDE
{
    class EditingDataset : IEditingObject
    {
        protected IDataset Dataset {get; set;}

        public EditingDataset(IDataset dataset)
        {
            if (dataset == null) throw new GeoServicesException("No se ha enviado ningun Dataset");

            this.Dataset = dataset;
        }

        /// <summary>
        /// Inicia sesión de edición
        /// </summary>
        public void StartEditing(bool WithUndoRedo)
        {
            if (this.isVersioned()) this.getWorkspace().StartEditing(WithUndoRedo);
        }

        /// <summary>
        /// Inicia una operación de edición
        /// </summary>
        public void StartEditOperation()
        {
            if (this.isVersioned()) this.getWorkspace().StartEditOperation();
        }

        /// <summary>
        /// Finaliza una operación de edición
        /// </summary>
        public void StopEditOperation()
        {
            if (this.isVersioned()) this.getWorkspace().StopEditOperation();
        }

        /// <summary>
        /// Finaliza la sesión de edición
        /// </summary>
        public void StopEditing(bool SaveEdits) 
        {
            if (this.isVersioned()) this.getWorkspace().StopEditing(SaveEdits);
        }

        /// <summary>
        /// Retorna el Workspace de edición
        /// </summary>
        protected IWorkspaceEdit2 getWorkspace()
        {
            return (IWorkspaceEdit2)this.Dataset.Workspace;
        }

        /// <summary>
        /// Informa si el dataset se encuentra bajo control de versiones
        /// </summary>
        public bool isVersioned()
        {
            return ((IVersionedObject3)this.Dataset).IsRegisteredAsVersioned;
        }
    }
}
