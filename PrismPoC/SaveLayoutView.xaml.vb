Imports Microsoft.Practices.Prism.Events
Imports PrismPoC.Infrastructure
Imports Microsoft.Practices.Unity

Partial Public Class SaveLayoutView
    Implements IView

    Public Property ViewModel As IViewModel Implements IView.ViewModel

    Private _eventAggregator As IEventAggregator

    Public Sub New(container As IUnityContainer, eventAggregator As IEventAggregator)
        InitializeComponent()
        _eventAggregator = eventAggregator
        ViewModel = container.Resolve(Of SaveLayoutViewModel)()
        Me.DataContext = ViewModel

    End Sub

    Private Sub txtLayoutName_TextChanged(sender As Object, e As TextChangedEventArgs)
        Dim vm As SaveLayoutViewModel = CType(ViewModel, SaveLayoutViewModel)
        vm.LayoutName = txtLayoutName.Text
    End Sub
End Class
