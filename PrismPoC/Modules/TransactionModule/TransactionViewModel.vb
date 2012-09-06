Imports Microsoft.Practices.Prism.Events
Imports Microsoft.Practices.Prism.Commands
Imports PrismPOC.Infrastructure

Public Class TransactionViewModel

    Private _eventAggregator As IEventAggregator

    Public Property View As TransactionView
    Public Property FacebookCommand As DelegateCommand

    Public Sub New(ByVal tbview As TransactionView, ByVal eventAggregator As IEventAggregator)
        View = tbview
        View.ViewModel = Me
        _eventAggregator = eventAggregator
    End Sub

End Class
