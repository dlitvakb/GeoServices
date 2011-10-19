using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoServicesSharp.SDE
{
    interface IEditingObject
    {
        void StartEditing(bool WithUndoRedo);
        void StartEditOperation();
        void StopEditOperation();
        void StopEditing(bool SaveEdits);
        bool isVersioned();
    }
}
