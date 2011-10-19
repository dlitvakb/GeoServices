using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geodatabase;

namespace GeoServicesSharp.SDE
{
    public class TableGateway : SDEDataGateway<ITable>
    {
        /// <summary>
        /// Obtiene todas las tablas del SDE para la conexión especificada
        /// </summary>
        /// <remarks>El esquema de obtención está realizado para tablas sin anidamiento que se encuentran en la raíz del SDE</remarks>
        protected override List<ITable> doGetAll(IWorkspace workspace, SDEPrivileges Privileges)
        {
            List<ITable> tables = new List<ITable>();

            IEnumDataset datasets = workspace.get_Datasets(esriDatasetType.esriDTTable);
            IDataset table = datasets.Next();
            while (table != null)
            {
                if (this.PermissionsValidation((ITable)table, Privileges)) tables.Add((ITable)table);
                table = datasets.Next();
            }

            System.Runtime.InteropServices.Marshal.ReleaseComObject(datasets);

            return tables;
        }

        protected override bool IsNameEquals(ITable element, string name)
        {
            return ((IDataset)element).Name.ToUpper().Contains(name.ToUpper());
        }

        protected override ITable doGetByName(string name, IFeatureWorkspace workspace)
        {
            return workspace.OpenTable(name);
        }
    }
}
