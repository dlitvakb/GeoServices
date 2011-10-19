using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using GeoServicesSharp.Exceptions;

namespace GeoServicesSharp.SDE
{
    /// <summary>
    /// Permite conocer los permisos sobre un objeto SDE para la conexión actual
    /// </summary>
    public class PrivilegesValidator
    {
        private IName DatasetName {get; set;}
        private const int SELECT_VAL = (int)SDEPrivileges.SDESelect;
        private const int UPDATE_VAL = (int)SDEPrivileges.SDEUpdate;
        private const int INSERT_VAL = (int)SDEPrivileges.SDEInsert;
        private const int DELETE_VAL = (int)SDEPrivileges.SDEDelete;

        public PrivilegesValidator(IDataset dataset)
        {
            this.DatasetName = dataset.FullName;
        }
        
        /// <summary>
        /// Retorna el valor que ESRI le otorga a los privilegios
        /// </summary>
        protected int GetPrivileges()
        { 
            return ((ISQLPrivilege)this.DatasetName).SQLPrivileges;
        }
        
        /// <summary>
        /// Informa si se pueden realizar todas las operaciones sobre un objeto SDE
        /// </summary>
        public bool CanEdit 
        {
            get
            {
                return this.CanSelect && this.CanInsert && this.CanUpdate && this.CanDelete;
            }
        }
        
        /// <summary>
        /// Informa si se puede visualizar un objeto SDE
        /// </summary>
        public bool CanSelect
        {
            get
            {
                List<int> possible = new List<int>();
                possible.Add(SELECT_VAL);
                possible.Add(SELECT_VAL + INSERT_VAL);
                possible.Add(SELECT_VAL + UPDATE_VAL);
                possible.Add(SELECT_VAL + INSERT_VAL + UPDATE_VAL);
                possible.Add(SELECT_VAL + DELETE_VAL);
                possible.Add(SELECT_VAL + INSERT_VAL + DELETE_VAL);
                possible.Add(SELECT_VAL + UPDATE_VAL + DELETE_VAL);
                possible.Add(SELECT_VAL + INSERT_VAL + UPDATE_VAL + DELETE_VAL);
                return this.CanDoAction(possible);
            }
        }
        
        /// <summary>
        /// Informa si se pueden ingresar nuevos datos en un objeto SDE
        /// </summary>
        public bool CanInsert
        {
            get
            {
                List<int> possible = new List<int>();
                possible.Add(INSERT_VAL);
                possible.Add(INSERT_VAL + SELECT_VAL);
                possible.Add(INSERT_VAL + UPDATE_VAL);
                possible.Add(INSERT_VAL + UPDATE_VAL + SELECT_VAL);
                possible.Add(INSERT_VAL + DELETE_VAL);
                possible.Add(INSERT_VAL + SELECT_VAL + DELETE_VAL);
                possible.Add(INSERT_VAL + UPDATE_VAL + DELETE_VAL);
                possible.Add(INSERT_VAL + SELECT_VAL + UPDATE_VAL + DELETE_VAL);
                return this.CanDoAction(possible);
            }
        }
        
        /// <summary>
        /// Informa si se pueden modificar los datos sobre un objeto SDE
        /// </summary>
        public bool CanUpdate
        {
            get
            {
                List<int> possible = new List<int>();
                possible.Add(UPDATE_VAL);
                possible.Add(UPDATE_VAL + SELECT_VAL);
                possible.Add(UPDATE_VAL + INSERT_VAL);
                possible.Add(UPDATE_VAL + SELECT_VAL + INSERT_VAL);
                possible.Add(UPDATE_VAL + DELETE_VAL);
                possible.Add(UPDATE_VAL + SELECT_VAL + DELETE_VAL);
                possible.Add(UPDATE_VAL + INSERT_VAL + DELETE_VAL);
                possible.Add(UPDATE_VAL + SELECT_VAL + INSERT_VAL + DELETE_VAL);
                return this.CanDoAction(possible);
            }
        }
        
        /// <summary>
        /// Informa si se pueden eliminar los datos de un objeto SDE
        /// </summary>
        public bool CanDelete
        {
            get
            {
                List<int> possible = new List<int>();
                possible.Add(DELETE_VAL);
                possible.Add(DELETE_VAL + SELECT_VAL);
                possible.Add(DELETE_VAL + INSERT_VAL);
                possible.Add(DELETE_VAL + UPDATE_VAL);
                possible.Add(DELETE_VAL + SELECT_VAL + INSERT_VAL);
                possible.Add(DELETE_VAL + UPDATE_VAL + INSERT_VAL);
                possible.Add(DELETE_VAL + SELECT_VAL + UPDATE_VAL);
                possible.Add(DELETE_VAL + SELECT_VAL + INSERT_VAL + UPDATE_VAL);
                return this.CanDoAction(possible);
            }
        }

        /// <summary>
        /// Función auxiliar para los cálculos necesarios de permisos (ya que por cada acción hay un conjunto de valores posibles, esta función evalua dicha lista)
        /// </summary>
        /// <param name="possiblePrivilegeValues"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        protected bool CanDoAction(List<int> possiblePrivilegeValues)
        {
            return possiblePrivilegeValues.Contains(this.GetPrivileges());
        }

        /// <summary>
        /// Informa si se tienen permisos para la acción pedida
        /// </summary>
        /// <param name="PermissionLevel"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool HasPrivileges(SDEPrivileges PermissionLevel)
        {
            switch ((int)PermissionLevel)
            {
                case SELECT_VAL:
                    return this.CanSelect;
                case UPDATE_VAL:
                    return this.CanUpdate;
                case INSERT_VAL:
                    return this.CanInsert;
                case DELETE_VAL:
                    return this.CanDelete;
                case (int)SDEPrivileges.SDEEdit:
                    return this.CanEdit;
                default:
                    throw new GeoServicesException("Se debe ingresar un nivel correcto de permisos");
            }
        }
    }
}