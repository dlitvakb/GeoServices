Imports System.Text
Imports ESRI.ArcGIS.Geodatabase

<TestClass()>
Public Class FeatureClassesGatewayTest

    <ClassInitialize()> Public Shared Sub MyClassInitialize(ByVal testContext As TestContext)
        GeoServices.License.LicenseInitializer.InitializeLicense()
    End Sub

    <TestMethod()>
    Public Sub ObtenerFeatureClasses()
        Assert.IsTrue(New GeoServices.SDE.FeatureClassesGateway().GetAll().Count > 1)
    End Sub

    <TestMethod()>
    Public Sub ObtenerFeatureClassPorNombre()
        Dim FClassTest = New GeoServices.SDE.FeatureClassesGateway().GetAll()(0)
        Assert.IsTrue(New GeoServices.SDE.FeatureClassesGateway().GetByName(FClassTest.AliasName).AliasName = FClassTest.AliasName)
    End Sub

    <TestMethod()>
    Public Sub FeatureClassPorNombreNoEncontrado()
        Try
            Dim fclass As IFeatureClass = New GeoServices.SDE.FeatureClassesGateway().GetByName("Cualquier_Cosa")
            Assert.Fail()
        Catch ex As DataException
            Assert.IsTrue(ex.Message = "El/La Feature Class " & "Cualquier_Cosa" & " no se ha encontrado")
        End Try
    End Sub

    <TestMethod()>
    Public Sub ObtenerFeatureClassPorListaDeNombres()
        Assert.IsTrue(Not New GeoServices.SDE.FeatureClassesGateway().GetByNameList({"FAC_ACU_TRAMOS"})(0) Is Nothing)
    End Sub

    <TestMethod()>
    Public Sub FeatureClassPorListaDeNombresNoEncontrado()
        Try
            Dim fclass As IFeatureClass = New GeoServices.SDE.FeatureClassesGateway().GetByNameList({"Cualquier_Cosa"})(0)
            Assert.Fail()
        Catch ex As DataException
            Assert.IsTrue(ex.Message = "No se encontraron Feature Classes para los nombres especificados")
        End Try
    End Sub

    <TestMethod()>
    Public Sub FeatureClassPorListaDeNombresYEncuentroSoloAlgunas()
        Assert.IsTrue(New GeoServices.SDE.FeatureClassesGateway().GetByNameList({"Cualquier_Cosa", "FAC_ACU_TRAMOS"}, GetResultsAnyway:=True).Count = 1)
    End Sub

    <ClassCleanup()> Public Shared Sub MyClassCleanup()
        GeoServices.License.LicenseInitializer.ReleaseLicense()
    End Sub
End Class
