Module EjemploGeoApp

    Sub Main()
        Try
            '' Inicializo el Logger
            GeoServices.Logger.Logger.initialize()

            '' Puedo usar el Logger en cualquier lugar una vez inicializado
            GeoServices.Logger.Logger.Info("Empiezo a listar Feature Classes")

            '' Hago una llamada a un WorkingComponent
            Dim worker As GeoServices.Worker.WorkingComponent = New EjemploWorkingComponent()
            worker.Execute()

            '' Puedo usar el Logger en cualquier lugar una vez inicializado
            GeoServices.Logger.Logger.Info("Terminé =)")

                Console.ReadKey()
            '' Imito la estructura de captura de excepciones de la plantilla ServiceHandler
        Catch ex As UnauthorizedAccessException
            GeoServices.Logger.Logger.Warn(ex)
        Catch ex As Exception
            GeoServices.Logger.Logger.Error(ex)
        End Try
    End Sub

End Module
