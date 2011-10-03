Imports ESRI.ArcGIS.Geodatabase

Namespace SDE
    Public Class EditingDataset
        Implements IEditingObject

        Protected Property Dataset As IDataset

        Public Sub New(ByVal dataset As IDataset)
            If dataset Is Nothing Then Throw New DataException("No se ha enviado ningun Dataset")

            Me.Dataset = dataset
        End Sub

        ''' <summary>
        ''' Inicia sesión de edición
        ''' </summary>
        Public Sub StartEditing(ByVal WithUndoRedo As Boolean) Implements IEditingObject.StartEditing
            If Me.isVersioned() Then Me.getWorkspace().StartEditing(WithUndoRedo)
        End Sub

        ''' <summary>
        ''' Inicia una operación de edición
        ''' </summary>
        Public Sub StartEditOperation() Implements IEditingObject.StartEditOperation
            If Me.isVersioned Then Me.getWorkspace().StartEditOperation()
        End Sub

        ''' <summary>
        ''' Finaliza una operación de edición
        ''' </summary>
        Public Sub StopEditOperation() Implements IEditingObject.StopEditOperation
            If Me.isVersioned Then Me.getWorkspace().StopEditOperation()
        End Sub

        ''' <summary>
        ''' Finaliza la sesión de edición
        ''' </summary>
        Public Sub StopEditing(ByVal SaveEdits As Boolean) Implements IEditingObject.StopEditing
            If Me.isVersioned() Then Me.getWorkspace().StopEditing(SaveEdits)
        End Sub

        ''' <summary>
        ''' Retorna el Workspace de edición
        ''' </summary>
        Protected Function getWorkspace() As IWorkspaceEdit2
            Return Me.Dataset.Workspace
        End Function

        ''' <summary>
        ''' Informa si el dataset se encuentra bajo control de versiones
        ''' </summary>
        Public Function isVersioned() As Boolean Implements IEditingObject.isVersioned
            Return CType(Me.Dataset, IVersionedObject3).IsRegisteredAsVersioned
        End Function
    End Class
End Namespace

