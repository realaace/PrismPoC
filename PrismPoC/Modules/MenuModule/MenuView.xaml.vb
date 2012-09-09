Imports Microsoft.Practices.Prism.Events
Imports PrismPOC.Infrastructure

Partial Public Class MenuView
    Implements IView

    Public Property ViewModel As IViewModel Implements IView.ViewModel

    Private _eventAggregator As IEventAggregator
    Public Sub New(vm As IViewModel, eventAggregator As IEventAggregator)
        InitializeComponent()
        _eventAggregator = eventAggregator
        ViewModel = vm
        DataContext = ViewModel
    End Sub
    Private Sub SaveLayout_Click(sender As System.Object, e As System.Windows.RoutedEventArgs)
        Dim win As Window = New SaveLayoutWindow(_eventAggregator)
        win.ShowDialog()

    End Sub

    Private Sub LoadLayout_Click(sender As System.Object, e As System.Windows.RoutedEventArgs)
        _eventAggregator.GetEvent(Of LoadLayoutEvent)().Publish("Load Layout")
    End Sub

End Class
