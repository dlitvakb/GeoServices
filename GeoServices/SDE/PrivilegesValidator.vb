Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.esriSystem

Namespace SDE
    ''' <summary>
    ''' Permite conocer los permisos sobre un objeto SDE para la conexión actual
    ''' </summary>
    Public Class PrivilegesValidator
        Private Property DatasetName As IName
        Private Const SELECT_VAL As Integer = SDEPrivileges.SDESelect
        Private Const UPDATE_VAL As Integer = SDEPrivileges.SDEUpdate
        Private Const INSERT_VAL As Integer = SDEPrivileges.SDEInsert
        Private Const DELETE_VAL As Integer = SDEPrivileges.SDEDelete

        Public Sub New(ByVal dataset As IDataset)
            Me.DatasetName = dataset.FullName
        End Sub

        ''' <summary>
        ''' Retorna el valor que ESRI le otorga a los privilegios
        ''' </summary>
        Protected Function GetPrivileges() As Integer
            Return CType(DatasetName, ISQLPrivilege).SQLPrivileges
        End Function

        ''' <summary>
        ''' Informa si se pueden realizar todas las operaciones sobre un objeto SDE
        ''' </summary>
        Public ReadOnly Property CanEdit() As Boolean
            Get
                Return Me.CanSelect() AndAlso Me.CanInsert() AndAlso Me.CanUpdate() AndAlso Me.CanDelete()
            End Get
        End Property

        ''' <summary>
        ''' Informa si se puede visualizar un objeto SDE
        ''' </summary>
        Public ReadOnly Property CanSelect() As Boolean
            Get
                Return Me.CanDoAction({SELECT_VAL, SELECT_VAL + INSERT_VAL, _
                                       SELECT_VAL + UPDATE_VAL, SELECT_VAL + INSERT_VAL + UPDATE_VAL, _
                                       SELECT_VAL + DELETE_VAL, SELECT_VAL + INSERT_VAL + DELETE_VAL, _
                                       SELECT_VAL + UPDATE_VAL + DELETE_VAL, SELECT_VAL + INSERT_VAL + UPDATE_VAL + DELETE_VAL})
            End Get
        End Property

        ''' <summary>
        ''' Informa si se pueden ingresar nuevos datos en un objeto SDE
        ''' </summary>
        Public ReadOnly Property CanInsert As Boolean
            Get
                Return Me.CanDoAction({INSERT_VAL, INSERT_VAL + SELECT_VAL, _
                                       INSERT_VAL + UPDATE_VAL, INSERT_VAL + UPDATE_VAL + SELECT_VAL, _
                                       INSERT_VAL + DELETE_VAL, INSERT_VAL + SELECT_VAL + DELETE_VAL, _
                                       INSERT_VAL + UPDATE_VAL + DELETE_VAL, INSERT_VAL + SELECT_VAL + UPDATE_VAL + DELETE_VAL})
            End Get
        End Property

        ''' <summary>
        ''' Informa si se pueden modificar los datos sobre un objeto SDE
        ''' </summary>
        Public ReadOnly Property CanUpdate As Boolean
            Get
                Return Me.CanDoAction({UPDATE_VAL, UPDATE_VAL + SELECT_VAL, _
                                       UPDATE_VAL + INSERT_VAL, UPDATE_VAL + SELECT_VAL + INSERT_VAL, _
                                       UPDATE_VAL + DELETE_VAL, UPDATE_VAL + SELECT_VAL + DELETE_VAL, _
                                       UPDATE_VAL + INSERT_VAL + DELETE_VAL, UPDATE_VAL + SELECT_VAL + INSERT_VAL + DELETE_VAL})
            End Get
        End Property
        ''' <summary>
        ''' Informa si se pueden eliminar los datos de un objeto SDE
        ''' </summary>
        Public ReadOnly Property CanDelete As Boolean
            Get
                Return Me.CanDoAction({DELETE_VAL, DELETE_VAL + SELECT_VAL, _
                                       DELETE_VAL + INSERT_VAL, DELETE_VAL + UPDATE_VAL, _
                                       DELETE_VAL + SELECT_VAL + INSERT_VAL, DELETE_VAL + UPDATE_VAL + INSERT_VAL,
                                       DELETE_VAL + SELECT_VAL + UPDATE_VAL, DELETE_VAL + SELECT_VAL + INSERT_VAL + UPDATE_VAL})
            End Get
        End Property

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
        Public Function HasPrivileges(ByVal PermissionLevel As SDEPrivileges) As Boolean
            Select Case PermissionLevel
                Case SELECT_VAL
                    Return Me.CanSelect()
                Case UPDATE_VAL
                    Return Me.CanUpdate()
                Case INSERT_VAL
                    Return Me.CanInsert()
                Case DELETE_VAL
                    Return Me.CanDelete()
                Case SDEPrivileges.SDEEdit
                    Return Me.CanEdit()
                Case Else
                    Throw New DataException("Se debe ingresar un nivel correcto de permisos")
            End Select
        End Function
    End Class
End Namespace
