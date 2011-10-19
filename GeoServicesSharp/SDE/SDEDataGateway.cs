using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geodatabase;
using GeoServicesSharp.Exceptions;

namespace GeoServicesSharp.SDE
{
    /// <summary>
    /// Clase abstracta que permite la obtención genérica de datos del SDE
    /// </summary>
    /// <typeparam name="T">Clase que representa al elemento a obtener del SDE. Por Ejemplo: IFeatureClass, ITable</typeparam>
    public abstract class SDEDataGateway<T> where T : class
    {
        /// <summary>
        /// Permite obtener todos los elementos del tipo especificado en la subclase presentes en el SDE para la conexión especificada.
        /// Por defecto retorna unicamente los elementos para los cuales se tienen permisos de edición, para obtener todos los elementos
        /// en el parámetro RequiresEditorPriviledges poner el valor False
        /// </summary>
        public virtual List<T> GetAll(int connectionNumber = 0, SDEPrivileges Privileges = SDE.SDEPrivileges.SDEEdit) 
        {
            IWorkspace wksp = new XML.XMLWorkspaceGetter().GetSingleWorkspace(connectionNumber);
            if (wksp == null) throw new GeoServicesException("No se ha provisto ningún workspace");

            List<T> elements = this.doGetAll(wksp, Privileges);

            if (elements.Count == 0) throw new GeoServicesException("No se ha encontrado ningun elemento");

            return elements;
        }

        protected abstract List<T> doGetAll(IWorkspace workspace, SDEPrivileges Privileges);

        /// <summary>
        /// Realiza la verificación de nombres para el objeto de SDE
        /// </summary>
        protected abstract bool IsNameEquals(T element, string name);

        /// <summary>
        /// Permite obtener elementos del SDE en base a una lista de nombres
        /// </summary>
        /// <remarks>Por defecto, si no se encuentra algún elemento, se lanza una DataException, sin embargo, cambiando GetResultsAnyway, permite obtener las que se haya encontrado</remarks>
        public List<T> GetByNameList(string[] names, int connectionNumber = 0, bool GetResultsAnyway = false, SDEPrivileges Privileges = SDEPrivileges.SDEEdit)
        {
            List<T> result = new List<T>();

            foreach (string name in names)
            {
                try
                {
                    result.Add(this.GetByName(name, connectionNumber, Privileges));
                }
                catch (Exception ex)
                {
                    if (!GetResultsAnyway) throw new GeoServicesException("No se encontraron elementos para los nombres especificados", ex);
                    else continue;
                }
            }

            return result;
        }

        /// <summary>
        /// Permite obtener elementos singulares dentro del SDE
        /// </summary>
        public virtual T GetByName(string name, int connectionNumber = 0, SDEPrivileges Privileges = SDE.SDEPrivileges.SDEEdit)
        {
            IFeatureWorkspace wksp = new XML.XMLWorkspaceGetter().GetSingleWorkspace(connectionNumber) as IFeatureWorkspace;
            if (wksp == null) throw new GeoServicesException("No se ha provisto ningún workspace");
            try
            {
                T elem = this.doGetByName(name, wksp);
                if (this.PermissionsValidation(elem, Privileges)) return elem;
                else throw new UnauthorizedAccessException("No se tienen permisos suficientes para acceder al elemento");
            }
            catch (UnauthorizedAccessException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new GeoServicesException("El elemento " + name + " no se ha encontrado", ex);
            }
        }

        /// <summary>
        /// Realiza chequeos de permisos sobre los objetos SDE
        /// </summary>
        protected virtual bool PermissionsValidation(T element, SDEPrivileges Privileges = SDEPrivileges.SDEEdit)
        {
            return new PrivilegesValidator((IDataset)element).HasPrivileges(Privileges);
        }

        protected string SanitizeString(string text)
        {
            return text.ToLower().Replace("á", "a").Replace("é", "e").Replace("í", "i").Replace("ó", "o").Replace("ú", "u");
        }
                
        /// <summary>
        /// Obtiene los elementos por nombre del SDE
        /// </summary>
        protected abstract T doGetByName(string name, IFeatureWorkspace workspace);
    }
}

