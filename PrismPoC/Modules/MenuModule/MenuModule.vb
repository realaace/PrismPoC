Imports Microsoft.Practices.Prism.Modularity
Imports Microsoft.Practices.Unity
Imports Microsoft.Practices.Prism.Regions
Imports PrismPOC.Infrastructure

Public Class MenuModule
    Implements IModule

    Private _container As IUnityContainer
    Private _regionManager As IRegionManager

    Public Sub New(ByVal container As IUnityContainer, regionManager As IRegionManager)
        _container = container
        _regionManager = regionManager
    End Sub


    Public Sub Initialize() Implements Microsoft.Practices.Prism.Modularity.IModule.Initialize

        _container.RegisterType(Of IViewModel, MenuViewViewModel)()
        _container.RegisterType(Of MenuView)()
        Dim region As IRegion = _regionManager.Regions("MenuRegion")
        region.Add(_container.Resolve(Of MenuView))

    End Sub
End Class