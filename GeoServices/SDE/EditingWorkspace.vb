Imports ESRI.ArcGIS.Geodatabase

Namespace SDE
    Public Class EditingWorkspace
        Implements IEditingObject

        Protected Property Workspace As IWorkspace
        Protected ReadOnly Property EditWorkspace As IWorkspaceEdit2
            Get
                Return CType(Me.Workspace, IWorkspaceEdit2)
            End Get
        End Property

        Public Sub New(ByVal workspace As IWorkspace)
            If Not TypeOf workspace Is IWorkspaceEdit2 Then Throw New DataException("El workspace no es válido para edición")
            Me.Workspace = workspace
        End Sub

        Public Function isVersioned() As Boolean Implements SDE.IEditingObject.isVersioned
            Dim version As String = Me.Workspace.ConnectionProperties.GetProperty("VERSION")
            Return Not version Is Nothing AndAlso Not version = ""
        End Function

        Public Sub StartEditing(ByVal WithUndoRedo As Boolean) Implements SDE.IEditingObject.StartEditing
            If Me.isVersioned() Then Me.EditWorkspace.StartEditing(WithUndoRedo)
        End Sub

        Public Sub StartEditOperation() Implements SDE.IEditingObject.StartEditOperation
            If Me.isVersioned() Then Me.EditWorkspace.StartEditOperation()
        End Sub

        Public Sub StopEditOperation() Implements SDE.IEditingObject.StopEditOperation
            If Me.isVersioned() Then Me.EditWorkspace.StopEditOperation()
        End Sub

        Public Sub StopEditing(ByVal SaveEdits As Boolean) Implements SDE.IEditingObject.StopEditing
            If Me.isVersioned() Then Me.EditWorkspace.StopEditing(SaveEdits)
        End Sub
    End Class
End Namespace