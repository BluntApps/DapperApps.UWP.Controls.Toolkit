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
//    using Windows.UI.Xaml;
//    using Windows.UI.Xaml.Controls;

//    /// <summary>
//    /// A listener class that provides events for various scrolling scenarios.
//    /// </summary>
//    public class PageRotationAnimation : DependencyObject
//    {
//        /// <summary>
//        /// The target FrameWorkElement to rotate.
//        /// </summary>
//        private FrameworkElement _target;

//        /// <summary>
//        /// The parent PhoneApplicationPage to listen for orientation changed events.
//        /// </summary>
//        private Page _parentPage;

//        /// <summary>
//        /// The last page orientation of the parent page of this ZoomingImage control.
//        /// </summary>
//        private Orientation _lastOrientation;

//        #region AnimationDirection Dependency Property
//        public AnimationDirection AnimationDirection
//        {
//            get { return (AnimationDirection)GetValue(AnimationDirectionProperty); }
//            set { SetValue(AnimationDirectionProperty, value); }
//        }

//        // Using a DependencyProperty as the backing store for AnimationDirection.  This enables animation, styling, binding, etc...
//        public static readonly DependencyProperty AnimationDirectionProperty =
//            DependencyProperty.Register(
//            "AnimationDirection",
//            typeof(AnimationDirection),
//            typeof(PageRotationAnimation),
//            new PropertyMetadata(null));
//        #endregion

//        public FrameworkElement Target
//        {
//            get
//            {
//                return _target;
//            }
//            internal set
//            {
//                if (null != value)
//                {
//                    _target = value;
//                    _target.Loaded += OnTargetLoaded;
//                }
//            }
//        }

//        void OnTargetLoaded(object sender, RoutedEventArgs e)
//        {
//            if (!DesignerProperties.IsInDesignTool)
//            {
//                _parentPage = ((Frame)Application.Current.RootVisual).Content as PhoneApplicationPage;
//                if (null == _parentPage)
//                {
//                    throw new NullReferenceException("The parent page of this PageRotationAnimation cannot be null.");
//                }
//                _lastOrientation = _parentPage.Orientation;
//                _parentPage.OrientationChanged += ParentPage_OrientationChanged;
//            }
//        }

//        /// <summary>
//        /// Event for when the orientation of the target page changes.
//        /// </summary>
//        /// <param name="sender">The PhoneApplicationPage that raised the event.</param>
//        /// <param name="ocea">The event args associated with this OrientationChanged event.</param>
//        private void ParentPage_OrientationChanged(object sender, OrientationChangedEventArgs ocea)
//        {
//            // Make into an attached property.
//            PageOrientation newOrientation = ocea.Orientation;
//            RotateTransition rotation = new RotateTransition();
//            switch (_lastOrientation)
//            {
//                // Dont need to worry about case where you go from landscape left to landscape right or vice versa (180 degrees).
//                case PageOrientation.LandscapeLeft:
//                    // New orientation should never be == old orientation.
//                    if (newOrientation == PageOrientation.PortraitUp)
//                    {
//                        // From landscape left to portrait up.
//                        rotation.Mode = RotateTransitionMode.In90Counterclockwise;
//                    }
//                    else
//                    {
//                        // From landscape left to landscape right.
//                        rotation.Mode = RotateTransitionMode.In180Counterclockwise;
//                    }
//                    break;
//                case PageOrientation.LandscapeRight:
//                    if (newOrientation == PageOrientation.PortraitUp)
//                    {
//                        // From landscape right to portrait up.
//                        rotation.Mode = RotateTransitionMode.In90Clockwise;
//                    }
//                    else
//                    {
//                        // From landscape right to portrait down.
//                        rotation.Mode = RotateTransitionMode.In180Clockwise;
//                    }
//                    break;
//                // Dont need to worry about case where you go from landscape left to landscape right or vice versa (180 degrees).
//                case PageOrientation.PortraitUp:
//                    // New orientation should never be == old orientation.
//                    if (newOrientation == PageOrientation.LandscapeLeft)
//                    {
//                        rotation.Mode = RotateTransitionMode.In90Clockwise;
//                    }
//                    else
//                    {
//                        rotation.Mode = RotateTransitionMode.In90Counterclockwise;
//                    }
//                    break;
//                default:
//                    break;
//            }

//            // Execute the transition
//            ITransition transition = rotation.GetTransition(_target);
//            transition.Completed += (o, e) => transition.Stop();
//            transition.Begin();
//            _lastOrientation = newOrientation;
//        }
//    }

//    /// <summary>
//    /// Determines the direction on animation on orientation changes.
//    /// </summary>
//    public enum AnimationDirection
//    {
//        WithRotation,
//        AgainstRotation
//    }
//}