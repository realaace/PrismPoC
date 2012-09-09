Imports Microsoft.Practices.Prism
Imports Microsoft.Practices.Prism.UnityExtensions
Imports Microsoft.Practices.Unity
Imports Microsoft.Practices.Prism.Modularity
Imports Microsoft.Practices.Prism.Regions
Imports Infragistics.Composite.Wpf.Docking
Imports Infragistics.Windows.DockManager

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

        '3 modules
        Dim moduleCatalog As ModuleCatalog = CType(Me.ModuleCatalog, ModuleCatalog)
        moduleCatalog.AddModule(GetType(MenuModule.MenuModule))
        moduleCatalog.AddModule(GetType(ToolbarModule.ToolbarModule))
        moduleCatalog.AddModule(GetType(TransactionModule.TransactionModule))
        moduleCatalog.AddModule(GetType(SavedQueryModule.SavedQueryModule))
        moduleCatalog.AddModule(GetType(PropertyGrid.PropertyGridModule))
    End Sub

    Protected Overrides Function ConfigureRegionAdapterMappings() As RegionAdapterMappings
        ' Let the base class register the standard mappings.
        Dim mappings As RegionAdapterMappings = MyBase.ConfigureRegionAdapterMappings()
        Dim factory As IRegionBehaviorFactory = Container.TryResolve(Of IRegionBehaviorFactory)()

        'YCL need to instantiate XamDockManagerRegionAdapter
        ' Register a region adapter mapping for the XamDockManager.
        mappings.RegisterMapping(GetType(XamDockManager), New XamDockManagerRegionAdapter(factory))
        Return mappings
    End Function

End Class
