Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.esriSystem

Namespace SDE
    Public Class PrivilegesValidator
        Private Property DatasetName As IName
        Private Const SELECT_VAL As Integer = 1
        Private Const UPDATE_VAL As Integer = 2
        Private Const INSERT_VAL As Integer = 4
        Private Const DELETE_VAL As Integer = 8

        Public Sub New(ByVal dataset As IDataset)
            Me.DatasetName = dataset.FullName
        End Sub

        Protected Function GetPrivileges() As Integer
            Return CType(DatasetName, ISQLPrivilege).SQLPrivileges
        End Function

        Public Function CanEdit() As Boolean
            Return Me.CanSelect() AndAlso Me.CanInsert() AndAlso Me.CanUpdate() AndAlso Me.CanDelete()
        End Function

        Public Function CanSelect() As Boolean
            Return Me.CanDoAction({SELECT_VAL, SELECT_VAL + INSERT_VAL, _
                                   SELECT_VAL + UPDATE_VAL, SELECT_VAL + INSERT_VAL + UPDATE_VAL, _
                                   SELECT_VAL + DELETE_VAL, SELECT_VAL + INSERT_VAL + DELETE_VAL, _
                                   SELECT_VAL + UPDATE_VAL + DELETE_VAL, SELECT_VAL + INSERT_VAL + UPDATE_VAL + DELETE_VAL})
        End Function

        Public Function CanInsert() As Boolean
            Return Me.CanDoAction({INSERT_VAL, INSERT_VAL + SELECT_VAL, _
                                   INSERT_VAL + UPDATE_VAL, INSERT_VAL + UPDATE_VAL + SELECT_VAL, _
                                   INSERT_VAL + DELETE_VAL, INSERT_VAL + SELECT_VAL + DELETE_VAL, _
                                   INSERT_VAL + UPDATE_VAL + DELETE_VAL, INSERT_VAL + SELECT_VAL + UPDATE_VAL + DELETE_VAL})
        End Function

        Public Function CanUpdate() As Boolean
            Return Me.CanDoAction({UPDATE_VAL, UPDATE_VAL + SELECT_VAL, _
                                   UPDATE_VAL + INSERT_VAL, UPDATE_VAL + SELECT_VAL + INSERT_VAL, _
                                   UPDATE_VAL + DELETE_VAL, UPDATE_VAL + SELECT_VAL + DELETE_VAL, _
                                   UPDATE_VAL + INSERT_VAL + DELETE_VAL, UPDATE_VAL + SELECT_VAL + INSERT_VAL + DELETE_VAL})
        End Function

        Public Function CanDelete() As Boolean
            Return Me.CanDoAction({DELETE_VAL, DELETE_VAL + SELECT_VAL, _
                                   DELETE_VAL + INSERT_VAL, DELETE_VAL + UPDATE_VAL, _
                                   DELETE_VAL + SELECT_VAL + INSERT_VAL, DELETE_VAL + UPDATE_VAL + INSERT_VAL,
                                   DELETE_VAL + SELECT_VAL + UPDATE_VAL, DELETE_VAL + SELECT_VAL + INSERT_VAL + UPDATE_VAL})
        End Function

        Protected Function CanDoAction(ByVal possiblePrivilegeValues As Integer()) As Boolean
            Return possiblePrivilegeValues.Contains(Me.GetPrivileges)
        End Function
    End Class
End Namespace
