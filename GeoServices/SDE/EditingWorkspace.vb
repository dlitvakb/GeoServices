Imports ESRI.ArcGIS.Geodatabase

Namespace SDE
    Public Class EditingWorkspace
        Protected Property Dataset As IDataset

        Public Sub New(ByVal dataset As IDataset)
            If dataset Is Nothing Then Throw New DataException("No se ha enviado ningun Dataset")

            Me.Dataset = dataset
        End Sub

        ''' <summary>
        ''' Inicia sesión de edición
        ''' </summary>
        ''' <param name="WithUndoRedo"></param>
        ''' <remarks></remarks>
        Public Sub StartEditing(ByVal WithUndoRedo As Boolean)
            If Me.isVersioned() Then
                Dim wksp As IWorkspaceEdit2 = Me.getWorkspace()

                wksp.StartEditing(WithUndoRedo)
                wksp.StartEditOperation()
            End If
        End Sub

        ''' <summary>
        ''' Finaliza la sesión de edición
        ''' </summary>
        ''' <param name="SaveEdits"></param>
        ''' <remarks></remarks>
        Public Sub StopEditing(ByVal SaveEdits As Boolean)
            If Me.isVersioned() Then
                Dim wksp As IWorkspaceEdit2 = Me.getWorkspace()

                wksp.StopEditOperation()
                wksp.StopEditing(SaveEdits)
            End If
        End Sub

        ''' <summary>
        ''' Retorna el Workspace de edición
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Function getWorkspace() As IWorkspaceEdit2
            Return Me.Dataset.Workspace
        End Function

        ''' <summary>
        ''' Informa si el dataset se encuentra bajo control de versiones
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function isVersioned() As Boolean
            Return CType(Me.Dataset, IVersionedObject3).IsRegisteredAsVersioned
        End Function
    End Class
End Namespace

