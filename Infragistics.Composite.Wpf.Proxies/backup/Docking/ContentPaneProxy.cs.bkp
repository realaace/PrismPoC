using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Infragistics.Windows.DockManager;
using Infragistics.Windows.DockManager.Events;
using IGDock = Infragistics.Windows.DockManager;

namespace Infragistics.Composite.Wpf.Proxies.Docking
{
    /// <summary>
    /// Represents the <see cref="ContentPane"/> element used by <see cref="XamDockManager"/>.
    /// Properties set on this object via its Style property are, in turn, applied to the actual
    /// ContentPane element shown in the user interface.  The <see cref="DataContext"/>
    /// of this proxy is the ContentPane element it represents.  A ContentPaneProxy is never part
    /// of an element tree, but its associated ContentPane is added to a XamDockManager's element
    /// tree at run time.
    /// </summary>
    public class ContentPaneProxy : HeaderedContentControl
    {
        #region Fields

        readonly ContentPane _contentPane;

        #endregion // Fields

        #region Constructor

        /// <summary>
        /// Initializes a new object.
        /// </summary>
        /// <param name="content">The content of the ContentPane.</param>
        public ContentPaneProxy(object content)
        {
            _contentPane = new ContentPane();
            _contentPane.Content = content;
            ContentPaneProxy.SetAssociatedProxy(_contentPane, this);

            base.DataContext = _contentPane;

            this.HookEvents(true);
        }

        #endregion // Constructor

        #region Routed Events

        #region Closed

        /// <summary>
        /// Represents the 'Closed' routed event.  This field is read-only.
        /// </summary>
        public static readonly RoutedEvent ClosedEvent = 
            EventManager.RegisterRoutedEvent(
            "Closed", 
            RoutingStrategy.Direct, 
            typeof(RoutedEventHandler), 
            typeof(ContentPaneProxy));

        /// <summary>
        /// Raised when the associated <see cref="ContentPane"/> is closed.
        /// This event uses the 'Direct' routing strategy, enabling it to referenced by an <see cref="EventSetter"/>.
        /// </summary>
        public event RoutedEventHandler Closed
        {
            add { AddHandler(ClosedEvent, value); }
            remove { RemoveHandler(ClosedEvent, value); }
        }

        #endregion // Closed

        #endregion // Routed Events

        #region Methods

        /// <summary>
        /// Attempts to close the associated <see cref="ContentPane"/>, using the
        /// CloseAction of 'RemovePane'.
        /// </summary>
        public void Close()
        {
            ContentPaneProxy.SetAssociatedProxy(_contentPane, null);
            _contentPane.Content = null;
            _contentPane.CloseAction = PaneCloseAction.RemovePane;
            _contentPane.ExecuteCommand(ContentPaneCommands.Close);
        }

        #endregion // Methods

        #region Base Class Overrides

        /// <summary>
        /// Transfers property settings applied via a Style to the associated <see cref="ContentPane"/> element.
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

            _contentPane.SetValue(e.Property, newValue);
        }

        #endregion // Base Class Overrides

        #region Attached Properties

        #region AssociatedProxy

        internal static ContentPaneProxy GetAssociatedProxy(ContentPane pane)
        {
            return (ContentPaneProxy)pane.GetValue(AssociatedProxyProperty);
        }

        internal static void SetAssociatedProxy(ContentPane pane, ContentPaneProxy value)
        {
            pane.SetValue(AssociatedProxyPropertyKey, value);
        }

        private static readonly DependencyPropertyKey AssociatedProxyPropertyKey =
            DependencyProperty.RegisterAttachedReadOnly(
            "AssociatedProxy",
            typeof(ContentPaneProxy),
            typeof(ContentPaneProxy),
            new UIPropertyMetadata(null));

        internal static readonly DependencyProperty AssociatedProxyProperty = AssociatedProxyPropertyKey.DependencyProperty;

        #endregion // AssociatedProxy

        #endregion // Attached Properties

        #region Properties

        /// <summary>
        /// Returns the content of this proxy's associated <see cref="ContentPane"/>.
        /// </summary>
        public new object Content
        {
            get { return _contentPane.Content; }
        }

        internal ContentPane ContentPane
        {
            get { return _contentPane; }
        }

        #endregion // Properties

        #region ContentPane Properties

        #region AllowClose

        public bool AllowClose
        {
            get { return (bool)GetValue(AllowCloseProperty); }
            set { SetValue(AllowCloseProperty, value); }
        }

        public static readonly DependencyProperty AllowCloseProperty = IGDock.ContentPane.AllowCloseProperty.AddOwner(typeof(ContentPaneProxy));

        #endregion // AllowClose

        #region AllowDocking

        public bool AllowDocking
        {
            get { return (bool)GetValue(AllowDockingProperty); }
            set { SetValue(AllowDockingProperty, value); }
        }

        public static readonly DependencyProperty AllowDockingProperty = IGDock.ContentPane.AllowDockingProperty.AddOwner(typeof(ContentPaneProxy));

        #endregion // AllowDocking

        #region AllowDockingBottom

        public Nullable<bool> AllowDockingBottom
        {
            get { return (Nullable<bool>)GetValue(AllowDockingBottomProperty); }
            set { SetValue(AllowDockingBottomProperty, value); }
        }

        public static readonly DependencyProperty AllowDockingBottomProperty = IGDock.ContentPane.AllowDockingBottomProperty.AddOwner(typeof(ContentPaneProxy));

        #endregion // AllowDocking

        #region AllowDockingFloating

        public Nullable<bool> AllowDockingFloating
        {
            get { return (Nullable<bool>)GetValue(AllowDockingFloatingProperty); }
            set { SetValue(AllowDockingFloatingProperty, value); }
        }

        public static readonly DependencyProperty AllowDockingFloatingProperty = IGDock.ContentPane.AllowDockingFloatingProperty.AddOwner(typeof(ContentPaneProxy));

        #endregion // AllowDocking

        #region AllowDockingInTabGroup

        public bool AllowDockingInTabGroup
        {
            get { return (bool)GetValue(AllowDockingInTabGroupProperty); }
            set { SetValue(AllowDockingInTabGroupProperty, value); }
        }

        public static readonly DependencyProperty AllowDockingInTabGroupProperty = IGDock.ContentPane.AllowDockingInTabGroupProperty.AddOwner(typeof(ContentPaneProxy));

        #endregion // AllowDockingInTabGroup

        #region AllowDockingLeft

        public Nullable<bool> AllowDockingLeft
        {
            get { return (Nullable<bool>)GetValue(AllowDockingLeftProperty); }
            set { SetValue(AllowDockingLeftProperty, value); }
        }

        public static readonly DependencyProperty AllowDockingLeftProperty = IGDock.ContentPane.AllowDockingLeftProperty.AddOwner(typeof(ContentPaneProxy));

        #endregion // AllowDockingLeft

        #region AllowDockingRight

        public Nullable<bool> AllowDockingRight
        {
            get { return (Nullable<bool>)GetValue(AllowDockingRightProperty); }
            set { SetValue(AllowDockingRightProperty, value); }
        }

        public static readonly DependencyProperty AllowDockingRightProperty = IGDock.ContentPane.AllowDockingRightProperty.AddOwner(typeof(ContentPaneProxy));

        #endregion // AllowDockingLeft

        #region AllowDockingTop

        public Nullable<bool> AllowDockingTop
        {
            get { return (Nullable<bool>)GetValue(AllowDockingTopProperty); }
            set { SetValue(AllowDockingTopProperty, value); }
        }

        public static readonly DependencyProperty AllowDockingTopProperty = IGDock.ContentPane.AllowDockingTopProperty.AddOwner(typeof(ContentPaneProxy));

        #endregion // AllowDockingLeft

        #region AllowFloatingOnly

        public bool AllowFloatingOnly
        {
            get { return (bool)GetValue(AllowFloatingOnlyProperty); }
            set { SetValue(AllowFloatingOnlyProperty, value); }
        }

        public static readonly DependencyProperty AllowFloatingOnlyProperty = IGDock.ContentPane.AllowFloatingOnlyProperty.AddOwner(typeof(ContentPaneProxy));

        #endregion // AllowFloatingOnly

        #region AllowInDocumentHost

        public bool AllowInDocumentHost
        {
            get { return (bool)GetValue(AllowInDocumentHostProperty); }
            set { SetValue(AllowInDocumentHostProperty, value); }
        }

        public static readonly DependencyProperty AllowInDocumentHostProperty = IGDock.ContentPane.AllowInDocumentHostProperty.AddOwner(typeof(ContentPaneProxy));

        #endregion // AllowInDocumentHost

        #region AllowPinning

        public bool AllowPinning
        {
            get { return (bool)GetValue(AllowPinningProperty); }
            set { SetValue(AllowPinningProperty, value); }
        }

        public static readonly DependencyProperty AllowPinningProperty = IGDock.ContentPane.AllowPinningProperty.AddOwner(typeof(ContentPaneProxy));

        #endregion // AllowPinning

        #region CloseAction [not implemented]

        // NOTE: The CloseAction property of ContentPane is not exposed by ContentPaneProxy because 
        // a ContentPaneProxy sets it to 'RemovePane' upon being closed.  This ensures that the
        // View contained in the pane is deactivated by CAL.

        #endregion // CloseAction [not implemented]

        #region Image

        public ImageSource Image
        {
            get { return (ImageSource)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        public static readonly DependencyProperty ImageProperty = IGDock.ContentPane.ImageProperty.AddOwner(typeof(ContentPaneProxy));

        #endregion // Image

        #region IsPinned

        public bool IsPinned
        {
            get { return (bool)GetValue(IsPinnedProperty); }
            set { SetValue(IsPinnedProperty, value); }
        }

        public static readonly DependencyProperty IsPinnedProperty = IGDock.ContentPane.IsPinnedProperty.AddOwner(typeof(ContentPaneProxy));

        #endregion // IsPinned

        #region NavigatorDescription

        public object NavigatorDescription
        {
            get { return (bool)GetValue(NavigatorDescriptionProperty); }
            set { SetValue(NavigatorDescriptionProperty, value); }
        }

        public static readonly DependencyProperty NavigatorDescriptionProperty = IGDock.ContentPane.NavigatorDescriptionProperty.AddOwner(typeof(ContentPaneProxy));

        #endregion // NavigatorDescription

        #region NavigatorDescriptionTemplate

        public DataTemplate NavigatorDescriptionTemplate
        {
            get { return (DataTemplate)GetValue(NavigatorDescriptionTemplateProperty); }
            set { SetValue(NavigatorDescriptionTemplateProperty, value); }
        }

        public static readonly DependencyProperty NavigatorDescriptionTemplateProperty = IGDock.ContentPane.NavigatorDescriptionTemplateProperty.AddOwner(typeof(ContentPaneProxy));

        #endregion // NavigatorDescriptionTemplate

        #region NavigatorDescriptionTemplateSelector

        public DataTemplateSelector NavigatorDescriptionTemplateSelector
        {
            get { return (DataTemplateSelector)GetValue(NavigatorDescriptionTemplateSelectorProperty); }
            set { SetValue(NavigatorDescriptionTemplateSelectorProperty, value); }
        }

        public static readonly DependencyProperty NavigatorDescriptionTemplateSelectorProperty = IGDock.ContentPane.NavigatorDescriptionTemplateSelectorProperty.AddOwner(typeof(ContentPaneProxy));

        #endregion // NavigatorDescriptionTemplateSelector

        #region NavigatorTitle

        public object NavigatorTitle
        {
            get { return (bool)GetValue(NavigatorTitleProperty); }
            set { SetValue(NavigatorTitleProperty, value); }
        }

        public static readonly DependencyProperty NavigatorTitleProperty = IGDock.ContentPane.NavigatorTitleProperty.AddOwner(typeof(ContentPaneProxy));

        #endregion // NavigatorTitle

        #region NavigatorTitleTemplate

        public DataTemplate NavigatorTitleTemplate
        {
            get { return (DataTemplate)GetValue(NavigatorTitleTemplateProperty); }
            set { SetValue(NavigatorTitleTemplateProperty, value); }
        }

        public static readonly DependencyProperty NavigatorTitleTemplateProperty = IGDock.ContentPane.NavigatorTitleTemplateProperty.AddOwner(typeof(ContentPaneProxy));

        #endregion // NavigatorTitleTemplate

        #region NavigatorTitleTemplateSelector

        public DataTemplateSelector NavigatorTitleTemplateSelector
        {
            get { return (DataTemplateSelector)GetValue(NavigatorTitleTemplateSelectorProperty); }
            set { SetValue(NavigatorTitleTemplateSelectorProperty, value); }
        }

        public static readonly DependencyProperty NavigatorTitleTemplateSelectorProperty = IGDock.ContentPane.NavigatorTitleTemplateSelectorProperty.AddOwner(typeof(ContentPaneProxy));

        #endregion // NavigatorTitleTemplateSelector

        #region SaveInLayout

        public bool SaveInLayout
        {
            get { return (bool)GetValue(SaveInLayoutProperty); }
            set { SetValue(SaveInLayoutProperty, value); }
        }

        public static readonly DependencyProperty SaveInLayoutProperty = IGDock.ContentPane.SaveInLayoutProperty.AddOwner(typeof(ContentPaneProxy));

        #endregion // SaveInLayout

        #region SerializationId

        public string SerializationId
        {
            get { return (string)GetValue(SerializationIdProperty); }
            set { SetValue(SerializationIdProperty, value); }
        }

        public static readonly DependencyProperty SerializationIdProperty = IGDock.ContentPane.SerializationIdProperty.AddOwner(typeof(ContentPaneProxy));

        #endregion // SerializationId

        #region TabHeader

        public object TabHeader
        {
            get { return (bool)GetValue(TabHeaderProperty); }
            set { SetValue(TabHeaderProperty, value); }
        }

        public static readonly DependencyProperty TabHeaderProperty = IGDock.ContentPane.TabHeaderProperty.AddOwner(typeof(ContentPaneProxy));

        #endregion // TabHeader

        #region TabHeaderTemplate

        public DataTemplate TabHeaderTemplate
        {
            get { return (DataTemplate)GetValue(TabHeaderTemplateProperty); }
            set { SetValue(TabHeaderTemplateProperty, value); }
        }

        public static readonly DependencyProperty TabHeaderTemplateProperty = IGDock.ContentPane.TabHeaderTemplateProperty.AddOwner(typeof(ContentPaneProxy));

        #endregion // TabHeaderTemplate

        #region TabHeaderTemplateSelector

        public DataTemplateSelector TabHeaderTemplateSelector
        {
            get { return (DataTemplateSelector)GetValue(TabHeaderTemplateSelectorProperty); }
            set { SetValue(TabHeaderTemplateSelectorProperty, value); }
        }

        public static readonly DependencyProperty TabHeaderTemplateSelectorProperty = IGDock.ContentPane.TabHeaderTemplateSelectorProperty.AddOwner(typeof(ContentPaneProxy));

        #endregion // TabHeaderTemplateSelector

        #endregion // ContentPane Properties

        #region Private Helpers

        void HookEvents(bool attach)
        {
            if (attach)
            {
                _contentPane.Closed += this.OnContentPaneClosed;
            }
            else
            {
                _contentPane.Closed -= this.OnContentPaneClosed;
            }
        }

        void OnContentPaneClosed(object sender, PaneClosedEventArgs e)
        {
            this.OnClosed();
            this.HookEvents(false);
        }

        void OnClosed()
        {
            base.RaiseEvent(new RoutedEventArgs(ClosedEvent, this));
        }

        #endregion // Private Helpers
    }
}