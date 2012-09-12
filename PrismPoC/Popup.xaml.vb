Imports Microsoft.Practices.Prism.Regions
Imports PrismPoC.Infrastructure

Partial Public Class Popup

    Private _regionManager As IRegionManager
    Public Sub New(rm As IRegionManager)
        InitializeComponent()
        _regionManager = rm
        RegionManager.SetRegionManager(Me, rm)
        'RegionManager.UpdateRegions()
    End Sub

    Private Sub Popup_Closing(sender As Object, e As ComponentModel.CancelEventArgs) Handles Me.Closing
        _regionManager.Regions.Remove(RegionNames.PopupRegion)
    End Sub
End Class
