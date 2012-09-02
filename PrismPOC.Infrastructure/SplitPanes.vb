Imports System
Imports System.Windows

Public Class SplitPanes
    Public Const LeftSidePaneName As String = "SPLITPANE_LeftSide"
    Public Const RightSidePaneName As String = "SPLITPANE_RightSide"

    Shared s_styles As SplitPanes

    ReadOnly _leftSidePaneStyle As Style
    ReadOnly _rightSidePaneStyle As Style

    Public Shared ReadOnly Property Styles() As SplitPanes
        Get
            If s_styles Is Nothing Then
                s_styles = New SplitPanes()
            End If

            Return s_styles
        End Get
    End Property

    Private Sub New()
        Dim paneStyles As ResourceDictionary = TryCast(Application.LoadComponent(New Uri("PrismPoC.Infrastructure;component/SplitPaneStyles.xaml", UriKind.Relative)), ResourceDictionary)

        _leftSidePaneStyle = TryCast(paneStyles(LeftSidePaneName), Style)
        _rightSidePaneStyle = TryCast(paneStyles(RightSidePaneName), Style)
    End Sub

    ''' <summary>
    ''' The Style applied to the <see cref="SplitPane"/> initially docked 
    ''' to the left side of the <see cref="XamDockManager"/> region.
    ''' </summary>
    Public ReadOnly Property LeftSidePane() As Style
        Get
            Return _leftSidePaneStyle
        End Get
    End Property

    ''' <summary>
    ''' The Style applied to the <see cref="SplitPane"/> initially docked 
    ''' to the right side of the <see cref="XamDockManager"/> region.
    ''' </summary>
    Public ReadOnly Property RightSidePane() As Style
        Get
            Return _rightSidePaneStyle
        End Get
    End Property
End Class
