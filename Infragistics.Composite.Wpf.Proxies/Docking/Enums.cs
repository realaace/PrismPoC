using IGDock = Infragistics.Windows.DockManager;

namespace Infragistics.Composite.Wpf.Proxies.Docking
{
    /// <summary>
    /// Enumeration used by the attached InitialLocation property of <see cref="XamDockManagerProxy"/>.
    /// This enumeration corresponds to the <see cref="Infragistics.Windows.DockManager.InitialPaneLocation"/> type.
    /// </summary>
    public enum InitialPaneLocation
    {
        [EnumMapping(IGDock.InitialPaneLocation.DockableFloating)]
        DockableFloating,

        [EnumMapping(IGDock.InitialPaneLocation.DockedBottom)]
        DockedBottom,

        [EnumMapping(IGDock.InitialPaneLocation.DockedLeft)]
        DockedLeft,

        [EnumMapping(IGDock.InitialPaneLocation.DockedRight)]
        DockedRight,

        [EnumMapping(IGDock.InitialPaneLocation.DockedTop)]
        DockedTop,

        [EnumMapping(IGDock.InitialPaneLocation.FloatingOnly)]
        FloatingOnly
    }
}