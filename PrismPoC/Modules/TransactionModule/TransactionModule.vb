Imports PrismPOC.Infrastructure
Imports Microsoft.Practices.Prism.Modularity
Imports Microsoft.Practices.Prism.Regions
Imports Microsoft.Practices.Prism.Events
Imports Microsoft.Practices.Unity

Public Class TransactionModule
    Implements IModule
    ReadOnly _regionManager As IRegionManager
    Private _eventAggregator As IEventAggregator
    Private _container As IUnityContainer


    Public Sub New(container As IUnityContainer, regionManager As IRegionManager, eventAggregator As IEventAggregator)
        _container = container
        _eventAggregator = eventAggregator
        _regionManager = regionManager
        'TODO: Move event handler (AddNewView) to an external class so module can be destroyed as intended by Prism.
        _eventAggregator.GetEvent(Of TransactionButtonEvent)().Subscribe(AddressOf AddNewView, True)
    End Sub

#Region "IModule Members"

    Public Sub Initialize() Implements IModule.Initialize

        _container.RegisterType(Of TransactionViewModel)()
        _container.RegisterType(Of TransactionView)()

        Dim region As IRegion = _regionManager.Regions(RegionNames.DockingAreaRegion)
        If region Is Nothing Then
            Return
        End If

        Dim view As TransactionView = _container.Resolve(Of TransactionView)()
        region.Add(view)

    End Sub

#End Region

    Private Sub AddNewView(ByVal name As String)
        If name = "Transaction" Then
            Dim region As IRegion = _regionManager.Regions(RegionNames.DockingAreaRegion)
            If region Is Nothing Then
                Return
            End If

            Dim view As TransactionView = _container.Resolve(Of TransactionView)()
            region.Add(view)
        End If
    End Sub

End Class
