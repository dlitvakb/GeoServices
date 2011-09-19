Imports System.Xml

Namespace XML
    Class XMLLoggerGetter
        Inherits XMLGetter

        Shared Function GetName() As String
            Return getSingleXMLElement("application").GetAttribute("name")
        End Function
    End Class
End Namespace
