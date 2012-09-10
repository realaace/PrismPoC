Imports Microsoft.Practices.Prism.Events
Imports Microsoft.Practices.Prism.Commands
Imports PrismPOC.Infrastructure
Imports BLL

Public Class TransactionViewModel
    Inherits ViewModelBase

    Private _eventAggregator As IEventAggregator
    Private _transactions As List(Of Transaction)
    Private _selectedTransaction As Transaction

    Public Property SelectedTransaction As Transaction
        Get
            Return _selectedTransaction
        End Get
        Set(value As Transaction)
            _selectedTransaction = value
            OnPropertyChanged("SelectedTransaction")
            Dim selected(1) As Object
            selected(0) = Me.GetHashCode()
            selected(1) = _selectedTransaction
            _eventAggregator.GetEvent(Of SelectedItemChangedEvent)().Publish(selected)
        End Set
    End Property

    Public Property Transactions As List(Of Transaction)
        Get
            Return _transactions
        End Get
        Set(value As List(Of Transaction))
            _transactions = value
        End Set
    End Property

    Public Sub New(ByVal eventAggregator As IEventAggregator)
        _eventAggregator = eventAggregator
        _transactions = Transaction.GetTransactions()
    End Sub

End Class
