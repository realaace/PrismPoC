Partial Public Class TransactionView
    Inherits UserControl

    Public Property ViewModel As TransactionViewModel
        Get
            Return CType(DataContext, TransactionViewModel)
        End Get
        Set(value As TransactionViewModel)
            DataContext = value
        End Set
    End Property

    Public ReadOnly Property IsDocumentContent
        Get
            Return True
        End Get
    End Property

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

End Class
