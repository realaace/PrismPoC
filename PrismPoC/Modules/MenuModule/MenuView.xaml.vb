Imports Microsoft.Practices.Prism.Events
Imports PrismPOC.Infrastructure

Public Class MenuView

    Private _eventAggregator As IEventAggregator
    Public Sub New(eventAggregator As IEventAggregator)
        InitializeComponent()
        _eventAggregator = eventAggregator
    End Sub
    Private Sub SaveLayout_Click(sender As System.Object, e As System.Windows.RoutedEventArgs)
        Dim win As Window = New SaveLayoutWindow(_eventAggregator)
        win.ShowDialog()

    End Sub

    Private Sub LoadLayout_Click(sender As System.Object, e As System.Windows.RoutedEventArgs)
        _eventAggregator.GetEvent(Of LoadLayoutEvent)().Publish("Load Layout")
    End Sub

End Class
