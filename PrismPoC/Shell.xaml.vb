Imports Microsoft.Practices.Prism.Events
Imports Microsoft.Practices.Prism.Regions
Imports PrismPoC.Infrastructure
Imports Infragistics.Windows.DockManager
Imports System.IO
Imports Microsoft.Practices.Unity

Public Class Shell
    Private _eventAggregator As IEventAggregator
    Private _container As IUnityContainer
    Private _regionManager As IRegionManager
    Private Shared paneNames As Dictionary(Of String, Integer)
    Private _popupWin As Popup


    Public Sub New(container As IUnityContainer, regionManager As IRegionManager, eventAggregator As IEventAggregator)
        InitializeComponent()
        If IsNothing(paneNames) Then
            paneNames = New Dictionary(Of String, Integer)
        End If

        _container = container
        _regionManager = regionManager
        _eventAggregator = eventAggregator

        _container.RegisterType(Of SaveLayoutView)()
        _eventAggregator.GetEvent(Of SaveLayoutEvent)().Subscribe(AddressOf SaveLayout)
        _eventAggregator.GetEvent(Of LoadLayoutEvent)().Subscribe(AddressOf LoadLayout)
        _eventAggregator.GetEvent(Of ExitAppEvent)().Subscribe(AddressOf ExitApp)
        _eventAggregator.GetEvent(Of SelectedItemChangedEvent)().Subscribe(AddressOf LoadSelectedItem)
        _eventAggregator.GetEvent(Of CreateViewEvent)().Subscribe(AddressOf CreateView)
        _eventAggregator.GetEvent(Of TransactionButtonEvent)().Subscribe(AddressOf AddView)

    End Sub

    Private Sub SaveLayout(ByVal layoutName As String)
        Dim layoutString As String

        RenumberPanes()

        layoutString = DockRegion.SaveLayout()
        Using fs As New FileStream("layout.xml", FileMode.Create, FileAccess.Write)
            DockRegion.SaveLayout(fs)
        End Using
        'MessageBox.Show(String.Format("{0}: {1}", layoutName, layoutString))
        cwPopup.WindowState = Xceed.Wpf.Toolkit.WindowState.Closed
    End Sub

    Private Sub LoadLayout()

        RenumberPanes()

        Using fs As New FileStream("layout.xml", FileMode.Open, FileAccess.Read)
            DockRegion.LoadLayout(fs)
        End Using
    End Sub

    Private Sub DockRegion_InitializePaneContent(sender As Object, e As Infragistics.Windows.DockManager.Events.InitializePaneContentEventArgs) Handles DockRegion.InitializePaneContent
        Dim cpName As String = e.NewPane.Name

        If cpName.LastIndexOf("_") > 0 Then
            cpName = cpName.Substring(0, cpName.LastIndexOf("_"))
        End If

        Select Case cpName
            Case "Transactions"
                e.NewPane.Content = _container.Resolve(Of TransactionModule.TransactionView)()
                e.NewPane.Header = "Transactions"
            Case "SavedQueries"
                e.NewPane.Content = _container.Resolve(Of SavedQueryModule.SavedQueryView)()
                e.NewPane.Header = "Saved Queries"
            Case "PropertyGrid"
                e.NewPane.Content = _container.Resolve(Of PropertyGrid.PropertyGridView2)()
                e.NewPane.Header = "Properties"
        End Select
    End Sub

    Private Sub RenumberPanes()
        Dim panes As IEnumerable(Of ContentPane)
        panes = DockRegion.GetPanes(PaneNavigationOrder.VisibleOrder)

        paneNames.Clear()
        For Each cp As ContentPane In panes
            Dim cpName As String = cp.Name
            If cpName.LastIndexOf("_") > 0 Then
                cpName = cpName.Substring(0, cpName.LastIndexOf("_"))
            End If

            If paneNames.ContainsKey(cpName) Then
                Dim i As Integer = paneNames.Item(cpName)
                i = i + 1
                cp.Name = cpName + "_" + i.ToString()
                paneNames(cpName) = i
            Else
                paneNames.Add(cpName, 1)
                cp.Name = cpName + "_1"
            End If
        Next
    End Sub

    Private Sub ExitApp()
        Me.Close()
    End Sub

    Private Sub LoadSelectedItem(selectedItem As Object)
        Dim panes As IEnumerable(Of ContentPane)
        panes = DockRegion.GetPanes(PaneNavigationOrder.ActivationOrder)

        Dim found As Boolean = False

        For Each cp As ContentPane In panes
            Dim cpName As String = cp.Name
            If cpName.Contains("PropertyGrid") Then
                Dim pgvm As PropertyGrid.PropertyGridViewModel
                pgvm = cp.Content.ViewModel
                If pgvm.ParentHashCode = selectedItem(0) Then
                    pgvm.SelectedObject = selectedItem(1)
                    found = True
                    Exit For
                ElseIf pgvm.ParentHashCode = 0 Then
                    pgvm.ParentHashCode = selectedItem(0)
                    pgvm.SelectedObject = selectedItem(1)
                    found = True
                    Exit For
                End If
            End If
        Next

        'If we need to create new view
        If Not found And selectedItem(2) = True Then
            Dim pgv As PropertyGrid.PropertyGridView2 = _container.Resolve(Of PropertyGrid.PropertyGridView2)()
            pgv.ViewModel.ParentHashCode = selectedItem(0)
            pgv.ViewModel.SelectedObject = selectedItem(1)
            _regionManager.Regions(RegionNames.DockingAreaRegion).Add(pgv)
        End If

    End Sub

    Private Sub CreateView(ByVal viewType As String)

        Dim panes As IEnumerable(Of ContentPane)
        panes = DockRegion.GetPanes(PaneNavigationOrder.ActivationOrder)
        paneNames.Clear()

        Dim found As Boolean = False
        For Each cp As ContentPane In panes
            Dim cpName As String = cp.Name
            If cpName.Contains("PropertyGrid") Then
                Dim pgvm As PropertyGrid.PropertyGridViewModel
                pgvm = cp.Content.ViewModel
                If pgvm.ParentHashCode = 0 Then
                    found = True
                    Exit For
                End If
            End If
        Next
        If Not found Then
            _regionManager.Regions(RegionNames.DockingAreaRegion).Add(_container.Resolve(Of PropertyGrid.PropertyGridView2)())
        End If
        _eventAggregator.GetEvent(Of ViewCreatedEvent)().Publish("Property")
    End Sub

    Private Sub AddView(ByVal viewType As String)

        Select Case viewType
            Case "ListOfValue"
                'Dim popUpWin As Popup = New Popup(_regionManager)
                '_regionManager.Regions(RegionNames.PopupRegion).Add(_container.Resolve(Of TransactionModule.TransactionView)())
                'popUpWin.ShowDialog()
                Dim view As TransactionModule.TransactionView
                view = _container.Resolve(Of TransactionModule.TransactionView)()
                _regionManager.Regions("ChildRegion").Add(view)
                cwPopup.Caption = "List of Values"
                cwPopup.Width = 800
                cwPopup.Height = 600
                cwPopup.Top = 50
                cwPopup.Left = (Me.ActualWidth - 800) / 2
                cwPopup.WindowState = Xceed.Wpf.Toolkit.WindowState.Open
            Case "SaveLayout"
                Dim view As SaveLayoutView
                view = _container.Resolve(Of SaveLayoutView)()
                _regionManager.Regions("ChildRegion").Add(view)
                cwPopup.Caption = "Save Layout"
                cwPopup.Width = 300
                cwPopup.Height = 200
                cwPopup.Top = 50
                cwPopup.Left = (Me.ActualWidth - 300) / 2
                cwPopup.WindowState = Xceed.Wpf.Toolkit.WindowState.Open

        End Select
    End Sub

    Private Sub cwPopup_Closing(sender As Object, e As ComponentModel.CancelEventArgs)
        For Each view In _regionManager.Regions("ChildRegion").Views
            _regionManager.Regions("ChildRegion").Remove(view)
        Next

    End Sub
End Class
