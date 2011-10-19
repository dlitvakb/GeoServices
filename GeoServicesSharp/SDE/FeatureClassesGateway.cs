using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geodatabase;
using GeoServicesSharp.Exceptions;

namespace GeoServicesSharp.SDE
{
    /// <summary>
    /// Permite obtener FeatureClasses y Tablas de una base de datos SDE
    /// </summary>
    /// <remarks>
    /// Los esquemas de obtención de datos están adaptados siguiente formato:
    ///     SDE -> FeatureClasses (muchos),
    ///     SDE -> FeatureDatasets (muchos) -> FeatureClasses (muchos)
    /// 
    /// Si se desea alterar, debe crease una nueva clase que herede de XMLGetter.
    /// En caso de querer el mismo esquema, pero requerir una validación diferente para los FeatureClass o Tablas
    /// se debe crear una clase que herede de esta y sobreescribir el método isValid realizando la validación correspondiente 
    /// </remarks>
    public class FeatureClassesGateway : SDEDataGateway<IFeatureClass>
    {
        /// <summary>
        /// Retorna el siguiente Feature Class
        /// </summary>
        protected IFeatureClass getNextFClass(IEnumDataset subset, IDataset dataset)
        {
            while (!this.isFClass(dataset))
            {
                dataset = subset.Next();
                if (dataset == null) return null;
            }
            return (IFeatureClass)dataset;
        }

        /// <summary>
        /// Retorna si el Dataset ingresado es Feature Class
        /// </summary>
        protected bool isFClass(IDataset dataset)
        {
            return (dataset is IFeatureClass || dataset == null);
        }

        /// <summary>
        /// Realiza la validación de un dataset (Feature Class, Tabla, Feature Dataset...) según lo especificado
        /// </summary>
        /// <remarks>
        /// La validación realizada acá es que sea una capa en producción y no sea de red.
        /// Para realizar otra validación se debe generar una subclase que sobreescriba este método
        /// </remarks>
        protected bool validDataset(IDataset dataset)
        {
            return !this.SanitizeString(dataset.Name).ToUpper().Contains("AUDITORIA") && !this.SanitizeString(dataset.Name).ToUpper().Contains("HISTORICO") && !this.SanitizeString(dataset.Name).ToUpper().Contains("RED") && dataset is IFeatureClass;
        }

        /// <summary>
        /// Realiza la validación de un dataset (Feature Class, Tabla, Feature Dataset...) según lo especificado
        /// </summary>
        /// <remarks>
        /// La validación realizada acá es que sea una capa en producción y no sea de red.
        /// Para realizar otra validación se debe generar una subclase que sobreescriba este método
        /// </remarks>
        protected bool validFeatureClass(IFeatureClass fclass)
        {
            return !this.SanitizeString(fclass.AliasName).ToUpper().Contains("AUDITORIA") && !this.SanitizeString(fclass.AliasName).ToUpper().Contains("HISTORICO") && !this.SanitizeString(fclass.AliasName).ToUpper().Contains("RED");
        }

        /// <summary>
        /// Realiza la validación de un dataset (Feature Class, Tabla, Feature Dataset...) según lo especificado
        /// </summary>
        /// <remarks>
        /// La validación realizada acá es que sea una capa en producción y no sea de red.
        /// Para realizar otra validación se debe generar una subclase que sobreescriba este método
        /// </remarks>
        protected bool isValid(IDataset dataset, SDEPrivileges Privileges = SDEPrivileges.SDEEdit)
        {
            return this.validFeatureClass((IFeatureClass)dataset) && this.validDataset(dataset) && this.PermissionsValidation((IFeatureClass)dataset, Privileges);
        }

        /// <summary>
        /// Obtiene una lista de todos los FeatureClasses para la conexión especificada
        /// </summary>
        protected override List<IFeatureClass> doGetAll(IWorkspace workspace, SDEPrivileges Privileges)
        {
            List<IFeatureClass> fclasses = new List<IFeatureClass>();

            IEnumDataset datasets = workspace.get_Datasets(esriDatasetType.esriDTFeatureDataset);
            IEnumDataset featureClasses = workspace.get_Datasets(esriDatasetType.esriDTFeatureClass);

            IFeatureClass fclass = featureClasses.Next() as IFeatureClass;

            while (fclass != null)
            {
                if (this.isValid((IDataset)fclass, Privileges)) fclasses.Add(fclass);
                fclass = featureClasses.Next() as IFeatureClass;
            }

            System.Runtime.InteropServices.Marshal.ReleaseComObject(featureClasses);

            IFeatureDataset dataset = datasets.Next() as IFeatureDataset;
            while (dataset != null)
            {
                if (dataset is IFeatureClass && this.isValid(dataset, Privileges)) fclasses.Add((IFeatureClass)dataset);
                if (dataset.Subsets != null)
                {
                    IEnumDataset subset = dataset.Subsets;
                    fclass = this.getNextFClass(subset, subset.Next());
                    while (fclass != null)
                    {
                        if (this.isValid((IDataset)fclass, Privileges)) fclasses.Add(fclass);
                        fclass = this.getNextFClass(subset, subset.Next());
                    }
                }
                dataset = datasets.Next() as IFeatureDataset;
            }

            System.Runtime.InteropServices.Marshal.ReleaseComObject(datasets);

            return fclasses;
        }

        protected IFeatureClass ReturnSingleElement(string name, IFeatureClass fclass, SDEPrivileges Privileges)
        {
            if (this.IsNameEquals(fclass, name))
            {
                if (this.PermissionsValidation(fclass, Privileges)) return fclass;
                else throw new GeoServicesException("El FeatureClass " + name + " no puede ser abierto para edición");
            }
            else return null;
        }

        protected override bool IsNameEquals(IFeatureClass element, string name)
        {
            return ((IDataset)element).Name.ToUpper().Contains(name.ToUpper()) || element.AliasName.ToUpper().Contains(name.ToUpper());
        }

        protected override IFeatureClass doGetByName(string name, IFeatureWorkspace workspace)
        {
            return workspace.OpenFeatureClass(name);
        }
    }
}
