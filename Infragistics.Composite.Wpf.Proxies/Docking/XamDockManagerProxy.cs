using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Infragistics.Composite.Wpf.Proxies.Properties;
using Infragistics.Windows.DockManager;
using System.Windows.Markup;

using IGDock = Infragistics.Windows.DockManager;

namespace Infragistics.Composite.Wpf.Proxies.Docking
{
    /// <summary>
    /// Represents the <see cref="XamDockManager"/> control.
    /// </summary>
    public class XamDockManagerProxy : ContentControl
    {
        #region Fields

        readonly XamDockManager _xamDockManager;
        readonly IList<SplitPaneProxy> _splitPaneProxies;

        #endregion // Fields

        #region Constructor

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="xamDockManager">A <see cref="XamDockManager"/> instance.</param>
        public XamDockManagerProxy(ContentControl xamDockManager)
        {
            // The 'xamDockManager' argument is of type ContentControl because the
            // main NCAL assembly cannot reference the Infragistics assemblies.

            _xamDockManager = xamDockManager as XamDockManager;

            if (_xamDockManager == null)
                throw new ArgumentException(Strings.Docking_Exception_XamDockManagerProxyArgument);

            _splitPaneProxies = new List<SplitPaneProxy>();

            // Create proxies for any pre-existing splitpanes.
            foreach (SplitPane existingPane in _xamDockManager.Panes)
                _splitPaneProxies.Add(new SplitPaneProxy(existingPane));

            this.HookEvents(true);
        }

        #endregion // Constructor

        #region Events

        /// <summary>
        /// Raised when the active pane of the <see cref="XamDockManager"/> changes.
        /// </summary>
        public event RoutedPropertyChangedEventHandler<ContentPaneProxy> ActivePaneChanged;

        #endregion // Events

        #region Methods

        /// <summary>
        /// Returns the <see cref="ContentPaneProxy"/> whose associated <see cref="ContentPane"/> contains the
        /// specified object as its logical child (i.e. its Content). Returns null if a pane cannot be found.
        /// </summary>
        /// <param name="depObj">The object contained by the pane.</param>
        public ContentPaneProxy FindContainingContentPaneProxy(DependencyObject depObj)
        {
            if (depObj == null)
                throw new ArgumentNullException("depObj");

            ContentPane contentPane = LogicalTreeHelper.GetParent(depObj) as ContentPane;
            return contentPane == null ? null : ContentPaneProxy.GetAssociatedProxy(contentPane);
        }

        /// <summary>
        /// Finds, or creates, a <see cref="SplitPaneProxy"/> whose associated <see cref="SplitPane"/> has the specified name.
        /// </summary>
        /// <param name="splitPaneName">The name of the pane associated with the proxy to find or create.</param>
        public SplitPaneProxy GetSplitPaneProxyByName(string splitPaneName)
        {
            if (splitPaneName == null)
                throw new ArgumentNullException("splitPaneName");

            SplitPaneProxy splitPaneProxy = _splitPaneProxies.FirstOrDefault(proxy => proxy.SplitPane.Name == splitPaneName);
            if (splitPaneProxy == null)
            {
                splitPaneProxy = new SplitPaneProxy(splitPaneName);
                _splitPaneProxies.Add(splitPaneProxy);
            }

            return splitPaneProxy;
        }

        /// <summary>
        /// Invoked when a <see cref="SplitPane"/> should be added to the dock manager's element tree.
        /// </summary>
        /// <param name="splitPaneProxy">The proxy that contains the pane to be added to the element tree.</param>
        public void AddSplitPaneToElementTree(SplitPaneProxy splitPaneProxy)
        {
            if (splitPaneProxy == null)
                throw new ArgumentNullException("splitPaneProxy");

            if (!_xamDockManager.Panes.Contains(splitPaneProxy.SplitPane))
                _xamDockManager.Panes.Add(splitPaneProxy.SplitPane);
        }

        public void AddContentPaneToDocumentHost(ContentPaneProxy contentPaneProxy)
        {
            if (contentPaneProxy == null)
                throw new ArgumentNullException("contentPaneProxy");

            // YCL - .NET Framework 4.0 bug?
            // Need to disassociate view from ContentPane. Otherwise, when adding view to DocumentContentHost
            // will cause the error: Specified element is already the logical child of another element. Disconnect it first.


            //----------------- Original Code. Works on .NET Framework 4.5 -------------------
            //  _xamDockManager.AddDocument(contentPaneProxy.Header, contentPaneProxy.ContentPane.Content);
            //----------------- End Original Code --------------------------------------------


            //----------------- New Code. Works on .NET Framework 4.0 and 4.5-----------------

            DependencyObject depObj = contentPaneProxy.ContentPane.Content as DependencyObject;
            ContentPane cp = LogicalTreeHelper.GetParent(depObj) as ContentPane;
            if (cp != null)
                cp.Content = null;

            ContentPane newCP;
            newCP = _xamDockManager.AddDocument(contentPaneProxy.Header, depObj);
            newCP.Name = contentPaneProxy.ContentPane.Name;
            newCP.Activate();

            //----------------- End New Code -------------------------------------------------

        }
        #endregion // Methods

        #region Attached Properties

        #region InitialLocation

        /// <summary>
        /// Returns the value of the 'InitialLocation' attached property.
        /// </summary>
        /// <param name="paneProxy">The <see cref="SplitPaneProxy"/> from which the property value is retrieved.</param>
        public static Proxies.Docking.InitialPaneLocation GetInitialLocation(SplitPaneProxy paneProxy)
        {
            return (Proxies.Docking.InitialPaneLocation)paneProxy.GetValue(InitialLocationProperty);
        }

        /// <summary>
        /// Sets the 'InitialLocation' attached property on the specified proxy.
        /// </summary>
        /// <param name="paneProxy">The <see cref="SplitPaneProxy"/> on which the property value is set.</param>
        /// <param name="value">The value set on the proxy.</param>
        public static void SetInitialLocation(SplitPaneProxy paneProxy, Proxies.Docking.InitialPaneLocation value)
        {
            paneProxy.SetValue(InitialLocationProperty, value);
        }

        /// <summary>
        /// Identifies the 'InitialLocation' attached property.
        /// </summary>
        public static readonly DependencyProperty InitialLocationProperty =
            DependencyProperty.RegisterAttached(
            "InitialLocation",
            typeof(Proxies.Docking.InitialPaneLocation),
            typeof(XamDockManagerProxy),
            new UIPropertyMetadata(Proxies.Docking.InitialPaneLocation.DockedLeft, OnInitialLocationChanged));

        static void OnInitialLocationChanged(DependencyObject depObj, DependencyPropertyChangedEventArgs e)
        {
            SplitPaneProxy proxy = depObj as SplitPaneProxy;
            if (proxy == null)
                return;

            if (e.NewValue is Proxies.Docking.InitialPaneLocation == false)
                return;

            var value = (IGDock.InitialPaneLocation)EnumMappingAttribute.GetMappedValue(e.NewValue);

            XamDockManager.SetInitialLocation(proxy.SplitPane, value);
        }

        #endregion // InitialLocation

        #endregion // Attached Properties

        #region Private Helpers

        void HookEvents(bool attach)
        {
            if (attach)
            {
                _xamDockManager.ActivePaneChanged += this.OnXamDockManagerActivePaneChanged;
                _xamDockManager.Panes.CollectionChanged += this.OnXamDockManagerPanesCollectionChanged;
            }
            else
            {
                _xamDockManager.ActivePaneChanged -= this.OnXamDockManagerActivePaneChanged;
                _xamDockManager.Panes.CollectionChanged -= this.OnXamDockManagerPanesCollectionChanged;
            }
        }

        void OnXamDockManagerPanesCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    Debug.Assert(e.NewItems.Count == 1, "More than one pane was added: " + e.NewItems.Count);
                    if (e.NewItems.Count > 0)
                    {
                        SplitPane splitPane = e.NewItems[0] as SplitPane;
                        SplitPaneProxy existingProxy = _splitPaneProxies.FirstOrDefault(proxy => proxy.SplitPane == splitPane);
                        if (existingProxy == null)
                            _splitPaneProxies.Add(new SplitPaneProxy(splitPane));
                    }
                    break;

                case NotifyCollectionChangedAction.Remove:
                    Debug.Assert(e.OldItems.Count == 1, "More than one pane was removed: " + e.OldItems.Count);
                    if (e.OldItems.Count > 0)
                    {
                        SplitPane splitPane = e.OldItems[0] as SplitPane;
                        SplitPaneProxy toBeRemoved = _splitPaneProxies.FirstOrDefault(proxy => proxy.SplitPane == splitPane);
                        if (toBeRemoved != null)
                            _splitPaneProxies.Remove(toBeRemoved);
                    }
                    break;

                default:
                    Debug.Fail("Unexpected NotifyCollectionChangedAction: " + e.Action);
                    break;
            }
        }

        void OnXamDockManagerActivePaneChanged(object sender, RoutedPropertyChangedEventArgs<ContentPane> e)
        {
            RoutedPropertyChangedEventHandler<ContentPaneProxy> handler = this.ActivePaneChanged;
            if (handler != null)
            {
                ContentPaneProxy oldProxy = e.OldValue == null ? null : ContentPaneProxy.GetAssociatedProxy(e.OldValue);
                ContentPaneProxy newProxy = e.NewValue == null ? null : ContentPaneProxy.GetAssociatedProxy(e.NewValue);
                var proxyArgs = new RoutedPropertyChangedEventArgs<ContentPaneProxy>(oldProxy, newProxy);
                handler(this, proxyArgs);
            }
        }

        #endregion // Private Helpers
    }
}