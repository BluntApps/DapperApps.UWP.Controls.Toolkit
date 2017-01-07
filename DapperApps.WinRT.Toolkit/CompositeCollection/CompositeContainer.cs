/*
 * Copyright (c) Dapper Apps.  All rights reserved.
 * Use of this source code is subject to the terms of the Dapper Apps license 
 * agreement under which you licensed this sample source code and is provided AS-IS.
 * If you did not accept the terms of the license agreement, you are not authorized 
 * to use this sample source code.  For the terms of the license, please see the 
 * license agreement between you and Dapper Apps.
 *
 * To see the article about this app, visit http://www.dapper-apps.com/DapperToolkit
 */

using System.Collections;
using Windows.UI.Xaml;

namespace DapperApps.WinRT.Toolkit.CompositeCollection
{
    public class CompositeContainer : DependencyObject
    {
        #region Collection Dependency Property
        /// <summary>
        /// TODO
        /// </summary>
        public IEnumerable Collection
        {
            get { return (IEnumerable)GetValue(CollectionProperty); }
            set { SetValue(CollectionProperty, value); }
        }

        /// <summary>
        /// TODO
        /// </summary>
        public static readonly DependencyProperty CollectionProperty =
            DependencyProperty.Register(
            "Collection",
            typeof(IEnumerable),
            typeof(CompositeContainer),
            new PropertyMetadata(null));
        #endregion
    }
}
