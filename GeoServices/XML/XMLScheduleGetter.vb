Imports System.Xml

Namespace XML
    ''' <summary>
    ''' Obtiene el itinerario del archivo Config.xml
    ''' </summary>
    ''' <remarks></remarks>
    Public Class XMLScheduleGetter
        Inherits XMLGetter

        ''' <summary>
        ''' Obtiene una lista de horarios para los cuales está configurado el servicio
        ''' </summary>
        ''' <returns>Lista de horarios en formato HH:mm</returns>
        ''' <remarks></remarks>
        Public Shared Function getSchedule() As List(Of String)
            Dim schedule As New List(Of String)

            Try
                For Each nodo As XmlElement In getSingleXMLElement("schedule").GetElementsByTagName("time")
                    schedule.Add(nodo.GetAttribute("start"))
                Next
            Catch ex As Exception
                Logger.Logger.Error(ex)
            End Try

            Return schedule
        End Function
    End Class
End Namespace