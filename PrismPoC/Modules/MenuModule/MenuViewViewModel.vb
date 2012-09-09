Imports PrismPOC.Infrastructure
Imports Microsoft.Practices.Prism.Events
Imports Microsoft.Practices.Prism.Commands

Public Class MenuViewViewModel
    Inherits ViewModelBase
    Private _eventAggregator As IEventAggregator

    Public Property ExitCommand As DelegateCommand

    Public Sub New(ByVal eventAggregator As IEventAggregator)
        _eventAggregator = eventAggregator
        ExitCommand = New DelegateCommand(AddressOf Me.ExitApp, AddressOf Me.CanExitApp)
    End Sub

    Private Sub ExitApp()
        _eventAggregator.GetEvent(Of ExitAppEvent)().Publish("Exit")
    End Sub

    Private Function CanExitApp() As Boolean
        Return True
    End Function

End Class
