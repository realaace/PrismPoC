Public Class ToolbarView

    Public Property ViewModel As ToolbarViewModel
        Get
            Return CType(DataContext, ToolbarViewModel)
        End Get
        Set(value As ToolbarViewModel)
            DataContext = value
        End Set
    End Property

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub


End Class
