Public Class EjemploGeoService
    ''Extiendo de ServiceHandler
    Inherits GeoServices.ServiceHandler

    Protected Overrides Sub doWork()
        '' Acá va el codigo de las llamadas a  WorkingComponents

        '' En este caso, como la llamada es a un solo WorkingComponent 
        '' no hace falta crear nuevos hilos de ejecución
        Dim worker As GeoServices.Worker.WorkingComponent = New EjemploWorkingComponent
        worker.Execute()
    End Sub
End Class
