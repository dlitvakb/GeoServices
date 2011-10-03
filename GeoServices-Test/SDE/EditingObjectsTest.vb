Imports System.Text

<TestClass()>
Public Class EditingObjectsTest

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
    Public Sub DatasetVersionado()
        Dim editWksp As New GeoServices.SDE.EditingDataset(New GeoServices.SDE.FeatureClassesGateway().GetByName("FAC_ACU_TRAMOS"))
        Assert.IsTrue(editWksp.isVersioned)
    End Sub

    <TestMethod()>
    Public Sub DatasetNoVersionado()
        Dim editWksp As New GeoServices.SDE.EditingDataset(New GeoServices.SDE.TableGateway().GetByName("TBINST_ENCABEZADO_GEOGRAFICO"))
        Assert.IsFalse(editWksp.isVersioned)
    End Sub

    <TestMethod()>
    Public Sub WorkspaceVersionado()
        Dim editWksp As New GeoServices.SDE.EditingWorkspace(New GeoServices.XML.XMLWorkspaceGetter().GetSingleWorkspace())
        Assert.IsTrue(editWksp.isVersioned)
    End Sub

    <ClassCleanup()> Public Shared Sub MyClassCleanup()
        GeoServices.License.LicenseInitializer.ReleaseLicense()
    End Sub

End Class
