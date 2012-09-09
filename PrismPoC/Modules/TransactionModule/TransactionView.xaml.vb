Imports Microsoft.Practices.Unity

Partial Public Class TransactionView
    Inherits UserControl

    Private _viewModel As TransactionViewModel

    Public Property ViewModel As TransactionViewModel
        Get
            Return _viewModel
        End Get
        Set(value As TransactionViewModel)
            _viewModel = value
        End Set
    End Property

    Public ReadOnly Property IsDocumentContent
        Get
            Return True
        End Get
    End Property

    Public Sub New(container As IUnityContainer)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _viewModel = container.Resolve(Of TransactionViewModel)()
        Me.DataContext = _viewModel
    End Sub

    Private Sub xdgTransactions_SelectedItemsChanged_1(sender As Object, e As Infragistics.Windows.DataPresenter.Events.SelectedItemsChangedEventArgs)

    End Sub

    Private Sub xdgTransactions_MouseDoubleClick_1(sender As Object, e As MouseButtonEventArgs)
        MessageBox.Show(String.Format("Double clicked on {0}", sender.ToString))
    End Sub
End Class
