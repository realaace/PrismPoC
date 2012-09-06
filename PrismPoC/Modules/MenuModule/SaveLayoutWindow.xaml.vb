Imports Microsoft.Practices.Prism.Events
Imports Microsoft.Practices.Prism.Commands
Imports PrismPOC.Infrastructure

Public Class SaveLayoutWindow

    Private _eventAggregator As IEventAggregator

    Public Sub New(eventAggregator As IEventAggregator)
        InitializeComponent()
        _eventAggregator = eventAggregator
    End Sub
    Private Sub btnSaveLayout_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles btnSaveLayout.Click
        _eventAggregator.GetEvent(Of SaveLayoutEvent)().Publish(txtLayoutName.Text)
        Me.Close()
    End Sub

    Private Sub txtLayoutName_TextChanged(sender As System.Object, e As System.Windows.Controls.TextChangedEventArgs) Handles txtLayoutName.TextChanged
        btnSaveLayout.IsEnabled = (txtLayoutName.Text <> "")

        'If (txtLayoutName.Text <> "") Then
        '    btnSaveLayout.IsEnabled = True
        'Else
        '    btnSaveLayout.IsEnabled = False
        'End If

    End Sub
End Class
