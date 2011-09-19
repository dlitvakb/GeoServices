Imports System.Xml

Namespace XML
    Public MustInherit Class XMLGetter
        ''' <summary>
        ''' Provee acceso al archivo Config.xml de forma transparente
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Shared Function getXml() As XmlDocument
            Dim xml As New XmlDocument
            xml.Load(System.AppDomain.CurrentDomain.BaseDirectory & "\Config.xml")
            Return xml
        End Function

        ''' <summary>
        ''' Provee acceso al primer elemento que tenga el nombre especificado
        ''' </summary>
        ''' <param name="tagName"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <exception cref="XmlException">Si no existe el elemento arroja excepción</exception>
        Protected Shared Function getSingleXMLElement(ByVal tagName As String, Optional ByVal index As Integer = 0) As XmlElement
            Return getXml().GetElementsByTagName(tagName)(index)
        End Function

        ''' <summary>
        ''' Provee acceso a todos los elementos que tengan el nombre especificado
        ''' </summary>
        ''' <param name="tagName"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Shared Function getElements(ByVal tagName As String) As XmlNodeList
            Return getXml().GetElementsByTagName(tagName)
        End Function
    End Class
End Namespace