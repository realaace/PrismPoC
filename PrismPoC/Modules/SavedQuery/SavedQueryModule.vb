Imports PrismPOC.Infrastructure
Imports Microsoft.Practices.Prism.Modularity
Imports Microsoft.Practices.Prism.Regions
Imports Microsoft.Practices.Prism.Events

Public Class SavedQueryModule
    Implements IModule
    ReadOnly _regionManager As IRegionManager
    Private _eventAggregator As IEventAggregator

    Public Sub New(regionManager As IRegionManager, eventAggregator As IEventAggregator)
        _regionManager = regionManager
        _eventAggregator = eventAggregator
        'TODO: Move event handler (AddNewView) to an external class so module can be destroyed as intended by Prism.
        _eventAggregator.GetEvent(Of TransactionButtonEvent)().Subscribe(AddressOf AddNewView, True)

    End Sub

    Private Sub AddNewView(ByVal name As String)
        If name = "SavedQuery" Then
            Dim region As IRegion = _regionManager.Regions(RegionNames.DockingAreaRegion)
            If region Is Nothing Then
                Return
            End If

            Dim view As New SavedQueryView()
            region.Add(view)
        End If
    End Sub

#Region "IModule Members"

    Public Sub Initialize() Implements IModule.Initialize
        Dim region As IRegion = _regionManager.Regions(RegionNames.DockingAreaRegion)
        If region Is Nothing Then
            Return
        End If

        Dim view As New SavedQueryView()
        region.Add(view)

    End Sub

#End Region
End Class
