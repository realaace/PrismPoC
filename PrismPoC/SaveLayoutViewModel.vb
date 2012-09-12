Imports Microsoft.Practices.Prism.Events
Imports Microsoft.Practices.Prism.Commands
Imports PrismPOC.Infrastructure
Imports Xceed.Wpf.Toolkit

Public Class SaveLayoutViewModel
    Inherits ViewModelBase

    Private _eventAggregator As IEventAggregator
    Private _layoutName As String

    Public Property OpenSaveLayoutDialogCommand As DelegateCommand
    Public Property SaveLayoutCommand As DelegateCommand

    Public Property LayoutName As String
        Get
            Return _layoutName
        End Get
        Set(value As String)
            _layoutName = value
            OnPropertyChanged("LayoutName")
            SaveLayoutCommand.RaiseCanExecuteChanged()
        End Set
    End Property

    Public Sub New(ByVal eventAggregator As IEventAggregator)
        _eventAggregator = eventAggregator
        SaveLayoutCommand = New DelegateCommand(AddressOf Me.SaveLayout, AddressOf Me.CanSaveLayout)
    End Sub

    Private Sub SaveLayout()
        _eventAggregator.GetEvent(Of SaveLayoutEvent).Publish(LayoutName)
    End Sub

    Private Function CanSaveLayout() As Boolean
        Return Not String.IsNullOrEmpty(LayoutName)
    End Function

End Class
