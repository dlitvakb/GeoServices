Imports System.Text

<TestClass()>
Public Class XMLScheduleGetterTest

    <TestMethod()>
    Public Sub ObtengoHorariosDeEjecucion()
        Assert.IsTrue(GeoServices.XML.XMLScheduleGetter.getSchedule()(0) = "12:00")
        Assert.IsTrue(GeoServices.XML.XMLScheduleGetter.getSchedule()(1) = "00:00")
    End Sub

End Class
