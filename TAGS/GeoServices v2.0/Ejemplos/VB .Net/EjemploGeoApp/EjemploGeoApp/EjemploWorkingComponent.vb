Imports ESRI.ArcGIS.Geodatabase

Class EjemploWorkingComponent
    '' Extiendo de WorkingComponent
    Inherits GeoServices.Worker.WorkingComponent

    Protected Overrides Sub doExecute()
        '' Aca va el flujo de ejecución

        '' Muestro todos los Feature Class por pantalla
        Me.ListarFeatureClasses()
    End Sub

    Private Sub ListarFeatureClasses()
        '' Por cada Feature Class en el SDE
        For Each pFClass As IFeatureClass In New GeoServices.SDE.FeatureClassesGateway().GetAll()
            '' Muestro el nombre por pantalla
            Console.WriteLine(pFClass.AliasName)
        Next
    End Sub

End Class
