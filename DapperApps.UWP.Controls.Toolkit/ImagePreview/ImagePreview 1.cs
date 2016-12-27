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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace DapperApps.Phone.Controls
{
    /// <summary>
    /// An animated, scrolling, preview of an Image.
    /// </summary>
    [TemplateVisualState(Name = NormalStateName, GroupName = CommonStatesName)]
    [TemplatePart(Name = ViewportName, Type = typeof(ViewportControl))]
    [TemplatePart(Name = ImagePanelName, Type = typeof(Canvas))]
    [TemplatePart(Name = ImageName, Type = typeof(Image))]
    public class ImagePreview : Control
    {
        /// <summary>
        /// Common visual states.
        /// </summary>
        private const string CommonStatesName = "CommonStates";

        /// <summary>
        /// Default visual state.
        /// </summary>
        private const string NormalStateName = "Normal";

        /// <summary>
        /// Template Part name for the ViewPort.
        /// </summary>
        private const string ViewportName = "Viewport";

        /// <summary>
        /// Template Part name for the Image's Container.
        /// </summary>
        private const string ImagePanelName = "ImagePanel";

        /// <summary>
        /// Template Part name for the Image.
        /// </summary>
        private const string ImageName = "Image";

        /// <summary>
        /// The constant rate of change for previewing.
        /// </summary>
        private const double pixelsPerSecond = 88.8d;

        /// <summary>
        /// The viewport that controls how much, and what part of the image is seen when scaled up.
        /// </summary>
        private ViewportControl _viewport;

        /// <summary>
        /// Panel that contains the Image and resizes along with the image.
        /// </summary>
        private Canvas _imagePanel;

        /// <summary>
        /// The source image for this PTZImage.
        /// </summary>
        private Image _image;

        /// <summary>
        /// The source image for this PTZImage.
        /// </summary>
        private BitmapImage _bitmap;

        private VisualStateGroup _commonStates;

        /// <summary>
        /// The normal visual state for this ImagePreview.
        /// </summary>
        private VisualState _normalState;

        /// <summary>
        /// The storyboard containing the preview animations.
        /// </summary>
        private Storyboard _previewStoryboard;

        /// <summary>
        /// The preview animation for this ImagePreview.
        /// </summary>
        private DoubleAnimation _previewAnimation;

        #region Source DependencyProperty
        /// <summary>
        /// The source dependency property.
        /// </summary>
        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register(
            "Source",
            typeof(ImageSource),
            typeof(ImagePreview),
            new PropertyMetadata(null));

        /// <summary>
        /// Gets or sets the source of this ImagePreview control.
        /// </summary>
        public ImageSource Source
        {
            get { return (ImageSource)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }
        #endregion Source DependencyProperty

        //#region IsFrozen DependencyProperty

        ///// <summary>
        ///// Gets or sets whether to animate this ImagePreview
        ///// </summary>
        //public bool IsFrozen
        //{
        //    get { return (bool)GetValue(IsFrozenProperty); }
        //    set { SetValue(IsFrozenProperty, value); }
        //}

        ///// <summary>
        ///// Identifies the IsFrozen dependency property.
        ///// </summary>
        //public static readonly DependencyProperty IsFrozenProperty =
        //    DependencyProperty.Register("IsFrozen",
        //    typeof(bool),
        //    typeof(ImagePreview),
        //    new PropertyMetadata(false, new PropertyChangedCallback(IsFrozen_Changed)));

        ///// <summary>
        ///// Removes the frozen image from the enabled image pool or the stalled image pipeline.
        ///// Adds the non-frozen image to the enabled image pool.  
        ///// </summary>
        ///// <param name="obj">The dependency object.</param>
        ///// <param name="e">The event information.</param>
        //private static void IsFrozen_Changed(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        //{
        //    HubTile tile = (HubTile)obj;

        //    if ((bool)e.NewValue)
        //    {
        //        HubTileService.FreezeHubTile(tile);
        //    }
        //    else
        //    {
        //        HubTileService.UnfreezeHubTile(tile);
        //    }
        //}

        //#endregion IsFrozenDependencyProperty

        /// <summary>
        /// Scales the ImagePanel such that its height matches the height of the Viewport.
        /// </summary>
        private void ScaleImage()
        {
            // Make sure the image has loaded before attempting to scale it.
            if (null != _image && null != _bitmap)
            {
                Transform imageTransform = _image.RenderTransform;
                if (null == imageTransform || !(imageTransform is ScaleTransform))
                {
                    imageTransform = _image.RenderTransform = new ScaleTransform();
                }

                if (null == _image.RenderTransformOrigin)
                {
                    _image.RenderTransformOrigin = new Point(0.0d, 0.0d);
                }

                double scale = _viewport.ActualHeight / _bitmap.PixelHeight;

                double newWidth = _imagePanel.Width = Math.Round(_bitmap.PixelWidth * scale);
                double newHeight = _imagePanel.Height = Math.Round(_bitmap.PixelHeight * scale);

                ScaleTransform transform = (ScaleTransform)_image.RenderTransform;
                transform.ScaleX = transform.ScaleY = scale;

                _viewport.Bounds = new Rect(0, 0, newWidth, newHeight);
            }
        }

        /// <summary>
        /// Sets the animation for this ImagePreview's animated scrolling effect.
        /// </summary>
        private void SetAnimation()
        {
            if (null != _previewAnimation)
            {
                double imageWidth = _imagePanel.ActualWidth;
                double viewportWidth = _viewport.ActualWidth;
                // If picture is smaller than viewport, dont animate.
                if (imageWidth <= viewportWidth)
                {
                    double seconds = imageWidth / pixelsPerSecond;
                    _previewAnimation.Duration = new Duration(TimeSpan.FromSeconds(seconds));
                    _previewAnimation.From = 0.0d;
                    _previewAnimation.To = -(imageWidth - viewportWidth);
                }
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _viewport = this.GetTemplateChild(ViewportName) as ViewportControl;
            _imagePanel = this.GetTemplateChild(ImagePanelName) as Canvas;
            _image = this.GetTemplateChild(ImageName) as Image;

            if (null != _imagePanel)
            {
                Storyboard.SetTarget(_previewStoryboard, _imagePanel);
                Storyboard.SetTargetProperty(_previewStoryboard,
                    new PropertyPath("(UIElement.RenderTransform).(TranslateTransform.X)"));
            }

            if (null != _viewport)
            {
                _viewport.SizeChanged += Viewport_SizeChanged;
                _viewport.IsHitTestVisible = false;
            }

            if (null != _image)
            {
                _image.Loaded += Image_loaded;
            }
        }

        public ImagePreview()
        {
            this.DefaultStyleKey = typeof(ImagePreview);
            // Preview storyboard and animation.
            _previewAnimation = new DoubleAnimation();
            _previewStoryboard = new Storyboard
            {
                RepeatBehavior = RepeatBehavior.Forever,
                AutoReverse = true
            };
            _previewStoryboard.Children.Add(_previewAnimation);

            // Normal visual state and common state group.
            _commonStates = new VisualStateGroup();
            _commonStates.SetValue(FrameworkElement.NameProperty, CommonStatesName);
            _normalState = new VisualState();
            _normalState.SetValue(FrameworkElement.NameProperty, NormalStateName);
            _commonStates.States.Add(_normalState);
            _normalState.Storyboard = _previewStoryboard;
            VisualStateManager.GetVisualStateGroups(this).Add(_commonStates);

            Loaded += ImagePreview_Loaded;
            Unloaded += ImagePreview_Unloaded;
        }

        /// <summary>
        /// This event handler gets called when an ImagePreview is added to the visual tree.
        /// </summary>
        /// <param name="sender">The ImagePreview that was added.</param>
        /// <param name="e">The event arguments.</param>
        private void ImagePreview_Loaded(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// This event handler gets called when an ImagePreview is removed from the visual tree.
        /// </summary>
        /// <param name="sender">The ImagePreview that was added.</param>
        /// <param name="e">The event arguments.</param>
        private void ImagePreview_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Function called when the image is finished loading.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Image_loaded(object sender, RoutedEventArgs e)
        {
            _bitmap = (BitmapImage)_image.Source;
            ScaleImage();
            SetAnimation();
            VisualStateManager.GoToState(this, NormalStateName, true);
        }

        /// <summary>
        /// Function called whenever the size of the ViewportControl changes
        /// Including when it first gets rendered.
        /// </summary>
        /// <param name="sender">The viewport.</param>
        /// <param name="e">Arguments for this handler.</param>
        private void Viewport_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ScaleImage();
            SetAnimation();
            VisualStateManager.GoToState(this, NormalStateName, true);
        }
    }
}
