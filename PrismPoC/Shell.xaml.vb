﻿Imports Microsoft.Practices.Prism.Events
Imports PrismPoC.Infrastructure
Imports Infragistics.Windows.DockManager
Imports System.IO
Imports Microsoft.Practices.Unity

Public Class Shell
    Private _eventAggregator As IEventAggregator
    Private _container As IUnityContainer
    Private Shared paneNames As Dictionary(Of String, Integer)


    Public Sub New(container As IUnityContainer, eventAggregator As IEventAggregator)
        InitializeComponent()
        If IsNothing(paneNames) Then
            paneNames = New Dictionary(Of String, Integer)
        End If

        _container = container
        _eventAggregator = eventAggregator
        _eventAggregator.GetEvent(Of SaveLayoutEvent)().Subscribe(AddressOf SaveLayout)
        _eventAggregator.GetEvent(Of LoadLayoutEvent)().Subscribe(AddressOf LoadLayout)
        _eventAggregator.GetEvent(Of ExitAppEvent)().Subscribe(AddressOf ExitApp)

    End Sub

    Private Sub SaveLayout(ByVal layoutName As String)
        Dim layoutString As String

        RenumberPanes()

        layoutString = DockRegion.SaveLayout()
        Using fs As New FileStream("layout.xml", FileMode.Create, FileAccess.Write)
            DockRegion.SaveLayout(fs)
        End Using
        'MessageBox.Show(String.Format("{0}: {1}", layoutName, layoutString))
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
        End Select
    End Sub

    Private Sub RenumberPanes()
        Dim panes As IEnumerable(Of ContentPane)
        panes = DockRegion.GetPanes(PaneNavigationOrder.ActivationOrder)

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

End Class
