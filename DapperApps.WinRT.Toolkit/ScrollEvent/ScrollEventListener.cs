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

using System;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
// ReSharper disable NotResolvedInText

namespace DapperApps.WinRT.Toolkit.ScrollEvent
{
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
        /// The target ScrollViewer whose VerticalOffset property to listen to.
        /// </summary>
        private ScrollViewer _target;

        /// <summary>
        /// The Binding to listen to the ScrollViewer's VeritcalOffset dependency property.
        /// </summary>
        private Binding _listener;

        private bool _bottomOffsetThresholdWasSet;

        private bool _topOffsetThresholdWasSet;

        private bool _thresholdPercentageWasSet;

        public ScrollEventListener()
        {
            _bottomOffsetThresholdWasSet = false;
            _topOffsetThresholdWasSet = false;
            _thresholdPercentageWasSet = false;
        }

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
                if (null == value) return;
                _target = value;
                AttachBindingListener();
                _target.Loaded +=
                    (s, e) =>
                    {
                        CheckThreshold();
                    };
            }
        }

        #region BottomOffsetThreshold Dependency Property
        /// <summary>
        /// Offset, in pixels, from the bottom, to be reached.
        /// </summary>
        public double BottomOffsetThreshold
        {
            get { return (double)GetValue(BottomOffsetThresholdProperty); }
            set
            {
                if (_topOffsetThresholdWasSet)
                    throw new InvalidOperationException("You cannot set both bottom and top offset values.");
                if (_thresholdPercentageWasSet)
                    throw new InvalidOperationException("You cannot set both bottom and percent threshold values.");
                if (value > 1.0 || value < 0.0)
                    throw new ArgumentOutOfRangeException("BottomOffsetThreshold");
                _bottomOffsetThresholdWasSet = true;
                SetValue(BottomOffsetThresholdProperty, value);
            }
        }

        /// <summary>
        /// Dependency property for the offset, in pixels, from the bottom, to be reached.
        /// </summary>
        public static readonly DependencyProperty BottomOffsetThresholdProperty =
            DependencyProperty.Register(
                "BottomOffsetThreshold",
                typeof(double),
                typeof(ScrollEventListener),
                new PropertyMetadata(0.0));
        #endregion

        #region TopOffsetThreshold Dependency Property
        /// <summary>
        /// Offset, in pixels, from the top, to be reached.
        /// </summary>
        public double TopOffsetThreshold
        {
            get { return (double)GetValue(TopOffsetThresholdProperty); }
            set
            {
                if (_bottomOffsetThresholdWasSet)
                    throw new InvalidOperationException("You cannot set both a top and bottom offset values.");
                if (_thresholdPercentageWasSet)
                    throw new InvalidOperationException("You cannot set both top and percent threshold values.");
                if (value > Target.ExtentHeight && value < 0.0)
                    throw new ArgumentOutOfRangeException("TopOffsetThreshold");
                _topOffsetThresholdWasSet = true;
                SetValue(TopOffsetThresholdProperty, value);
            }
        }

        /// <summary>
        /// Dependency property for the offset, in pixels, from the top, to be reached.
        /// </summary>
        public static readonly DependencyProperty TopOffsetThresholdProperty =
            DependencyProperty.Register(
                "TopOffsetThreshold",
                typeof(double),
                typeof(ScrollEventListener),
                new PropertyMetadata(0.0));
        #endregion

        #region ThresholdPercentage Dependency Property
        /// <summary>
        /// The scrolling percentage, between 0 and 1, needed before reaching the threshold.
        /// </summary>
        public double ThresholdPercentage
        {
            get
            {
                return (double)GetValue(ThresholdPercentageProperty);
            }
            set
            {
                if (_bottomOffsetThresholdWasSet)
                    throw new InvalidOperationException("You cannot set both percent and bottom threshold values.");
                if (_topOffsetThresholdWasSet)
                    throw new InvalidOperationException("You cannot set both percent and top threshold values.");
                if (value < 0.0 || value > 1.0)
                    throw new ArgumentOutOfRangeException("ThresholdPercentage");
                _thresholdPercentageWasSet = true;
                SetValue(ThresholdPercentageProperty, value);
            }
        }

        /// <summary>
        /// The scrolling amount, between 0 and 1, needed before reaching the threshold.
        /// </summary>
        public static readonly DependencyProperty ThresholdPercentageProperty =
            DependencyProperty.Register(
                "ScrollPercentage",
                typeof(double),
                typeof(ScrollEventListener),
                new PropertyMetadata(0.0));
        #endregion

        #region VerticalOffsetBinding Dependency Property
        /// <summary>
        /// A property to bind to the Target's VerticalOffsetProperty.
        /// </summary>
        // ReSharper disable once UnusedMember.Local
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
                new PropertyMetadata(null, CommandChanged));
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
                new PropertyMetadata(true, IsEnabledChanged));
        #endregion

        private static void CommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var sel = (ScrollEventListener)d;
            sel.HookUpCommand((ICommand)e.OldValue, (ICommand)e.NewValue);
        }

        /// <summary>
        /// Binds the VerticalOffsetBinding dependency property to
        /// the ScrollViewer's VerticalOffset property to recieve callbacks.
        /// </summary>
        private void AttachBindingListener()
        {
            if (null != _listener) return;
            _listener = new Binding
            {
                Source = Target,
                Path = new PropertyPath("VerticalOffset"),
                Mode = BindingMode.OneWay,
            };
            BindingOperations.SetBinding(this, VerticalOffsetBindingProperty, _listener);
        }

        private void DetachBindingListener()
        {
            if (null == _listener) return;
            BindingOperations.SetBinding(this, VerticalOffsetBindingProperty, null);
            _listener = null;
        }

        private static void IsEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var sel = (ScrollEventListener)d;
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
            if (null == ThresholdReachedCommand) return;
            IsEnabled = ThresholdReachedCommand.CanExecute(null);
        }

        /// <summary>
        /// Function called whenever the VerticalOffset property of the Target changes.
        /// </summary>
        private static void OnVerticalOffsetChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ((ScrollEventListener) sender)?.CheckThreshold();
        }

        /// <summary>
        /// Defines the logic performed to check if a threshold has been reached.
        /// </summary>
        private void CheckThreshold()
        {
            var handler = ThresholdReached;
            var cmdHandler = ThresholdReachedCommand;
            if (_bottomOffsetThresholdWasSet)
            {
                if (Target.VerticalOffset + Target.ViewportHeight
                    < Target.ExtentHeight - BottomOffsetThreshold) return;
                handler?.Invoke(this, new EventArgs());
                if (null != cmdHandler && cmdHandler.CanExecute(null))
                    cmdHandler.Execute(null);
            }
            else if (_thresholdPercentageWasSet)
            {
                if ((Target.VerticalOffset + Target.ViewportHeight) / Target.ExtentHeight
                    < ThresholdPercentage) return;
                handler?.Invoke(this, new EventArgs());
                if (null != cmdHandler && cmdHandler.CanExecute(null))
                    cmdHandler.Execute(null);
            }
            else
            {
                if (!_topOffsetThresholdWasSet) return;
                if (Target.VerticalOffset + Target.ViewportHeight
                    < TopOffsetThreshold) return;
                handler?.Invoke(this, new EventArgs());
                if (null != cmdHandler && cmdHandler.CanExecute(null))
                    cmdHandler.Execute(null);
            }
        }
    }
}