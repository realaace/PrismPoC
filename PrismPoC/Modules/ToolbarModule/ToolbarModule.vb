Imports Microsoft.Practices.Prism.Modularity
Imports Microsoft.Practices.Unity
Imports Microsoft.Practices.Prism.Regions

Public Class ToolbarModule
    Implements IModule

    Private _container As IUnityContainer
    Private _regionManager As IRegionManager

    Public Sub New(ByVal container As IUnityContainer, regionManager As IRegionManager)
        _container = container
        _regionManager = regionManager
    End Sub


    Public Sub Initialize() Implements Microsoft.Practices.Prism.Modularity.IModule.Initialize

        _container.RegisterType(Of ToolbarViewModel)()
        _container.RegisterType(Of ToolbarView)()

        Dim tbvm = _container.Resolve(Of ToolbarViewModel)()
        Dim toolbarregion As IRegion = _regionManager.Regions("ToolbarRegion")
        toolbarregion.Add(tbvm.View)

    End Sub
End Class
