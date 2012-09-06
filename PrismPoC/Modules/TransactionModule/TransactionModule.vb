Imports PrismPOC.Infrastructure
Imports Microsoft.Practices.Prism.Modularity
Imports Microsoft.Practices.Prism.Regions

Public Class TransactionModule
    Implements IModule
    ReadOnly _regionManager As IRegionManager

    Public Sub New(regionManager As IRegionManager)
        _regionManager = regionManager
    End Sub

#Region "IModule Members"

    Public Sub Initialize() Implements IModule.Initialize
        Dim region As IRegion = _regionManager.Regions(RegionNames.DockingAreaRegion)
        If region Is Nothing Then
            Return
        End If

        Dim view As New TransactionView()
        region.Add(view)

    End Sub

#End Region
End Class
