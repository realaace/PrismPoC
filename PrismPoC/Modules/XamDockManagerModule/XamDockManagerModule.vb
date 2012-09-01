Imports Microsoft.Practices.Prism.Modularity
Imports Microsoft.Practices.Unity
Imports Microsoft.Practices.Prism.Regions

Public Class XamDockManagerModule
    Implements IModule

    Private _container As IUnityContainer
    Private _regionManager As IRegionManager

    Public Sub New(ByVal container As IUnityContainer, regionManager As IRegionManager)
        _container = container
        _regionManager = regionManager
    End Sub


    Public Sub Initialize() Implements Microsoft.Practices.Prism.Modularity.IModule.Initialize

        _container.RegisterType(Of XamDockManagerViewModel)()
        _container.RegisterType(Of XamDockManagerView)()

        Dim xdmvm = _container.Resolve(Of XamDockManagerViewModel)()
        Dim dmregion As IRegion = _regionManager.Regions("XamDockManagerRegion")
        dmregion.Add(xdmvm.View)

    End Sub
End Class
