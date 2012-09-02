using System.Collections.Generic;
using Infragistics.Windows.DockManager;

namespace Infragistics.Composite.Wpf.Proxies.Docking
{
    internal class TabGroupPaneProxy
    {
        readonly TabGroupPane _tabGroupPane;
        readonly List<ContentPaneProxy> _contentPaneProxies;

        internal TabGroupPaneProxy()
        {
            _tabGroupPane = new TabGroupPane();
            _contentPaneProxies = new List<ContentPaneProxy>();
        }

        internal TabGroupPane TabGroupPane
        {
            get { return _tabGroupPane; }
        }

        internal void AddContentPaneProxy(ContentPaneProxy contentPaneProxy)
        {
            contentPaneProxy.Closed += this.OnContentPaneProxyClosed;
            _contentPaneProxies.Add(contentPaneProxy);
            _tabGroupPane.Items.Add(contentPaneProxy.ContentPane);
        }

        void OnContentPaneProxyClosed(object sender, System.EventArgs e)
        {
            ContentPaneProxy proxy = sender as ContentPaneProxy;
            if (proxy == null)
                return;

            proxy.Closed -= this.OnContentPaneProxyClosed;

            _contentPaneProxies.Remove(proxy);
        }
    }
}