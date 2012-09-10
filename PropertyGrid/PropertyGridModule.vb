Imports PrismPOC.Infrastructure
Imports Microsoft.Practices.Prism.Modularity
Imports Microsoft.Practices.Prism.Regions
Imports Microsoft.Practices.Prism.Events
Imports Microsoft.Practices.Unity

Public Class PropertyGridModule
    Implements IModule
    ReadOnly _regionManager As IRegionManager
    Private _eventAggregator As IEventAggregator
    Private _container As IUnityContainer


    Public Sub New(container As IUnityContainer, regionManager As IRegionManager, eventAggregator As IEventAggregator)
        _container = container
        _eventAggregator = eventAggregator
        _regionManager = regionManager
    End Sub

#Region "IModule Members"

    Public Sub Initialize() Implements IModule.Initialize

        _container.RegisterType(Of PropertyGridViewModel)()
        _container.RegisterType(Of PropertyGridView)()
        _container.RegisterType(Of PropertyGridView2)()

        Dim region As IRegion = _regionManager.Regions(RegionNames.DockingAreaRegion)
        If region Is Nothing Then
            Return
        End If

        Dim view As PropertyGridView2 = _container.Resolve(Of PropertyGridView2)()
        region.Add(view)

        'Dim view2 As PropertyGridView2 = _container.Resolve(Of PropertyGridView2)()
        'region.Add(view2)

    End Sub

#End Region

End Class
