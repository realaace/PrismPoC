Imports PrismPOC.Infrastructure
Imports Microsoft.Practices.Prism.Modularity
Imports Microsoft.Practices.Prism.Regions

Public Class SavedQueryModule
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

        Dim view As New SavedQueryView()
        region.Add(view)

        Dim view2 As New SavedQueryView()
        region.Add(view2)

        Dim view3 As New SavedQueryView()
        region.Add(view3)
    End Sub

#End Region
End Class
