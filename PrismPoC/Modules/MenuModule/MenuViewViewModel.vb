Imports PrismPOC.Infrastructure
Imports Microsoft.Practices.Prism.Events
Imports Microsoft.Practices.Prism.Commands
Imports Xceed.Wpf.Toolkit

Public Class MenuViewViewModel
    Inherits ViewModelBase
    Private _eventAggregator As IEventAggregator
    Private _layoutName As String
    Private _saveLayoutVisible As WindowState = WindowState.Closed

    Public Property SaveLayoutVisible As WindowState
        Get
            Return _saveLayoutVisible
        End Get
        Set(value As WindowState)
            _saveLayoutVisible = value
            OnPropertyChanged("SaveLayoutVisible")
        End Set
    End Property

    Public Property ExitCommand As DelegateCommand
    Public Property OpenSaveLayoutDialogCommand As DelegateCommand(Of String)
    Public Property SaveLayoutCommand As DelegateCommand(Of String)

    Public Property LayoutName As String
        Get
            Return _layoutName
        End Get
        Set(value As String)
            _layoutName = value
            OnPropertyChanged("LayoutName")
        End Set
    End Property

    Public Sub New(ByVal eventAggregator As IEventAggregator)
        _eventAggregator = eventAggregator
        ExitCommand = New DelegateCommand(AddressOf Me.ExitApp, AddressOf Me.CanExitApp)
        OpenSaveLayoutDialogCommand = New DelegateCommand(Of String)(AddressOf Me.OpenSaveLayoutDialog, AddressOf Me.CanOpenSaveLayoutDialog)
        SaveLayoutCommand = New DelegateCommand(Of String)(AddressOf Me.SaveLayout, AddressOf Me.CanSaveLayout)
    End Sub

    Private Sub ExitApp()
        _eventAggregator.GetEvent(Of ExitAppEvent)().Publish("Exit")
    End Sub

    Private Function CanExitApp() As Boolean
        Return True
    End Function

    Private Sub OpenSaveLayoutDialog(ByVal viewType As String)
        _eventAggregator.GetEvent(Of TransactionButtonEvent)().Publish(viewType)
    End Sub

    Private Function CanOpenSaveLayoutDialog() As Boolean
        Return True
    End Function

    Private Sub SaveLayout()
        MessageBox.Show("Layout Saved.")
    End Sub

    Private Function CanSaveLayout() As Boolean
        Return Not String.IsNullOrEmpty(_layoutName)
    End Function
End Class
