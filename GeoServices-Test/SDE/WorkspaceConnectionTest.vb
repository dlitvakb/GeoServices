Imports System.Text

<TestClass()>
Public Class WorkspaceConnectionTest

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
            testContextInstance = Value
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
    Public Sub LaConexionEsValida()
        Assert.IsTrue(Not New GeoServices.SDE.WorkspaceConnection("sde", "7QOzDseajRSMp1sM1iBPCQ==", "192.168.1.32", "5151", "", "SDE.DEFAULT").GetWorkspace() Is Nothing)
    End Sub

    <TestMethod()>
    Public Sub LaConexionEsinvalida()
        Try
            Assert.Fail(Not New GeoServices.SDE.WorkspaceConnection("sde", "fruta", "192.168.1.32", "5151", "", "SDE.DEFAULT").GetWorkspace() Is Nothing)
        Catch ex As DataException
            If ex.Message = "No se pudo obtener el Workspace para la configuración especificada" Then Assert.IsTrue(True)
        End Try
    End Sub

    <ClassCleanup()> Public Shared Sub MyClassCleanup()
        GeoServices.License.LicenseInitializer.ReleaseLicense()
    End Sub
End Class
