Imports System.Text
Imports ESRI.ArcGIS.Geodatabase

<TestClass()>
Public Class TableGatewayTest

    Private testContextInstance As TestContext

    '''<summary>
    '''Gets or sets the test context which provides
    '''information about and functionality for the current test run.
    '''</summary>
    Public Property TestContext() As TestContext
        Get
            Return testContextInstance
        End Get
        Set(ByVal value As TestContext)
            testContextInstance = value
        End Set
    End Property

#Region "Additional test attributes"
    '
    ' You can use the following additional attributes as you write your tests:
    '
    ' Use ClassInitialize to run code before running the first test in the class
    ' <ClassInitialize()> Public Shared Sub MyClassInitialize(ByVal testContext As TestContext)
    ' End Sub
    '
    ' Use ClassCleanup to run code after all tests in a class have run
    ' <ClassCleanup()> Public Shared Sub MyClassCleanup()
    ' End Sub
    '
    ' Use TestInitialize to run code before running each test
    ' <TestInitialize()> Public Sub MyTestInitialize()
    ' End Sub
    '
    ' Use TestCleanup to run code after each test has run
    ' <TestCleanup()> Public Sub MyTestCleanup()
    ' End Sub
    '
#End Region

    <ClassInitialize()> Public Shared Sub MyClassInitialize(ByVal testContext As TestContext)
        GeoServices.License.LicenseInitializer.InitializeLicense()
    End Sub

    <TestMethod()>
    Public Sub ObtenerUnaTablaPorNombre()
        Assert.IsTrue(CType(New GeoServices.SDE.TableGateway().GetByName("AH_SDE.TBSEG_USUARIOSCONTROL"), IDataset).Name.ToUpper.Contains("TBSEG_USUARIOSCONTROL"))
    End Sub

    <TestMethod()>
    Public Sub UnaTablaNoEncontradaPorNombre()
        Try
            Dim tabla As ITable = New GeoServices.SDE.TableGateway().GetByName("FRUTA.Cualquier_Cosa")
            Assert.Fail()
        Catch ex As DataException
            Assert.IsTrue(ex.Message = "El elemento " & "FRUTA.Cualquier_Cosa" & " no se ha encontrado")
        End Try
    End Sub

    <TestMethod()>
    Public Sub ObtenerTablasPorListaDeNombres()
        Assert.IsTrue(CType(New GeoServices.SDE.TableGateway().GetByNameList({"AH_SDE.TBSEG_USUARIOSCONTROL"})(0), IDataset).Name.ToUpper.Contains("TBSEG_USUARIOSCONTROL"))
    End Sub

    <TestMethod()>
    Public Sub ObtenerTablasPorListaDeNombresYFalloPorqueMandeFruta()
        Try
            Dim tabla As ITable = New GeoServices.SDE.TableGateway().GetByNameList({"FRUTA.Cualquier_Cosa"})
            Assert.Fail()
        Catch ex As DataException
            Assert.IsTrue(ex.Message = "No se encontraron elementos para los nombres especificados")
        End Try
    End Sub

    <TestMethod()>
    Public Sub ObtenerTablasPorListaDeNombresYPermitirVacios()
        Assert.IsTrue(New GeoServices.SDE.TableGateway().GetByNameList({"AH_SDE.TBSEG_USUARIOSCONTROL", "FRUTA.Cualquier_Cosa"}, GetResultsAnyway:=True).Count = 1)
    End Sub

    <TestMethod()>
    Public Sub ObtenerTodasLasTablas()
        Assert.IsTrue(New GeoServices.SDE.TableGateway().GetAll().Count > 0)
    End Sub

    <ClassCleanup()> Public Shared Sub MyClassCleanup()
        GeoServices.License.LicenseInitializer.ReleaseLicense()
    End Sub
End Class
