Imports PrismPOC.Infrastructure
Imports Microsoft.Practices.Prism.Commands
Imports Microsoft.Practices.Prism.Regions
Imports Microsoft.Practices.Unity

Public Class ShellViewModel
    Inherits ViewModelBase
    Private _container As IUnityContainer
    Private ReadOnly _regionManager As IRegionManager
    Dim _popupWindow As PopupWindowProperties

    Public Property PopupWindow() As PopupWindowProperties
        Get
            Return _popupWindow
        End Get
        Set(value As PopupWindowProperties)
            _popupWindow = value
            OnPropertyChanged("PopupWindow")
        End Set
    End Property

    Public Property NavigateCommand() As DelegateCommand(Of Object)
        Get
            Return m_NavigateCommand
        End Get
        Private Set(value As DelegateCommand(Of Object))
            m_NavigateCommand = value
        End Set
    End Property
    Private m_NavigateCommand As DelegateCommand(Of Object)

    Public Sub New(container As IUnityContainer, regionManager As IRegionManager)
        _regionManager = regionManager
        _container = container

        NavigateCommand = New DelegateCommand(Of Object)(AddressOf Navigate)
        ApplicationCommands.NavigateCommand.RegisterCommand(NavigateCommand)
    End Sub

    Private Sub Navigate(navigatePath As Object)
        If navigatePath IsNot Nothing Then
            PopupWindow = New PopupWindowProperties()
            Select Case navigatePath.ToString
                Case "ListOfValue"
                    Dim view As TransactionModule.TransactionView = _container.Resolve(Of TransactionModule.TransactionView)()
                    _regionManager.Regions("ChildRegion").Add(view)
                    PopupWindow.Caption = "List of Values"
                    PopupWindow.Width = 800
                    PopupWindow.Height = 600
                    PopupWindow.Top = 50
                    PopupWindow.Left = 50
                    PopupWindow.WindowState = Xceed.Wpf.Toolkit.WindowState.Open
                    OnPropertyChanged("PopupWindow")
                Case "SaveLayout"
                    Dim view As SaveLayoutView = _container.Resolve(Of SaveLayoutView)()
                    _regionManager.Regions("ChildRegion").Add(view)
                    PopupWindow.Caption = "Save Layout"
                    PopupWindow.Width = 300
                    PopupWindow.Height = 200
                    PopupWindow.Top = 50
                    PopupWindow.Left = 50
                    PopupWindow.WindowState = Xceed.Wpf.Toolkit.WindowState.Open
                    OnPropertyChanged("PopupWindow")
                Case Else
            End Select
            '_regionManager.RequestNavigate(RegionNames.DockingAreaRegion, navigatePath.ToString(), AddressOf NavigationComplete)
        End If
    End Sub

    Private Sub NavigationComplete(result As NavigationResult)

    End Sub

End Class
