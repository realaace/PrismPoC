Imports Microsoft.Practices.Prism.Events
Imports PrismPoC.Infrastructure
Imports Infragistics.Windows.DockManager
Imports System.IO
Imports Microsoft.Practices.Unity

Public Class Shell
    Private _eventAggregator As IEventAggregator
    Private _container As IUnityContainer

    Public Sub New(container As IUnityContainer, eventAggregator As IEventAggregator)
        InitializeComponent()
        _container = container
        _eventAggregator = eventAggregator
        _eventAggregator.GetEvent(Of SaveLayoutEvent)().Subscribe(AddressOf SaveLayout)
        _eventAggregator.GetEvent(Of LoadLayoutEvent)().Subscribe(AddressOf LoadLayout)

    End Sub

    Private Sub SaveLayout(ByVal layoutName As String)
        Dim layoutString As String
        'Dim paneNames As New Dictionary(Of String, Integer)
        'Dim panes As IEnumerable(Of ContentPane)
        'panes = DockRegion.GetPanes(PaneNavigationOrder.VisibleOrder)
        'For Each cp As ContentPane In panes
        '    If paneNames.ContainsKey(cp.Name) Then
        '        Dim i As Integer = paneNames.Item(cp.Name)
        '        i = i = 1
        '        cp.Name = cp.Name + "_" + i.ToString()
        '    Else
        '        paneNames.Add(cp.Name, 1)
        '        cp.Name = cp.Name + "_1"
        '    End If
        '    MessageBox.Show(cp.Name)
        'Next

        layoutString = DockRegion.SaveLayout()
        Using fs As New FileStream("layout.xml", FileMode.Create, FileAccess.Write)
            DockRegion.SaveLayout(fs)
        End Using
        'MessageBox.Show(String.Format("{0}: {1}", layoutName, layoutString))
    End Sub

    Private Sub LoadLayout()
        Using fs As New FileStream("layout.xml", FileMode.Open, FileAccess.Read)
            DockRegion.LoadLayout(fs)
        End Using
    End Sub

    Private Sub DockRegion_InitializePaneContent(sender As Object, e As Infragistics.Windows.DockManager.Events.InitializePaneContentEventArgs) Handles DockRegion.InitializePaneContent
        Dim cpName As String = e.NewPane.Name
        Dim paneNames As New Dictionary(Of String, Integer)

        If cpName.LastIndexOf("_") > 0 Then
            cpName = cpName.Substring(0, cpName.LastIndexOf("_"))
        End If

        Select Case cpName
            Case "Transactions"
                'e.NewPane.Content = New TransactionModule.TransactionView()
                e.NewPane.Content = _container.Resolve(Of TransactionModule.TransactionView)()
            Case "SavedQueries"
                e.NewPane.Content = _container.Resolve(Of SavedQueryModule.SavedQueryView)()
                e.NewPane.Header = "Saved Queries"
                'e.NewPane.Content = New SavedQueryModule.SavedQueryView()
        End Select
    End Sub

    Private Sub DockRegion_LayoutUpdated(sender As Object, e As System.EventArgs) Handles DockRegion.LayoutUpdated
        Dim panes As IEnumerable(Of ContentPane)
        Dim paneNames As New Dictionary(Of String, Integer)
        panes = DockRegion.GetPanes(PaneNavigationOrder.VisibleOrder)
        For Each cp As ContentPane In panes
            If cp.Name.LastIndexOf("_") < 0 Then
                If paneNames.ContainsKey(cp.Name) Then
                    Dim i As Integer = paneNames.Item(cp.Name)
                    i = i + 1
                    paneNames(cp.Name) = i
                    cp.Name = cp.Name + "_" + i.ToString()
                Else
                    paneNames.Add(cp.Name, 1)
                    cp.Name = cp.Name + "_1"
                End If
            End If
            'MessageBox.Show(cp.Name)
        Next
    End Sub
End Class
