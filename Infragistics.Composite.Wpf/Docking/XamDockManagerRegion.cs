using System;
using System.Windows;
using System.Windows.Data;
using Infragistics.Composite.Wpf.Properties;
using Infragistics.Composite.Wpf.Proxies.Docking;
using Microsoft.Practices.Prism.Regions;

namespace Infragistics.Composite.Wpf.Docking
{
    /// <summary>
    /// A <see cref="SingleActiveRegion"/> subclass that works with <see cref="XamDockManager"/>.
    /// </summary>
    public class XamDockManagerRegion : SingleActiveRegion
    {
        #region Fields

        WeakReference _xamDockManagerProxyWeakRef;

        #endregion // Fields

        #region Public Properties

        /// <summary>
        /// Returns true if the region has a reference to a <see cref="XamDockManagerProxy"/>.
        /// </summary>
        public bool HasDockManagerProxy
        {
            get { return _xamDockManagerProxyWeakRef != null && GetWeakReferenceTargetSafe(_xamDockManagerProxyWeakRef) != null; }
        }

        /// <summary>
        /// Gets/sets the <see cref="XamDockManagerProxy"/> referenced by this region.
        /// </summary>
        public XamDockManagerProxy DockManagerProxy
        {
            get { return (_xamDockManagerProxyWeakRef != null) ? GetWeakReferenceTargetSafe(_xamDockManagerProxyWeakRef) as XamDockManagerProxy : null; }
            set
            {
                XamDockManagerProxy currentDockManagerProxy = this.DockManagerProxy;

                if (value == currentDockManagerProxy)
                    return;

                if (currentDockManagerProxy != null)
                    currentDockManagerProxy.ActivePaneChanged -= this.OnActivePaneChanged;

                if (value != null)
                {
                    _xamDockManagerProxyWeakRef = new WeakReference(value);
                    value.ActivePaneChanged += this.OnActivePaneChanged;
                }
                else
                {
                    _xamDockManagerProxyWeakRef = null;
                }
            }
        }

        #endregion // Public Properties

        #region Base Class Overrides

        #region Add

        /// <summary>
        /// Adds a new view to the region.
        /// </summary>
        /// <param name="view">The view to add.</param>
        /// <param name="viewName">The name of the view. This can be used to retrieve it later by calling <see cref="IRegion.GetView"/>.</param>
        /// <param name="createRegionManagerScope">When <see langword="true"/>, the added view will receive a new instance of <see cref="IRegionManager"/>, otherwise it will use the current region manager for this region.</param>
        /// <returns>The <see cref="IRegionManager"/> that is set on the view if it is a <see cref="DependencyObject"/>.</returns>
        public override IRegionManager Add(object view, string viewName, bool createRegionManagerScope)
        {
            if (!this.HasDockManagerProxy)
                throw new InvalidOperationException(Strings.Docking_Exception_Uninitialized);

            DependencyObject depObj = view as DependencyObject;
            
            // CONTENT PANE
            string contentPaneName = depObj == null ? String.Empty : XamDockManagerSettings.GetContentPaneName(depObj);
            ContentPaneProxy contentPaneProxy = new ContentPaneProxy(view, contentPaneName);
            contentPaneProxy.Closed += this.OnContentPaneClosed;
            this.ApplyContentPaneStyle(view, contentPaneProxy);

            XamDockManagerProxy dockManagerProxy = this.DockManagerProxy;

            // YCL - Added support to add view to the DocumentContentHost
            bool isInDocumentContentHost = this.ShouldAddViewToDocumentContentHost(view);
            if (isInDocumentContentHost)
            {
                dockManagerProxy.AddContentPaneToDocumentHost(contentPaneProxy);
            }
            else
            {
                // SPLIT PANE
                string splitPaneName = depObj == null ? String.Empty : XamDockManagerSettings.GetSplitPaneName(depObj);
                SplitPaneProxy splitPaneProxy = dockManagerProxy.GetSplitPaneProxyByName(splitPaneName);
                this.ApplySplitPaneStyle(view, splitPaneProxy);
                // We must delay adding the SplitPane to the element tree so that the 
                // attached InitialLocation property can be set first, via a Style.
                dockManagerProxy.AddSplitPaneToElementTree(splitPaneProxy);

                // ADD VIEW TO DOCKMANAGER
                bool isInTabGroup = this.ShouldAddViewToTabPaneGroup(view);
                splitPaneProxy.AddContentPaneProxy(contentPaneProxy, isInTabGroup);            
            }
            return base.Add(view, viewName, createRegionManagerScope);
        }

        #endregion // Add

        #region Remove

        /// <summary>
        /// Removes the specified view from the region.
        /// </summary>
        /// <param name="view">The view to remove.</param>
        public override void Remove(object view)
        {
            DependencyObject depObj = view as DependencyObject;
            XamDockManagerProxy dockManagerProxy = this.DockManagerProxy;
            if (depObj != null && dockManagerProxy != null)
            {
                ContentPaneProxy contentPaneProxy = dockManagerProxy.FindContainingContentPaneProxy(depObj);
                if (contentPaneProxy != null)
                    contentPaneProxy.Close();
            }

            base.Remove(view);
        }

        #endregion // Remove

        #endregion // Base Class Overrides

        #region Private Helpers

        void ApplyContentPaneStyle(object view, ContentPaneProxy contentPaneProxy)
        {
            this.ApplyPaneStyleHelper(view, contentPaneProxy, XamDockManagerSettings.ContentPaneProxyStyleProperty);
        }

        void ApplySplitPaneStyle(object view, SplitPaneProxy splitPaneProxy)
        {
            this.ApplyPaneStyleHelper(view, splitPaneProxy, XamDockManagerSettings.SplitPaneProxyStyleProperty);
        }

        void ApplyPaneStyleHelper(object view, DependencyObject pane, DependencyProperty attachedStyleProperty)
        {
            DependencyObject depObj = view as DependencyObject;
            if (depObj != null)
            {
                Binding bnd = new Binding
                {
                    Path = new PropertyPath(attachedStyleProperty),
                    Source = depObj
                };
                BindingOperations.SetBinding(pane, FrameworkElement.StyleProperty, bnd);
            }
        }

        static object GetWeakReferenceTargetSafe(WeakReference weakReference)
        {
            try
            {
                return weakReference.Target;
            }
            catch (Exception)
            {
                return null;
            }
        }

        void OnActivePaneChanged(object sender, RoutedPropertyChangedEventArgs<ContentPaneProxy> e)
        {
            ContentPaneProxy pane = e.NewValue;
            if (pane != null && pane.Content != null && base.Views.Contains(pane.Content))
                base.Activate(pane.Content);
        }

        void OnContentPaneClosed(object sender, EventArgs e)
        {
            ContentPaneProxy pane = sender as ContentPaneProxy;
            if (pane.Content != null && base.Views.Contains(pane.Content))
                this.Remove(pane.Content);
        }

        bool ShouldAddViewToTabPaneGroup(object view)
        {
            DependencyObject depObj = view as DependencyObject;
            if (depObj != null)
                return XamDockManagerSettings.GetIsContentPaneInTabGroup(depObj);

            return true;
        }

        bool ShouldAddViewToDocumentContentHost(object view)
        {
            DependencyObject depObj = view as DependencyObject;
            if (depObj != null)
                return XamDockManagerSettings.GetIsContentPaneInDocumentContentHost(depObj);

            return true;
        }

        #endregion // Private Helpers
    }
}