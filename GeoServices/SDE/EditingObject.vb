Namespace SDE
    Public Interface IEditingObject
        Sub StartEditing(ByVal WithUndoRedo As Boolean)
        Sub StartEditOperation()
        Sub StopEditOperation()
        Sub StopEditing(ByVal SaveEdits As Boolean)
        Function isVersioned() As Boolean
    End Interface
End Namespace