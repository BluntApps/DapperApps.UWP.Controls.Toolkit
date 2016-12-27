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

//namespace DapperApps.Phone.Controls
//{
//    using System;
//    using System.Windows;
//    using Windows.UI.Xaml;

//    public static class PageRotationService
//    {
//        #region PageRotationAnimation AttachedProperty
//        /// <summary>
//        /// Gets a PageRotationAnimation property for the provided DependencyObject, creates a new one if necessary.
//        /// </summary>
//        /// <param name="obj">The DependencyObject whose PageRotationAnimation you would like to get.</param>
//        /// <returns>The PageRotationAnimation associated with the provided DependencyObject</returns>
//        public static PageRotationAnimation GetPageRotationAnimation(DependencyObject obj)
//        {
//            if (null == obj)
//            {
//                throw new ArgumentNullException("obj");
//            }
//            else
//            {
//                PageRotationAnimation animation =
//                    (obj.GetValue(PageRotationAnimationProperty) as PageRotationAnimation) ?? new PageRotationAnimation();
//                obj.SetValue(PageRotationAnimationProperty, animation);
//                return animation;
//            }
//        }

//        /// <summary>
//        /// Sets the PageRotationAnimation Attached Property for the provided DependencyObject.
//        /// </summary>
//        /// <param name="obj">The DependencyObject whose PageRotationAnimation you would like to set.</param>
//        /// <param name="value">The PageRotationAnimation you would like to set to the provided DependencyObject.</param>
//        public static void SetPageRotationAnimation(DependencyObject obj, PageRotationAnimation value)
//        {
//            if (null == obj)
//            {
//                throw new ArgumentNullException("obj");
//            }
//            obj.SetValue(PageRotationAnimationProperty, value);
//        }

//        // Using a DependencyProperty as the backing store for PageRotationAnimation.  This enables animation, styling, binding, etc...
//        public static readonly DependencyProperty PageRotationAnimationProperty =
//            DependencyProperty.RegisterAttached(
//            "PageRotationAnimation",
//            typeof(PageRotationAnimation),
//            typeof(PageRotationService),
//            new PropertyMetadata(null, OnPageRotationAnimationChanged));
//        #endregion Attached Properties

//        /// <summary>
//        /// Handles changes to the PageRotationAnimation DependencyProperty.
//        /// </summary>
//        /// <param name="o">DependencyObject that changed.</param>
//        /// <param name="e">Event data for the DependencyPropertyChangedEvent.</param>
//        private static void OnPageRotationAnimationChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
//        {
//            FrameworkElement target = o as FrameworkElement;
//            if (null != target)
//            {
//                PageRotationAnimation animation = e.NewValue as PageRotationAnimation;
//                if (null != animation)
//                {
//                    animation.Target = target;
//                }
//                else
//                {
//                    throw new InvalidOperationException("The content of a PageRotationService must be a PageRotationAnimation.");
//                }
//            }
//            else
//            {
//                throw new ArgumentNullException("o");
//            }
//        }
//    }
//}
