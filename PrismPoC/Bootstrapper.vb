Imports Microsoft.Practices.Prism
Imports Microsoft.Practices.Prism.UnityExtensions
Imports Microsoft.Practices.Unity
Imports Microsoft.Practices.Prism.Modularity

Public Class Bootstrapper
    Inherits UnityBootstrapper

    Protected Overrides Function CreateShell() As System.Windows.DependencyObject
        Return Container.Resolve(Of Shell)()
    End Function

    Protected Overrides Sub InitializeShell()
        MyBase.InitializeShell()

        Application.Current.MainWindow = CType(Me.Shell, Window)
        Application.Current.MainWindow.Show()
    End Sub

    Protected Overrides Sub ConfigureModuleCatalog()
        MyBase.ConfigureModuleCatalog()

        Dim moduleCatalog As ModuleCatalog = CType(Me.ModuleCatalog, ModuleCatalog)
        moduleCatalog.AddModule(GetType(MenuModule.MenuModule))
        moduleCatalog.AddModule(GetType(ToolbarModule.ToolbarModule))
        moduleCatalog.AddModule(GetType(XamDockManagerModule.XamDockManagerModule))
    End Sub

End Class
