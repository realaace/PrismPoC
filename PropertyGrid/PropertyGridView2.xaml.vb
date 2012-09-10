Imports Microsoft.Practices.Unity

Public Class PropertyGridView2
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

    Public Sub New(container As IUnityContainer)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _viewModel = container.Resolve(Of PropertyGridViewModel)()
        Me.DataContext = _viewModel
    End Sub
End Class
