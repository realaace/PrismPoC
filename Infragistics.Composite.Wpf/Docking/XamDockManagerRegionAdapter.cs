using System.Windows.Controls;
using Infragistics.Composite.Wpf.Proxies.Docking;
using Microsoft.Practices.Prism.Regions;

namespace Infragistics.Composite.Wpf.Docking
{
    /// <summary>
    /// Adapter that creates a new <see cref="XamDockManagerRegionAdapter"/>.
    /// The <see cref="ActivePane"/> of the <see cref="XamDockManager"/> contains the
    /// <see cref="ActiveView"/> of the region.
    /// </summary>
    /// <remarks>
    /// The base class's type argument is <see cref="ContentControl"/> because this
    /// assembly cannot directly reference the Infragistics assemblies.
    /// </remarks>
    public class XamDockManagerRegionAdapter : RegionAdapterBase<ContentControl>
    {

        public XamDockManagerRegionAdapter(IRegionBehaviorFactory factory)
            : base(factory)
        {

        }
        /// <summary>
        /// Adapts a <see cref="XamDockManager"/> to an <see cref="IRegion"/>.
        /// </summary>
        /// <param name="region">The new region being used.</param>
        /// <param name="regionTarget">The object to adapt.</param>
        protected override void Adapt(IRegion region, ContentControl regionTarget)
        {
            XamDockManagerRegion xdmRegion = region as XamDockManagerRegion;
            if (xdmRegion != null && !xdmRegion.HasDockManagerProxy)
                xdmRegion.DockManagerProxy = new XamDockManagerProxy(regionTarget);
        }

        /// <summary>
        /// Creates a new instance of <see cref="XamDockManagerRegion"/>.
        /// </summary>
        /// <returns>A new instance of <see cref="XamDockManagerRegion"/>.</returns>
        protected override IRegion CreateRegion()
        {
            return new XamDockManagerRegion();
        }
    }
}