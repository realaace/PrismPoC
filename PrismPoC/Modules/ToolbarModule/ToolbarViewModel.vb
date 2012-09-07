Imports Microsoft.Practices.Prism.Events
Imports Microsoft.Practices.Prism.Commands
Imports PrismPOC.Infrastructure

Public Class ToolbarViewModel

    Private _eventAggregator As IEventAggregator

    Public Property View As ToolbarView
    Public Property AddViewCommand As DelegateCommand(Of String)

    Public Sub New(ByVal tbview As ToolbarView, ByVal eventAggregator As IEventAggregator)
        View = tbview
        View.ViewModel = Me
        _eventAggregator = eventAggregator
        AddViewCommand = New DelegateCommand(Of String)(AddressOf Me.AddView, AddressOf Me.CanAddView)
    End Sub

    Private Sub AddView(viewType As String)
        _eventAggregator.GetEvent(Of TransactionButtonEvent)().Publish(viewType)
    End Sub

    Private Function CanAddView() As Boolean
        Return True
    End Function

End Class
