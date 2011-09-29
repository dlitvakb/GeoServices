Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.esriSystem

Namespace SDE
    ''' <summary>
    ''' Permite conocer los permisos sobre un objeto SDE para la conexión actual
    ''' </summary>
    ''' <remarks></remarks>
    Public Class PrivilegesValidator
        Private Property DatasetName As IName
        Private Const SELECT_VAL As Integer = SDEPermissions.SDESelect
        Private Const UPDATE_VAL As Integer = SDEPermissions.SDEUpdate
        Private Const INSERT_VAL As Integer = SDEPermissions.SDEInsert
        Private Const DELETE_VAL As Integer = SDEPermissions.SDEDelete

        Public Sub New(ByVal dataset As IDataset)
            Me.DatasetName = dataset.FullName
        End Sub

        ''' <summary>
        ''' Retorna el valor que ESRI le otorga a los privilegios
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Function GetPrivileges() As Integer
            Return CType(DatasetName, ISQLPrivilege).SQLPrivileges
        End Function

        ''' <summary>
        ''' Informa si se pueden realizar todas las operaciones sobre un objeto SDE
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function CanEdit() As Boolean
            Return Me.CanSelect() AndAlso Me.CanInsert() AndAlso Me.CanUpdate() AndAlso Me.CanDelete()
        End Function

        ''' <summary>
        ''' Informa si se puede visualizar un objeto SDE
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function CanSelect() As Boolean
            Return Me.CanDoAction({SELECT_VAL, SELECT_VAL + INSERT_VAL, _
                                   SELECT_VAL + UPDATE_VAL, SELECT_VAL + INSERT_VAL + UPDATE_VAL, _
                                   SELECT_VAL + DELETE_VAL, SELECT_VAL + INSERT_VAL + DELETE_VAL, _
                                   SELECT_VAL + UPDATE_VAL + DELETE_VAL, SELECT_VAL + INSERT_VAL + UPDATE_VAL + DELETE_VAL})
        End Function

        ''' <summary>
        ''' Informa si se pueden ingresar nuevos datos en un objeto SDE
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function CanInsert() As Boolean
            Return Me.CanDoAction({INSERT_VAL, INSERT_VAL + SELECT_VAL, _
                                   INSERT_VAL + UPDATE_VAL, INSERT_VAL + UPDATE_VAL + SELECT_VAL, _
                                   INSERT_VAL + DELETE_VAL, INSERT_VAL + SELECT_VAL + DELETE_VAL, _
                                   INSERT_VAL + UPDATE_VAL + DELETE_VAL, INSERT_VAL + SELECT_VAL + UPDATE_VAL + DELETE_VAL})
        End Function

        ''' <summary>
        ''' Informa si se pueden modificar los datos sobre un objeto SDE
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function CanUpdate() As Boolean
            Return Me.CanDoAction({UPDATE_VAL, UPDATE_VAL + SELECT_VAL, _
                                   UPDATE_VAL + INSERT_VAL, UPDATE_VAL + SELECT_VAL + INSERT_VAL, _
                                   UPDATE_VAL + DELETE_VAL, UPDATE_VAL + SELECT_VAL + DELETE_VAL, _
                                   UPDATE_VAL + INSERT_VAL + DELETE_VAL, UPDATE_VAL + SELECT_VAL + INSERT_VAL + DELETE_VAL})
        End Function

        ''' <summary>
        ''' Informa si se pueden eliminar los datos de un objeto SDE
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function CanDelete() As Boolean
            Return Me.CanDoAction({DELETE_VAL, DELETE_VAL + SELECT_VAL, _
                                   DELETE_VAL + INSERT_VAL, DELETE_VAL + UPDATE_VAL, _
                                   DELETE_VAL + SELECT_VAL + INSERT_VAL, DELETE_VAL + UPDATE_VAL + INSERT_VAL,
                                   DELETE_VAL + SELECT_VAL + UPDATE_VAL, DELETE_VAL + SELECT_VAL + INSERT_VAL + UPDATE_VAL})
        End Function

        ''' <summary>
        ''' Función auxiliar para los cálculos necesarios de permisos (ya que por cada acción hay un conjunto de valores posibles, esta función evalua dicha lista)
        ''' </summary>
        ''' <param name="possiblePrivilegeValues"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Function CanDoAction(ByVal possiblePrivilegeValues As Integer()) As Boolean
            Return possiblePrivilegeValues.Contains(Me.GetPrivileges)
        End Function

        ''' <summary>
        ''' Informa si se tienen permisos para la acción pedida
        ''' </summary>
        ''' <param name="PermissionLevel"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function HasPermissions(ByVal PermissionLevel As SDEPermissions) As Boolean
            Select Case PermissionLevel
                Case SELECT_VAL
                    Return Me.CanSelect()
                Case UPDATE_VAL
                    Return Me.CanUpdate()
                Case INSERT_VAL
                    Return Me.CanInsert()
                Case DELETE_VAL
                    Return Me.CanDelete()
                Case SDEPermissions.SDEEdit
                    Return Me.CanEdit()
                Case Else
                    Return Me.CanDoAction({PermissionLevel})
            End Select
        End Function
    End Class
End Namespace
