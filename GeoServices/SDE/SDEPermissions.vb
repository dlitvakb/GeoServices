Namespace SDE
    ''' <summary>
    ''' Valores de posibles permisos en los objetos SDE, cualquier combinacion de ellos también es válida y se realiza mediante la suma
    ''' (descontando SDEEdit que debe ser utilizado siempre por separado, ya que es la suma de todos)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SDEPermissions
        SDESelect = 1
        SDEUpdate = 2
        SDEInsert = 4
        SDEDelete = 8
        SDEEdit = 15
    End Enum
End Namespace