Imports ESRI.ArcGIS.Geodatabase

Namespace SDE
    Public Class EditingWorkspace
        Implements IEditingObject

        Protected Property Workspace As IWorkspace

        Public Sub New(ByVal workspace As IWorkspace)
            Me.Workspace = workspace
        End Sub

        Public Function isVersioned() As Boolean Implements SDE.IEditingObject.isVersioned
            Dim version As String = Me.Workspace.ConnectionProperties.GetProperty("VERSION")
            Return Not version Is Nothing AndAlso Not version = ""
        End Function

        Public Sub StartEditing(ByVal WithUndoRedo As Boolean) Implements SDE.IEditingObject.StartEditing
            If Me.isVersioned() Then Me.Workspace.StartEditing(WithUndoRedo)
        End Sub

        Public Sub StartEditOperation() Implements SDE.IEditingObject.StartEditOperation
            If Me.isVersioned() Then Me.Workspace.StartEditOperation()
        End Sub

        Public Sub StopEditOperation() Implements SDE.IEditingObject.StopEditOperation
            If Me.isVersioned() Then Me.Workspace.StopEditOperation()
        End Sub

        Public Sub StopEditing(ByVal SaveEdits As Boolean) Implements SDE.IEditingObject.StopEditing
            If Me.isVersioned() Then Me.Workspace.StopEditing(SaveEdits)
        End Sub
    End Class
End Namespace