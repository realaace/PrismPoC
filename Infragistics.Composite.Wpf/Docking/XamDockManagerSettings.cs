using System;
using System.Windows;
using Infragistics.Composite.Wpf.Proxies.Docking;

namespace Infragistics.Composite.Wpf.Docking
{
    /// <summary>
    /// Exposes attached properties that can be set on elements in a View, which
    /// are subsequently applied to properties on visual elements in a <see cref="XamDockManager"/>.
    /// </summary>
    public static class XamDockManagerSettings
    {
        #region ContentPaneProxyStyle

        /// <summary>
        /// Gets the <see cref="ContentPaneProxyStyleProperty"/> attached property.
        /// </summary>
        /// <param name="obj">The <see cref="DependencyObject"/> on which the property is set.</param>
        /// <returns>The <see cref="Style"/> applied to a <see cref="ContentPaneProxy"/>.</returns>
        public static Style GetContentPaneProxyStyle(DependencyObject obj)
        {
            return (Style)obj.GetValue(ContentPaneProxyStyleProperty);
        }

        /// <summary>
        /// Sets the <see cref="ContentPaneProxyStyleProperty"/> attached property.
        /// </summary>
        /// <param name="obj">The <see cref="DependencyObject"/> on which the property is set.</param>
        /// <param name="value">The <see cref="Style"/> to apply to a <see cref="ContentPaneProxy"/>.</param>
        public static void SetContentPaneProxyStyle(DependencyObject obj, Style value)
        {
            obj.SetValue(ContentPaneProxyStyleProperty, value);
        }

        /// <summary>
        /// Identifies the ContentPaneProxyStyle attached property.
        /// </summary>
        /// <remarks>
        /// The <see cref="ContentPaneProxyStyle"/> attached property must be set on the
        /// root element in a View in order for it to be applied to the <see cref="ContentPane"/>
        /// in which that View is hosted.  If the TargetType property of the
        /// <see cref="Style"/> is not set to <see cref="ContentPaneProxy"/>, or a subclass thereof,
        /// an <see cref="InvalidContentPaneProxyStyleException"/> is thrown upon assignment.
        /// </remarks>
        public static readonly DependencyProperty ContentPaneProxyStyleProperty =
            DependencyProperty.RegisterAttached(
            "ContentPaneProxyStyle",
            typeof(Style),
            typeof(XamDockManagerSettings),
            new UIPropertyMetadata(null, OnContentPaneProxyStyleChanged));

        static void OnContentPaneProxyStyleChanged(DependencyObject depObj, DependencyPropertyChangedEventArgs e)
        {
            Style style = e.NewValue as Style;
            if (style != null && !typeof(ContentPaneProxy).IsAssignableFrom(style.TargetType))
                throw new InvalidContentPaneProxyStyleException();
        }

        #endregion // ContentPaneProxyStyle

        #region IsContentPaneInTabGroup

        /// <summary>
        /// Gets the <see cref="IsContentPaneInTabGroupProperty"/> attached property.
        /// </summary>
        /// <param name="obj">The <see cref="DependencyObject"/> on which the property is set.</param>
        public static bool GetIsContentPaneInTabGroup(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsContentPaneInTabGroupProperty);
        }

        /// <summary>
        /// Sets the <see cref="IsContentPaneInTabGroupProperty"/> attached property.
        /// </summary>
        /// <param name="obj">The <see cref="DependencyObject"/> on which the property is set.</param>
        /// <param name="value">The bool that determines whether the <see cref="ContentPane"/> created for a View is added to a <see cref="TabGroupPane"/> or not.</param>
        public static void SetIsContentPaneInTabGroup(DependencyObject obj, bool value)
        {
            obj.SetValue(IsContentPaneInTabGroupProperty, value);
        }

        /// <summary>
        /// Identifies the IsContentPaneInTabGroup attached property.
        /// </summary>
        /// <remarks>
        /// The <see cref="ContentPaneStyle"/> attached property must be set on the
        /// root element in a View in order for it to be applied to the <see cref="ContentPane"/>
        /// in which that View is hosted.  The default value is true.
        /// </remarks>
        public static readonly DependencyProperty IsContentPaneInTabGroupProperty =
            DependencyProperty.RegisterAttached(
            "IsContentPaneInTabGroup", 
            typeof(bool), 
            typeof(XamDockManagerSettings), 
            new UIPropertyMetadata(true));

        #endregion // IsContentPaneInTabGroup

        #region IsContentPaneInDocumentContentHost

        /// <summary>
        /// Gets the <see cref="IsContentPaneInDocumentContentHostProperty"/> attached property.
        /// </summary>
        /// <param name="obj">The <see cref="DependencyObject"/> on which the property is set.</param>
        public static bool GetIsContentPaneInDocumentContentHost(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsContentPaneInDocumentContentHostProperty);
        }

        /// <summary>
        /// Sets the <see cref="IsContentPaneInDocumentContentHostProperty"/> attached property.
        /// </summary>
        /// <param name="obj">The <see cref="DependencyObject"/> on which the property is set.</param>
        /// <param name="value">The bool that determines whether the <see cref="ContentPane"/> created for a View is added to a <see cref="DocumentContentHost"/> or not.</param>
        public static void SetIsContentPaneInDocumentContentHost(DependencyObject obj, bool value)
        {
            obj.SetValue(IsContentPaneInDocumentContentHostProperty, value);
        }

        /// <summary>
        /// Identifies the IsContentPaneInDocumentContentHost attached property.
        /// </summary>
        /// <remarks>
        /// The <see cref="ContentPaneStyle"/> attached property must be set on the
        /// root element in a View in order for it to be applied to the <see cref="ContentPane"/>
        /// in which that View is hosted.  The default value is true.
        /// </remarks>
        public static readonly DependencyProperty IsContentPaneInDocumentContentHostProperty =
            DependencyProperty.RegisterAttached(
            "IsContentPaneInDocumentContentHost",
            typeof(bool),
            typeof(XamDockManagerSettings),
            new UIPropertyMetadata(true));

        #endregion // IsContentPaneInDocumentContentHost

        #region SplitPaneProxyStyle

        /// <summary>
        /// Gets the <see cref="SplitPaneProxyStyleProperty"/> attached property.
        /// </summary>
        /// <param name="obj">The <see cref="DependencyObject"/> on which the property is set.</param>
        /// <returns>The <see cref="Style"/> applied to a <see cref="SplitPaneProxy"/>.</returns>
        public static Style GetSplitPaneProxyStyle(DependencyObject obj)
        {
            return (Style)obj.GetValue(SplitPaneProxyStyleProperty);
        }

        /// <summary>
        /// Sets the <see cref="SplitPaneProxyStyleProperty"/> attached property.
        /// </summary>
        /// <param name="obj">The <see cref="DependencyObject"/> on which the property is set.</param>
        /// <param name="value">The <see cref="Style"/> to apply to a <see cref="SplitPaneProxy"/>.</param>
        public static void SetSplitPaneProxyStyle(DependencyObject obj, Style value)
        {
            obj.SetValue(SplitPaneProxyStyleProperty, value);
        }

        /// <summary>
        /// Identifies the SplitPaneProxyStyle attached property.
        /// </summary>
        /// <remarks>
        /// The <see cref="SplitPaneProxyStyle"/> attached property must be set on the
        /// root element in a View in order for it to be applied to the <see cref="SplitPane"/>
        /// in which that View is hosted.  If the TargetType property of the
        /// <see cref="Style"/> is not set to <see cref="SplitPaneProxy"/>, or a subclass thereof,
        /// an <see cref="InvalidSplitPaneProxyStyleException"/> is thrown upon assignment.
        /// </remarks>
        public static readonly DependencyProperty SplitPaneProxyStyleProperty =
            DependencyProperty.RegisterAttached(
            "SplitPaneProxyStyle",
            typeof(Style),
            typeof(XamDockManagerSettings),
            new UIPropertyMetadata(null, OnSplitPaneStyleChanged));

        static void OnSplitPaneStyleChanged(DependencyObject depObj, DependencyPropertyChangedEventArgs e)
        {
            Style style = e.NewValue as Style;
            if (style != null && !typeof(SplitPaneProxy).IsAssignableFrom(style.TargetType))
                throw new InvalidSplitPaneProxyStyleException();
        }

        #endregion // SplitPaneProxyStyle

        #region SplitPaneName

        public static string GetSplitPaneName(DependencyObject obj)
        {
            return (string)obj.GetValue(SplitPaneNameProperty);
        }

        public static void SetSplitPaneName(DependencyObject obj, string value)
        {
            obj.SetValue(SplitPaneNameProperty, value);
        }

        public static readonly DependencyProperty SplitPaneNameProperty =
            DependencyProperty.RegisterAttached(
            "SplitPaneName", 
            typeof(string), 
            typeof(XamDockManagerSettings), 
            new UIPropertyMetadata(String.Empty));

        #endregion // SplitPaneName
    }
}