Imports System.Text

<TestClass()>
Public Class XMLScheduleGetterTest

    <TestMethod()>
    Public Sub ObtengoHorariosDeEjecucion()
        Assert.IsTrue(GeoServices.XML.XMLScheduleGetter.getSchedule()(0) = "12:00")
        Assert.IsTrue(GeoServices.XML.XMLScheduleGetter.getSchedule()(1) = "00:00")
    End Sub

    <TestMethod()>
    Public Sub ToStringDeUnDateTimeConFormatoMeDevuelveEn24hs()
        Assert.IsTrue(New DateTime(2011, 10, 3, 12, 0, 0).ToString("HH:mm") = "12:00")
        Assert.IsTrue(New DateTime(2011, 10, 3, 14, 0, 0).ToString("HH:mm") = "14:00")
        Assert.IsTrue(New DateTime(2011, 10, 3, 8, 0, 0).ToString("HH:mm") = "08:00")
    End Sub

End Class
