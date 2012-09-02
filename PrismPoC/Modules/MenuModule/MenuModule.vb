﻿Imports Microsoft.Practices.Prism.Modularity
Imports Microsoft.Practices.Unity
Imports Microsoft.Practices.Prism.Regions

Public Class MenuModule
    Implements IModule

    Private _container As IUnityContainer
    Private _regionManager As IRegionManager

    Public Sub New(ByVal container As IUnityContainer, regionManager As IRegionManager)
        _container = container
        _regionManager = regionManager
    End Sub


    Public Sub Initialize() Implements Microsoft.Practices.Prism.Modularity.IModule.Initialize

        Dim region As IRegion = _regionManager.Regions("MenuRegion")
        region.Add(_container.Resolve(Of MenuView))

    End Sub
End Class