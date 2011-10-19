using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geodatabase;
using GeoServicesSharp.Exceptions;

namespace GeoServicesSharp.SDE
{
    class EditingWorkspace : IEditingObject
    {
        protected IWorkspace Workspace {get; set;}
        protected IWorkspaceEdit2 EditWorkspace
        {
            get
            {
                return (IWorkspaceEdit2)this.Workspace;
            }
        }

        public EditingWorkspace(IWorkspace workspace)
        {
            if (!(workspace is IWorkspaceEdit2)) throw new GeoServicesException("El workspace no es válido para edición");
            this.Workspace = workspace;
        }

        public bool isVersioned()
        {
            string version = (string)this.Workspace.ConnectionProperties.GetProperty("VERSION");
            return !(version == null) &&  !(version == "");
        }

        public void StartEditing(bool WithUndoRedo)
        {
            if (this.isVersioned()) this.EditWorkspace.StartEditing(WithUndoRedo);
        }

        public void StartEditOperation()
        {
            if (this.isVersioned()) this.EditWorkspace.StartEditOperation();
        }

        public void StopEditOperation()
        {
            if (this.isVersioned()) this.EditWorkspace.StopEditOperation();
        }

        public void StopEditing(bool SaveEdits)
        {
            if (this.isVersioned()) this.EditWorkspace.StopEditing(SaveEdits);
        }
    }
}
