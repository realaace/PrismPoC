Imports Microsoft.Practices.Prism.Events
Imports Microsoft.Practices.Prism.Commands
Imports PrismPOC.Infrastructure

Public Class ToolbarViewModel

    Private _eventAggregator As IEventAggregator

    Public Property View As ToolbarView
    Public Property FacebookCommand As DelegateCommand

    Public Sub New(ByVal tbview As ToolbarView, ByVal eventAggregator As IEventAggregator)
        View = tbview
        View.ViewModel = Me
        _eventAggregator = eventAggregator
        FacebookCommand = New DelegateCommand(AddressOf Me.RunFacebook, AddressOf Me.CanRunFacebook)
    End Sub

    Private Sub RunFacebook()
        _eventAggregator.GetEvent(Of FacebookButtonEvent)().Publish("Sorry, blocked by Invesco.")
    End Sub

    Private Function CanRunFacebook() As Boolean
        Return True
    End Function

End Class
