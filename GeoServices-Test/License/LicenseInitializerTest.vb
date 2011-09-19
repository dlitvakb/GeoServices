Imports System.Text
Imports ESRI.ArcGIS.Geodatabase

<TestClass()>
Public Class LicenseInitializerTest

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

    <TestMethod()>
    Public Sub NoInicializoLicenciaTiraError()
        Try
            Dim fclass As IFeatureClass = New GeoServices.SDE.FeatureClassesGateway().GetAll()(0)
            Assert.Fail()
        Catch ex As Exception
            Assert.IsTrue(True)
        End Try
    End Sub

    <TestMethod()>
    Public Sub InicializoLicenciaEstaTodoOK()
        Try
            GeoServices.License.LicenseInitializer.InitializeLicense()
            Dim fclass As IFeatureClass = New GeoServices.SDE.FeatureClassesGateway().GetAll()(0)
            GeoServices.License.LicenseInitializer.ReleaseLicense()
            Assert.IsTrue(True)
        Catch ex As Exception
            Assert.Fail()
        End Try
    End Sub
End Class
