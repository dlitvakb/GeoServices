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
        Assert.IsTrue(New GeoServices.SDE.FeatureClassesGateway().GetByName("AH_SDE.FAC_Acu_Tramos").AliasName.Contains("Acueductos"))
    End Sub

    <TestMethod()>
    Public Sub FeatureClassPorNombreNoEncontrado()
        Try
            Dim fclass As IFeatureClass = New GeoServices.SDE.FeatureClassesGateway().GetByName("FRUTA.Cualquier_Cosa")
            Assert.Fail()
        Catch ex As DataException
            Assert.IsTrue(ex.Message = "El elemento " & "FRUTA.Cualquier_Cosa" & " no se ha encontrado")
        End Try
    End Sub

    <TestMethod()>
    Public Sub ObtenerFeatureClassPorListaDeNombres()
        Assert.IsTrue(Not New GeoServices.SDE.FeatureClassesGateway().GetByNameList({"AH_SDE.FAC_Acu_Tramos"})(0) Is Nothing)
    End Sub

    <TestMethod()>
    Public Sub FeatureClassPorListaDeNombresNoEncontrado()
        Try
            Dim fclass As IFeatureClass = New GeoServices.SDE.FeatureClassesGateway().GetByNameList({"FRUTA.Cualquier_Cosa"})(0)
            Assert.Fail()
        Catch ex As DataException
            Assert.IsTrue(ex.Message = "No se encontraron elementos para los nombres especificados")
        End Try
    End Sub

    <TestMethod()>
    Public Sub FeatureClassPorListaDeNombresYEncuentroSoloAlgunas()
        Assert.IsTrue(New GeoServices.SDE.FeatureClassesGateway().GetByNameList({"AH_SDE.Cualquier_Cosa", "AH_SDE.FAC_ACU_TRAMOS"}, GetResultsAnyway:=True).Count = 1)
    End Sub

    <TestMethod()>
    Public Sub FeatureClassPorNombreYConPermisosDeEdicionYFunciona()
        Assert.IsTrue(New GeoServices.SDE.FeatureClassesGateway().GetByName("SDE.PRUEBA_PERMISOS", connectionNumber:=0, Privileges:=GeoServices.SDE.SDEPrivileges.SDEEdit).AliasName.Contains("PRUEBA_PERMISOS"))
    End Sub

    <TestMethod()>
    Public Sub FeatureClassPorNombreYConPermisosDeEdicionYFallaPorqueNoTengoPermisos()
        Try
            Dim debe_fallar As IFeatureClass = New GeoServices.SDE.FeatureClassesGateway().GetByName("SDE.PRUEBA_PERMISOS", connectionNumber:=1, Privileges:=GeoServices.SDE.SDEPrivileges.SDEEdit)
        Catch ex As DataException
            Assert.IsTrue(ex.Message = "No se tienen permisos suficientes para acceder al elemento")
        End Try
    End Sub

    <TestMethod()>
    Public Sub FeatureClassPorNombreYSinPermisosDeEdicionYFunciona()
        Assert.IsTrue(New GeoServices.SDE.FeatureClassesGateway().GetByName("AH_SDE.PPP_VERSIONADO_SINPERMISOS", connectionNumber:=1, Privileges:=GeoServices.SDE.SDEPrivileges.SDESelect).AliasName.Contains("PPP"))
    End Sub

    <TestMethod()>
    Public Sub BuscoEntreTodosLosFeatureClassUnoSinPermisosDeEdicionYNoDeberiaEstar()
        For Each fclass As IFeatureClass In New GeoServices.SDE.FeatureClassesGateway().GetAll(connectionNumber:=1, Privileges:=GeoServices.SDE.SDEPrivileges.SDEEdit)
            If Not New GeoServices.SDE.PrivilegesValidator(fclass).CanEdit Then Assert.Fail()
        Next
        Assert.IsTrue(True)
    End Sub

    <TestMethod()>
    Public Sub BuscoLasCapasDeEncabezadoGeografico()
        Dim fclasses As List(Of IFeatureClass) = New GeoServices.SDE.FeatureClassesGateway().GetByNameList({"AH_SDE.FAC_INSTALACIONES", "AH_SDE.FAC_OLEO_COLECTORES", "AH_SDE.FAC_GAS_COLECTORES", "AH_SDE.FAC_ACU_SATELITES", "AH_SDE.FAC_TANQUES", "AH_SDE.FAC_ELECTRICAS"}, Privileges:=GeoServices.SDE.SDEPrivileges.SDESelect)
        Assert.IsTrue(fclasses.Count = 6)

        fclasses = New GeoServices.SDE.FeatureClassesGateway().GetByNameList({"AH_SDE.ADT_Yacimientos", "AH_SDE.ADT_AreasConcesion", "AH_SDE.GEN_DistritosExplota_SJ", "AH_SDE.GEN_Provincias", "AH_SDE.GEN_Departamentos", "AH_SDE.ADT_AreasConcesion", "AH_SDE.ADT_AreasConcesion"}, Privileges:=GeoServices.SDE.SDEPrivileges.SDESelect)
        Assert.IsTrue(fclasses.Count = 7)
    End Sub

    <ClassCleanup()> Public Shared Sub MyClassCleanup()
        GeoServices.License.LicenseInitializer.ReleaseLicense()
    End Sub
End Class
