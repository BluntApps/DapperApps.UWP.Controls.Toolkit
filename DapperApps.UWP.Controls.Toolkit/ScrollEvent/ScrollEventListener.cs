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

namespace DapperApps.UWP.Controls
{
    using System;
    using System.Windows.Input;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Data;

    /// <summary>
    /// A listener class that provides events for various scrolling scenarios.
    /// </summary>
    public class ScrollEventListener : DependencyObject
    {
        /// <summary>
        /// The BottomReached event. Called when a user scrolls to the bottom of the target list.
        /// </summary>
        public event EventHandler<EventArgs> ThresholdReached;

        /// <summary>
        /// TODO
        /// </summary>
        private ScrollViewer _target;

        /// <summary>
        /// TODO
        /// </summary>
        private Binding _listener;

        /// <summary>
        /// The target element to check for scrolling.
        /// </summary>
        public ScrollViewer Target
        {
            get
            {
                return _target;
            }
            internal set
            {
                if (null != value)
                {
                    _target = value;
                    AttachBindingListener();
                    _target.Loaded +=
                        (s, e) =>
                        {
                            CheckThreshold();
                        };
                }
            }
        }

        #region BottomOffsetThreshold Dependency Property
        /// <summary>
        /// Offset, in pixels, from the bottom, to be reached.
        /// </summary>
        public double? BottomOffsetThreshold
        {
            get { return (double?)GetValue(BottomOffsetThresholdProperty); }
            set
            {
                if (TopOffsetThreshold.HasValue)
                    throw new InvalidOperationException("You cannot set both bottom and top offset values.");
                if (ThresholdPercentage.HasValue)
                    throw new InvalidOperationException("You cannot set both bottom and percent threshold values.");
                if (value > Target.ExtentHeight && value < 0)
                    throw new ArgumentOutOfRangeException("BottomOffsetThreshold");
                SetValue(BottomOffsetThresholdProperty, value);
            }
        }

        /// <summary>
        /// Dependency property for the offset, in pixels, from the bottom, to be reached.
        /// </summary>
        public static readonly DependencyProperty BottomOffsetThresholdProperty =
            DependencyProperty.Register(
                "BottomOffsetThreshold",
                typeof(double?),
                typeof(ScrollEventListener),
                new PropertyMetadata(null));
        #endregion

        #region TopOffsetThreshold Dependency Property
        /// <summary>
        /// Offset, in pixels, from the top, to be reached.
        /// </summary>
        public double? TopOffsetThreshold
        {
            get { return (double?)GetValue(TopOffsetThresholdProperty); }
            set
            {
                if (BottomOffsetThreshold.HasValue)
                    throw new InvalidOperationException("You cannot set both a top and bottom offset values.");
                if (ThresholdPercentage.HasValue)
                    throw new InvalidOperationException("You cannot set both top and percent threshold values.");
                if (value > Target.ExtentHeight && value < 0)
                    throw new ArgumentOutOfRangeException("TopOffsetThreshold");
                SetValue(TopOffsetThresholdProperty, value);
            }
        }

        /// <summary>
        /// Dependency property for the offset, in pixels, from the top, to be reached.
        /// </summary>
        public static readonly DependencyProperty TopOffsetThresholdProperty =
            DependencyProperty.Register(
                "TopOffsetThreshold",
                typeof(double?),
                typeof(ScrollEventListener),
                new PropertyMetadata(null));
        #endregion

        #region ThresholdPercentage Dependency Property
        /// <summary>
        /// The scrolling percentage, between 0 and 1, needed before reaching the threshold.
        /// </summary>
        public double? ThresholdPercentage
        {
            get
            {
                return (double?)GetValue(ThresholdPercentageProperty);
            }
            set
            {
                if (value < 0 || value > 1)
                    throw new ArgumentOutOfRangeException("ThresholdPercentage");
                if (BottomOffsetThreshold.HasValue)
                    throw new InvalidOperationException("You cannot set both percent and bottom threshold values.");
                if (TopOffsetThreshold.HasValue)
                    throw new InvalidOperationException("You cannot set both percent and top threshold values.");
                SetValue(ThresholdPercentageProperty, value);
            }
        }

        /// <summary>
        /// The scrolling amount, between 0 and 1, needed before reaching the threshold.
        /// </summary>
        public static readonly DependencyProperty ThresholdPercentageProperty =
            DependencyProperty.Register(
                "ScrollPercentage",
                typeof(double?),
                typeof(ScrollEventListener),
                new PropertyMetadata(null));
        #endregion

        #region VerticalOffsetBinding Dependency Property
        /// <summary>
        /// A property to bind to the Target's VerticalOffsetProperty.
        /// </summary>
        private double VerticalOffsetBinding
        {
            get { return (double)GetValue(VerticalOffsetBindingProperty); }
            set { SetValue(VerticalOffsetBindingProperty, value); }
        }

        /// <summary>
        /// Private property binded to a ScrollViewers' Vertical Offset property to recieve callbacks.
        /// </summary>
        private static readonly DependencyProperty VerticalOffsetBindingProperty =
            DependencyProperty.Register(
            "VerticalOffsetBinding",
            typeof(double),
            typeof(ScrollEventListener),
            new PropertyMetadata(new PropertyChangedCallback(OnVerticalOffsetChanged)));
        #endregion

        #region ThresholdReachedCommand Dependency Property
        public ICommand ThresholdReachedCommand
        {
            get { return (ICommand)GetValue(ThresholdReachedCommandProperty); }
            set { SetValue(ThresholdReachedCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ThresholdReachedCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ThresholdReachedCommandProperty =
            DependencyProperty.Register(
                "ThresholdReachedCommand",
                typeof(ICommand),
                typeof(ScrollEventListener),
                new PropertyMetadata(null, new PropertyChangedCallback(CommandChanged)));
        #endregion

        #region IsEnabled DependencyProperty
        public bool IsEnabled
        {
            get { return (bool)GetValue(IsEnabledProperty); }
            set { SetValue(IsEnabledProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsEnabled.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsEnabledProperty =
            DependencyProperty.Register(
                "IsEnabled",
                typeof(bool),
                typeof(ScrollEventListener),
                new PropertyMetadata(true, new PropertyChangedCallback(IsEnabledChanged)));
        #endregion

        private static void CommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ScrollEventListener sel = (ScrollEventListener)d;
            sel.HookUpCommand((ICommand)e.OldValue, (ICommand)e.NewValue);
        }

        /// <summary>
        /// Binds the VerticalOffsetBinding dependency property to
        /// the ScrollViewer's VerticalOffset property to recieve callbacks.
        /// </summary>
        private void AttachBindingListener()
        {
            if (null == _listener)
            {
                _listener = new Binding
                {
                    Source = Target,
                    Path = new PropertyPath("VerticalOffset"),
                    Mode = BindingMode.OneWay,
                };
                BindingOperations.SetBinding(this, ScrollEventListener.VerticalOffsetBindingProperty, _listener);
            }
        }

        private void DetachBindingListener()
        {
            if (null != _listener)
            {
                BindingOperations.SetBinding(this, ScrollEventListener.VerticalOffsetBindingProperty, null);
                _listener = null;
            }
        }

        private static void IsEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ScrollEventListener sel = (ScrollEventListener)d;
            if ((bool)e.NewValue)
            {
                sel.AttachBindingListener();
            }
            else
            {
                sel.DetachBindingListener();
            }
        }

        // Add a new command to the Command Property.
        private void HookUpCommand(ICommand oldCommand, ICommand newCommand)
        {
            // If oldCommand is not null, then we need to remove the handlers.
            if (null != oldCommand)
                oldCommand.CanExecuteChanged -= CanExecuteChanged;
            if (null != newCommand)
                newCommand.CanExecuteChanged += CanExecuteChanged;
        }

        private void CanExecuteChanged(object sender, EventArgs e)
        {
            if (null != ThresholdReachedCommand)
            {
                if (ThresholdReachedCommand.CanExecute(null))
                {
                    this.IsEnabled = true;
                }
                else
                {
                    this.IsEnabled = false;
                }
            }
        }

        /// <summary>
        /// Function called whenever the VerticalOffset property of the Target changes.
        /// </summary>
        private static void OnVerticalOffsetChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ((ScrollEventListener)sender).CheckThreshold();
        }

        /// <summary>
        /// Defines the logic performed to check if a threshold has been reached.
        /// </summary>
        private void CheckThreshold()
        {
            var handler = ThresholdReached;
            var cmdHandler = ThresholdReachedCommand;
            if (BottomOffsetThreshold.HasValue)
            {
                if ((Target.VerticalOffset + Target.ViewportHeight) >= (Target.ExtentHeight - BottomOffsetThreshold))
                {
                    handler?.Invoke(this, new EventArgs());
                    if (null != cmdHandler && cmdHandler.CanExecute(null))
                        cmdHandler.Execute(null);
                }
            }
            else if (ThresholdPercentage.HasValue)
            {
                if (((Target.VerticalOffset + Target.ViewportHeight) / Target.ExtentHeight) >= ThresholdPercentage)
                {
                    handler?.Invoke(this, new EventArgs());
                    if (null != cmdHandler && cmdHandler.CanExecute(null))
                        cmdHandler.Execute(null);
                }
            }
            else
            {
                if (TopOffsetThreshold.HasValue)
                {
                    if ((Target.VerticalOffset + Target.ViewportHeight) >= TopOffsetThreshold)
                    {
                        handler?.Invoke(this, new EventArgs());
                        if (null != cmdHandler && cmdHandler.CanExecute(null))
                            cmdHandler.Execute(null);
                    }
                }
            }
        }
    }
}