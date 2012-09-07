Imports PrismPOC.Infrastructure
Imports Microsoft.Practices.Prism.Modularity
Imports Microsoft.Practices.Prism.Regions
Imports Microsoft.Practices.Prism.Events

Public Class TransactionModule
    Implements IModule
    ReadOnly _regionManager As IRegionManager
    Private _eventAggregator As IEventAggregator

    Public Sub New(regionManager As IRegionManager, eventAggregator As IEventAggregator)
        _eventAggregator = eventAggregator
        _regionManager = regionManager
        'TODO: Move event handler (AddNewView) to an external class so module can be destroyed as intended by Prism.
        _eventAggregator.GetEvent(Of TransactionButtonEvent)().Subscribe(AddressOf AddNewView, True)
    End Sub

#Region "IModule Members"

    Public Sub Initialize() Implements IModule.Initialize

        Dim region As IRegion = _regionManager.Regions(RegionNames.DockingAreaRegion)
        If region Is Nothing Then
            Return
        End If

        Dim view As New TransactionView()
        region.Add(view)

        Dim view2 As New TransactionView()
        region.Add(view2)

        Dim view3 As New TransactionView()
        region.Add(view3)

    End Sub

#End Region

    Private Sub AddNewView(ByVal name As String)
        If name = "Transaction" Then
            Dim region As IRegion = _regionManager.Regions(RegionNames.DockingAreaRegion)
            If region Is Nothing Then
                Return
            End If

            Dim view As New TransactionView()
            region.Add(view)
        End If
    End Sub

End Class
