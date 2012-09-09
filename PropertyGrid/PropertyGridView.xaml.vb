Imports Microsoft.Practices.Unity

Partial Public Class PropertyGridView
    Inherits UserControl

    Private _viewModel As PropertyGridViewModel

    Public Property ViewModel As PropertyGridViewModel
        Get
            Return _viewModel
        End Get
        Set(value As PropertyGridViewModel)
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
        _viewModel = container.Resolve(Of PropertyGridViewModel)()
        Me.DataContext = _viewModel
    End Sub
End Class
