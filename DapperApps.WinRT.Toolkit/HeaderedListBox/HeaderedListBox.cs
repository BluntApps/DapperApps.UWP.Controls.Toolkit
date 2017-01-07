/*
 * Copyright (c) Dapper Apps.  All rights reserved.
 * Use of this sample source code is subject to the terms of the Dapper Apps license 
 * agreement under which you licensed this sample source code and is provided AS-IS.
 * If you did not accept the terms of the license agreement, you are not authorized 
 * to use this sample source code.  For the terms of the license, please see the 
 * license agreement between you and Dapper Apps.
 *
 * To see the article about this app, visit http://www.dapper-apps.com/DapperToolkit
 */

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace DapperApps.WinRT.Toolkit.HeaderedListBox
{
    /// <summary>
    /// A ListBox with scrolling Header and content.
    /// </summary>
    [TemplatePart(Name = HeaderName, Type = typeof(ContentControl))]
    [TemplatePart(Name = ItemsName, Type = typeof(ContentControl))]
    public class HeaderedListBox : ListBox
    {
        /// <summary>
        /// Template Part name for Header.
        /// </summary>
        private const string HeaderName = "Header";

        /// <summary>
        /// Template Part name for Header.
        /// </summary>
        private const string ItemsName = "Items";

        #region Header Dependency Property
        /// <summary>
        /// Gets or sets the content for the header of the control.
        /// </summary>
        /// <value>
        /// The content for the header of the control. The default value is
        /// null.
        /// </value>
        public object Header
        {
            get { return GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        /// <summary>
        /// Identifies the
        /// <see cref="P:System.Windows.Controls.HeaderedContentControl.Header" />
        /// dependency property.
        /// </summary>
        /// <value>
        /// The identifier for the
        /// <see cref="P:System.Windows.Controls.HeaderedContentControl.Header" />
        /// dependency property.
        /// </value>
        public static readonly DependencyProperty HeaderProperty =
                DependencyProperty.Register(
                        "Header",
                        typeof(object),
                        typeof(HeaderedListBox),
                        new PropertyMetadata(null));
        #endregion

        #region HeaderTemplate Dependency Property
        /// <summary>
        /// Gets or sets the template that is used to display the content of the
        /// control's header.
        /// </summary>
        /// <value>
        /// The template that is used to display the content of the control's
        /// header. The default is null.
        /// </value>
        public DataTemplate HeaderTemplate
        {
            get { return (DataTemplate)GetValue(HeaderTemplateProperty); }
            set { SetValue(HeaderTemplateProperty, value); }
        }

        /// <summary>
        /// Identifies the
        /// <see cref="P:System.Windows.Controls.HeaderedContentControl.HeaderTemplate" />
        /// dependency property.
        /// </summary>
        /// <value>
        /// The identifier for the
        /// <see cref="P:System.Windows.Controls.HeaderedContentControl.HeaderTemplate" />
        /// dependency property.
        /// </value>
        public static readonly DependencyProperty HeaderTemplateProperty =
                DependencyProperty.Register(
                        "HeaderTemplate",
                        typeof(DataTemplate),
                        typeof(HeaderedListBox),
                        new PropertyMetadata(null));
        #endregion

        /// <summary>
        /// Instantiates a new HeaderedListBox.
        /// </summary>
        public HeaderedListBox()
            : base()
        {
            this.DefaultStyleKey = typeof(HeaderedListBox);
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }
    }
}
