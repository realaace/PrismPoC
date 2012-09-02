using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Infragistics.Windows.DockManager;

using IGDock = Infragistics.Windows.DockManager;

namespace Infragistics.Composite.Wpf.Proxies.Docking
{
    /// <summary>
    /// Represents the <see cref="SplitPane"/> element used by <see cref="XamDockManager"/>.
    /// Properties set on this object via its Style property are, in turn, applied to the actual
    /// SplitPane element shown in the user interface.  The <see cref="DataContext"/>
    /// of this proxy is the SplitPane element it represents.  A SplitPaneProxy is never part
    /// of an element tree, but its associated SplitPane is added to a XamDockManager's element
    /// tree at run time.
    /// </summary>
    public class SplitPaneProxy : FrameworkElement
    {
        #region Fields

        readonly List<ContentPaneProxy> _contentPaneProxies;
        readonly SplitPane _splitPane;
        TabGroupPaneProxy _tabGroupPaneProxy;

        #endregion // Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance, creating a <see cref="SplitPane"/> with the specified name.
        /// </summary>
        /// <param name="name">The name assigned to the <see cref="SplitPane"/>.</param>
        public SplitPaneProxy(string name)
            : this(new SplitPane { Name = name })
        {
        }

        internal SplitPaneProxy(SplitPane splitPane)
            : this()
        {
            _splitPane = splitPane;
            base.DataContext = _splitPane;
        }

        private SplitPaneProxy()
        {
            _contentPaneProxies = new List<ContentPaneProxy>();
        }

        #endregion // Constructors

        #region Methods

        /// <summary>
        /// Adds a <see cref="ContentPaneProxy"/> to this proxy.  
        /// </summary>
        /// <param name="contentPaneProxy">The ContentPaneProxy to add to the split pane.</param>
        /// <param name="isInTabGroup">
        /// True if the <see cref="ContentPane"/> should be added to a <see cref="TabPaneGroup"/> 
        /// contained within the <see cref="SplitPane"/> element.
        /// </param>
        public void AddContentPaneProxy(ContentPaneProxy contentPaneProxy, bool isInTabGroup)
        {
            _contentPaneProxies.Add(contentPaneProxy);

            if (isInTabGroup)
                this.TabGroupPaneProxy.AddContentPaneProxy(contentPaneProxy);
            else
                this.SplitPane.Panes.Add(contentPaneProxy.ContentPane);
        }

        #endregion // Methods

        #region Base Class Overrides

        /// <summary>
        /// Transfers property settings applied via a Style to the associated <see cref="SplitPane"/> element.
        /// </summary>
        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

            if (e.Property.ReadOnly || e.Property == FrameworkElement.StyleProperty)
                return;

            // Only apply values that were set from the Style applied to this object.
            BaseValueSource source = DependencyPropertyHelper.GetValueSource(this, e.Property).BaseValueSource;
            if (source != BaseValueSource.Style && source != BaseValueSource.StyleTrigger)
                return;

            object newValue = e.NewValue;

            // NOTE: If the property's type is a custom Infragistics enum, perform a mapping between
            // an NCAL-specific version of that enum to the Infragistics enum value here.  Be sure to
            // make use of the EnumMappingAttribute class and its static GetMappedValue method.

            _splitPane.SetValue(e.Property, newValue);
        }

        #endregion // Base Class Overrides

        #region Properties

        internal SplitPane SplitPane
        {
            get { return _splitPane; }
        }

        internal TabGroupPaneProxy TabGroupPaneProxy
        {
            get
            {
                // Create the proxy if it has not yet been created, or if it has been removed from the UI already.
                if (_tabGroupPaneProxy == null || _tabGroupPaneProxy.TabGroupPane.Parent == null)
                {
                    _tabGroupPaneProxy = new TabGroupPaneProxy();
                    _splitPane.Panes.Add(_tabGroupPaneProxy.TabGroupPane);
                }

                return _tabGroupPaneProxy;
            }
        }

        #endregion // Properties

        #region SplitPane Properties

        #region SplitterOrientation

        public Orientation SplitterOrientation
        {
            get { return (Orientation)GetValue(SplitterOrientationProperty); }
            set { SetValue(SplitterOrientationProperty, value); }
        }

        public static readonly DependencyProperty SplitterOrientationProperty = IGDock.SplitPane.SplitterOrientationProperty.AddOwner(typeof(SplitPaneProxy));

        #endregion // SplitterOrientation

        #endregion // SplitPane Properties
    }
}