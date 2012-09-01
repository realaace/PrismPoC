Public Class XamDockManagerView

    Public Property ViewModel As XamDockManagerViewModel
        Get
            Return CType(DataContext, XamDockManagerViewModel)
        End Get
        Set(value As XamDockManagerViewModel)
            DataContext = value
        End Set
    End Property

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

End Class
