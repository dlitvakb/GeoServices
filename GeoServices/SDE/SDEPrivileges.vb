Namespace SDE
    ''' <summary>
    ''' Valores de posibles permisos en los objetos SDE.
    ''' SDEEdit es la combinacion de todos los permisos
    ''' </summary>
    Public Enum SDEPrivileges
        SDESelect = 1
        SDEUpdate = 2
        SDEInsert = 4
        SDEDelete = 8
        SDEEdit = 15
    End Enum
End Namespace