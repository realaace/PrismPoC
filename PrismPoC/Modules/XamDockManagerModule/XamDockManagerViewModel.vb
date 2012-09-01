Imports Microsoft.Practices.Prism.Events
Imports Microsoft.Practices.Prism.Commands
Imports System.ComponentModel
Imports PrismPOC.Infrastructure

Public Class XamDockManagerViewModel
    Implements INotifyPropertyChanged

    Private _eventAggregator As IEventAggregator
    Private _viewContent As String = ""

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    Protected Sub OnPropertyChanged(propertyName As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
    End Sub

    Public Property View As XamDockManagerView
    Public Property ViewContent As String
        Get
            Return _viewContent
        End Get
        Set(value As String)
            _viewContent = value
            OnPropertyChanged("ViewContent")
        End Set
    End Property

    Public Sub New(ByVal xdmView As XamDockManagerView, ByVal eventAggregator As IEventAggregator)
        View = xdmView
        View.ViewModel = Me
        _eventAggregator = eventAggregator
        _eventAggregator.GetEvent(Of FacebookButtonEvent)().Subscribe(AddressOf FacebookButtonClicked)

    End Sub

    Private Sub FacebookButtonClicked(ByVal text As String)
        ViewContent = text
    End Sub
End Class
