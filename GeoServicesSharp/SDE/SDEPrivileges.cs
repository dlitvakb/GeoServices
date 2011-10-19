using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoServicesSharp.SDE
{
    /// <summary>
    /// Valores de posibles permisos en los objetos SDE.
    /// SDEEdit es la combinacion de todos los permisos
    /// </summary>
    public enum SDEPrivileges
    {
        SDESelect = 1,
        SDEUpdate = 2,
        SDEInsert = 4,
        SDEDelete = 8,
        SDEEdit = 15
    }
}
