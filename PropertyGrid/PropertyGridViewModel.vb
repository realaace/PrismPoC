Imports Microsoft.Practices.Prism.Events
Imports Microsoft.Practices.Prism.Commands
Imports PrismPOC.Infrastructure
Imports BLL

Public Class PropertyGridViewModel
    Inherits ViewModelBase

    Private _eventAggregator As IEventAggregator
    Private _selectedObject As Object
    Private _parentHashCode As Integer = 0

    Public Property SelectedObject As Object
        Get
            Return _selectedObject
        End Get
        Set(value As Object)
            _selectedObject = value
            OnPropertyChanged("SelectedObject")
        End Set
    End Property

    Public Property ParentHashCode As Integer
        Get
            Return _parentHashCode
        End Get
        Set(value As Integer)
            _parentHashCode = value
        End Set
    End Property

    Public Sub New(ByVal eventAggregator As IEventAggregator)
        _eventAggregator = eventAggregator
    End Sub

End Class
