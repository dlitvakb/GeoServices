Imports ESRI.ArcGIS.Geodatabase

Public Class EjemploWorkingComponent
    ''Extiendo de WorkingComponent
    Inherits GeoServices.Worker.WorkingComponent

    Protected Overrides Sub doExecute()
        '' Aca va el flujo de ejecución del programa

        '' Por ejemplo... Listar los nombres de los Feature Classes en el SDE
        Me.ListarNombresFeatureClasses()
    End Sub

    Private Sub ListarNombresFeatureClasses()
        '' Por cada Feature Class en el SDE
        For Each pFClass As IFeatureClass In New GeoServices.XML.XMLGeoGetter().GetFeatureClasses()
            '' Guardo su nombre en el log de salida
            GeoServices.Logger.Logger.Info(pFClass.AliasName)
        Next
    End Sub
End Class
