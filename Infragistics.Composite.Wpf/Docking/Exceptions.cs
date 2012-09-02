using System;
using Infragistics.Composite.Wpf.Properties;

namespace Infragistics.Composite.Wpf.Docking
{
    #region InvalidContentPaneProxyStyleException

    /// <summary>
    /// Exception thrown if the ContentPaneStyle attached property of <see cref="XamDockManagerSettings"/>
    /// is assigned a Style whose TargetType is incompatible with the <see cref="ContentPaneProxy"/> class.
    /// </summary>
    public class InvalidContentPaneProxyStyleException : Exception
    {
        internal InvalidContentPaneProxyStyleException() 
            : base(Strings.Docking_Exception_InvalidContentPaneProxyStyle)
        {
        }
    }

    #endregion // InvalidContentPaneProxyStyleException

    #region InvalidSplitPaneProxyStyleException

    /// <summary>
    /// Exception thrown if the SplitPaneStyle attached property of <see cref="XamDockManagerSettings"/>
    /// is assigned a Style whose TargetType is incompatible with the <see cref="SplitPaneProxy"/> class.
    /// </summary>
    public class InvalidSplitPaneProxyStyleException : Exception
    {
        internal InvalidSplitPaneProxyStyleException()
            : base(Strings.Docking_Exception_InvalidSplitPaneProxyStyle)
        {
        }
    }

    #endregion // InvalidSplitPaneProxyStyleException
}