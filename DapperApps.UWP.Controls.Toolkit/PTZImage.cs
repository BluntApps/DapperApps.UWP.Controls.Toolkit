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

namespace DapperApps.Phone.Controls
{
    using System;
    using Windows.Foundation;
    using Windows.UI.Input;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Media;
    using Windows.UI.Xaml.Media.Imaging;

    // TODO fix double tap zoom scale
    // TODO Loaded zoom out
    /// <summary>
    /// A pinch-to-zoomable image.
    /// </summary>
    [TemplatePart(Name = ViewportName, Type = typeof(ViewportControl))]
    [TemplatePart(Name = CanvasName, Type = typeof(Canvas))]
    [TemplatePart(Name = ImageName, Type = typeof(Image))]
    public class PTZImage : ImageControl
    {
        /// <summary>
        /// Template Part name for the ViewPort.
        /// </summary>
        private const string ViewportName = "Viewport";

        /// <summary>
        /// Template Part name for the Image's Container.
        /// </summary>
        private const string CanvasName = "ImagePanel";

        /// <summary>
        /// Template Part name for the Image.
        /// </summary>
        private const string ImageName = "Image";

        /// <summary>
        /// The ratio of the minimum you can scale the image. (So it fits in the viewport perfectly).
        /// </summary>
        private double _minScale;

        /// <summary>
        /// How much a user tries to scale the image by.
        /// </summary>
        private double _scale;

        /// <summary>
        /// The _scale, but within the allowable limits of possible zooming in or out.
        /// </summary>
        private double _coercedScale;

        /// <summary>
        /// 
        /// </summary>
        private double _originalScale;

        /// <summary>
        /// The viewport that controls how much, and what part of the image is seen when scaled up.
        /// </summary>
        private ViewportControl _viewport;

        /// <summary>
        /// The dimensions (width and height) of the current viewport.
        /// </summary>
        private Size _viewportSize;

        /// <summary>
        /// Panel that contains the Image and resizes along with the image.
        /// </summary>
        private Canvas _imagePanel;

        /// <summary>
        /// A bitmap representation of the source image that provides pixel dimensions of such image.
        /// </summary>
        private BitmapSource _bitmap;

        /// <summary>
        /// TODO
        /// </summary>
        private ImageSource _source;

        /// <summary>
        /// Whether the user is currently pinch-to-zooming.
        /// </summary>
        private bool _pinching;

        /// <summary>
        /// Midpoint relative to the entire screen size.
        /// </summary>
        private Point _screenMidpoint;

        /// <summary>
        /// The midpoint of the last user pinch relative to the image dimensions.
        /// </summary>
        private Point _relativeMidpoint;

        //TODO wite other used image dprops.
        #region Source DependencyProperty
        /// <summary>
        /// The source dependency property.
        /// </summary>
        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register(
            "Source",
            typeof(ImageSource),
            typeof(PTZImage),
            new PropertyMetadata(OnSourcePropertyChanged));

        /// <summary>
        /// Gets or sets the source of this ImagePreview control.
        /// </summary>
        public ImageSource Source
        {
            get { return (ImageSource)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="d"></param>
        /// <param name="dpcea"></param>
        private static void OnSourcePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs dpcea)
        {
            PTZImage container = (PTZImage)d;
            Image contained = (Image)container.Image;
            ImageSource source = (ImageSource)dpcea.NewValue;
            if (null != contained)
            {
                contained.Source = container._source = source;
                container.Image_Opened(contained, null);
            }
            else
            {
                container._source = source;
            }
        }
        #endregion Source DependencyProperty

        #region Stretch Dependency Property
        /// <summary>
        /// The stretch dependency property.
        /// </summary>
        public static readonly DependencyProperty StretchProperty =
            DependencyProperty.Register(
            "Stretch",
            typeof(Stretch),
            typeof(PTZImage),
            new PropertyMetadata(Stretch.Uniform));

        /// <summary>
        /// Gets or sets a value that describes how an image control should be stretched to fill the destination rectangle.
        /// The default value is uniform.
        /// </summary>
        public Stretch Stretch
        {
            get { return (Stretch)GetValue(StretchProperty); }
            set { SetValue(StretchProperty, value); }
        }
        #endregion

        #region MaxScale Dependency Property
        /// <summary>
        /// The MaxScale dependency property.
        /// </summary>
        public static readonly DependencyProperty MaxScaleProperty =
            DependencyProperty.Register(
            "MaxScale",
            typeof(double),
            typeof(PTZImage),
            new PropertyMetadata(10.0d));

        /// <summary>
        /// The maximum ratio you can scale/zoom the image.
        /// Defaults to 10.
        /// </summary>
        public double MaxScale
        {
            get
            {
                return (double)GetValue(MaxScaleProperty);
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("MaxScale must be >= 0");
                }
                SetValue(MaxScaleProperty, value);
            }
        }
        #endregion

        #region IsDoubleTapEnabled Dependency Property
        /// <summary>
        /// The IsDoubleTapEnabled Dependency Property.
        /// </summary>
        public static readonly DependencyProperty IsDoubleTapEnabledProperty =
            DependencyProperty.Register(
            "IsDoubleTapEnabled",
            typeof(bool),
            typeof(PTZImage),
            new PropertyMetadata(true));

        /// <summary>
        /// Property that determines whether to scale on double tap.
        /// Default is true.
        /// </summary>
        public bool IsDoubleTapEnabled
        {
            get { return (bool)GetValue(IsDoubleTapEnabledProperty); }
            set { SetValue(IsDoubleTapEnabledProperty, value); }
        }
        #endregion

        /// <summary>
        /// Resets the scaling on the image to fill its container and center the view.
        /// </summary>
        public void ResetScale()
        {
            _coercedScale = _minScale;
            ResizeImage(true);
        }

        /// <summary> 
        /// Adjust the size of the image according to the coerced scale factor. Optionally 
        /// center the image, otherwise, try to keep the original midpoint of the pinch 
        /// in the same spot on the screen regardless of the scale. 
        /// </summary> 
        /// <param name="center">Center the image on the viewport</param> 
        private void ResizeImage(bool center)
        {
            if (_coercedScale != 0
                && null != _bitmap
                && null != _imagePanel
                && null != Image
                && null != _viewport)
            {
                // Store, and change the width and height of the canvas to fit the size of the new image.
                double newWidth = _imagePanel.Width = Math.Round(_bitmap.PixelWidth * _coercedScale);
                double newHeight = _imagePanel.Height = Math.Round(_bitmap.PixelHeight * _coercedScale);

                // Scale the image by _coercedScale amount
                // Values between 0 and 1 decrease the width/height of the scaled object;
                // Values greater than 1 increase the width/height of the scaled object.
                // A value of 1 indicates that the object is not scaled in the x/y-direction.
                // Negative values flip the scaled object horizontally/vertically.
                // Values between 0 and -1 flip the scale object and decrease its width/height.
                // Values less than -1 flip the object and increase its width/height.
                // A value of -1 flips the scaled object but does not change its horizontal/vertical size.
                // Scale both the width and the height simultaneously to preserve the aspect ratio.
                if (null == Image.RenderTransform || !(Image.RenderTransform is ScaleTransform))
                {
                    Image.RenderTransform = new ScaleTransform();
                }
                if (null == Image.RenderTransformOrigin)
                {
                    Image.RenderTransformOrigin = new Point(0.0d, 0.0d);
                }
                ScaleTransform xform = (ScaleTransform)Image.RenderTransform;
                xform.ScaleX = xform.ScaleY = _coercedScale;

                _viewport.Bounds = new Rect(0, 0, newWidth, newHeight);

                if (center)
                {
                    _viewport.SetViewportOrigin(
                        new Point(
                            Math.Round((newWidth - _viewport.ActualWidth) / 2),
                            Math.Round((newHeight - _viewport.ActualHeight) / 2)
                            ));
                }
                else
                {
                    Point newImgMid = new Point(newWidth * _relativeMidpoint.X, newHeight * _relativeMidpoint.Y);
                    Point origin = new Point(newImgMid.X - _screenMidpoint.X, newImgMid.Y - _screenMidpoint.Y);
                    _viewport.SetViewportOrigin(origin);
                }
            }
        }

        /// <summary> 
        /// Coerce the scale into being within the proper range. Optionally compute the constraints  
        /// on the scale so that it will always fill the entire screen and will never get too big  
        /// to be contained in the viewport. 
        /// </summary>
        /// <param name="recompute">Will recompute the min scale if true.</param>
        /// <inputs>
        /// _viewport, _bitmap, recompute, MaxScale, _scale
        /// </inputs>
        /// <outputs>
        /// _coercedScale, _minScale
        /// </outputs>
        private void CoerceScale(bool recompute)
        {
            if (recompute && null != _bitmap && null != _viewport)
            {
                // Calculate the minimum scale to fit the viewport
                double minX = _viewport.ActualWidth / _bitmap.PixelWidth;
                double minY = _viewport.ActualHeight / _bitmap.PixelHeight;

                // The ratio you need to scale the image bigger or smaller so it fits perfectly in view.
                // So the image isn't zoomed out more than the view...
                _minScale = Math.Min(minX, minY);
            }

            _coercedScale = Math.Min(MaxScale, Math.Max(_scale, _minScale));
        }

        /// <summary>
        /// Gets the template parts for this PTZImage and sets up
        /// references and target properties for such parts.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _viewport = this.GetTemplateChild(ViewportName) as ViewportControl;
            _imagePanel = this.GetTemplateChild(CanvasName) as Canvas;
            Image = this.GetTemplateChild(ImageName) as Image;

            if (null != _viewport)
            {
                _viewport.ViewportChanged += Viewport_Changed;
                _viewport.ManipulationStarted += Viewport_ManipulationStarted;
                _viewport.ManipulationDelta += Viewport_ManipulationDelta;
                _viewport.ManipulationCompleted += Viewport_ManipulationCompleted;
            }

            if (null != Image)
            {
                Image.ImageOpened += Image_Opened;
                if (null != _source)
                {
                    Image.Source = _source;
                    Image_Opened(Image, null);
                }
            }
        }

        /// <summary>
        /// Initializes an instance of a PTZImage.
        /// </summary>
        public PTZImage()
            : base()
        {
            this.DefaultStyleKey = typeof(PTZImage);

            // The initial scale is the full ratio/size of the image.
            _scale = 1.0d;
            DoubleTap += PTZImage_DoubleTap;
        }

        /// <summary>
        /// Initializes a new instance of the PTZImage
        /// class with the given ImageSource.
        /// </summary>
        public PTZImage(ImageSource source)
            : this()
        {
            this.Source = source;
        }

        /// <summary>
        /// Initializes an instance of a PTZImage with a
        /// BitmapImage source with given uri source.
        /// </summary>
        /// <param name="uri">The uri source for this PTZImage.</param>
        public PTZImage(Uri uri)
            : this(new BitmapImage(uri)) { }

        /// <summary>
        /// Initializes an instance of a PTZImage with a
        /// BitmapImage source with given path.
        /// </summary>
        /// <param name="path">The uri source for this PTZImage.</param>
        public PTZImage(string path)
            : this(new Uri(path, UriKind.RelativeOrAbsolute)) { }

        /// <summary>
        /// Function called when the user has double tapped the zoomimage.
        /// </summary>
        /// <param name="sender">sender of the event - this will be the tilt 
        /// container (eg, entire page).</param>
        /// <param name="e">Event arguments.</param>
        private void PTZImage_DoubleTap(object sender, GestureEventArgs e)
        {
            if (IsDoubleTapEnabled && null != Image && null != _viewport)
            {
                if (_scale <= _minScale)
                {
                    Point center = e.GetPosition(Image);
                    _relativeMidpoint = new Point(center.X / Image.ActualWidth, center.Y / Image.ActualHeight);

                    var xform = Image.TransformToVisual(_viewport);
                    _screenMidpoint = xform.Transform(center);

                    _scale = _minScale + (0.000806452 * _imagePanel.Width - 0.037097);
                    CoerceScale(false);
                    ResizeImage(false);
                }
                else
                {
                    ResetScale();
                }
            }
        }

        /// <summary>
        /// Either the user has manipulated the image or the size of the viewport has changed. We only
        /// care about the size.
        /// </summary>
        private void Viewport_Changed(object sender, ViewportChangedEventArgs e)
        {
            // Width and height of the viewport
            Size newSize = new Size(_viewport.Viewport.Width, _viewport.Viewport.Height);
            if (_viewportSize != newSize)
            {
                _viewportSize = newSize;
                CoerceScale(true);
                _coercedScale = _minScale;
                ResizeImage(false);
            }
        }

        /// <summary> 
        /// Handler for the ManipulationStarted event. Set initial state in case 
        /// it becomes a pinch later. 
        /// </summary> 
        private void Viewport_ManipulationStarted(object sender, ManipulationStartedEventArgs e)
        {
            _pinching = false;
            _originalScale = _scale;
        }

        /// <summary> 
        /// Handler for the ManipulationDelta event. It may or may not be a pinch. If it is not a  
        /// pinch, the ViewportControl will take care of it. 
        /// </summary> 
        /// <param name="sender"></param> 
        /// <param name="e"></param> 
        private void Viewport_ManipulationDelta(object sender, ManipulationDeltaEventArgs e)
        {
            if (null != Image && null != _viewport)
            {
                if (e.PinchManipulation != null)
                {
                    e.Handled = true;

                    if (!_pinching)
                    {
                        _pinching = true;
                        Point center = e.PinchManipulation.Original.Center;
                        _relativeMidpoint = new Point(center.X / Image.ActualWidth, center.Y / Image.ActualHeight);

                        var xform = Image.TransformToVisual(_viewport);
                        _screenMidpoint = xform.Transform(center);
                    }

                    _scale = _originalScale * e.PinchManipulation.CumulativeScale;

                    CoerceScale(false);
                    ResizeImage(false);
                }
                else
                {
                    if (_pinching)
                    {
                        _pinching = false;
                        _originalScale = _scale = _coercedScale;
                    }
                }
            }
        }

        /// <summary> 
        /// The manipulation has completed (no touch points anymore) so reset state. 
        /// </summary> 
        private void Viewport_ManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
        {
            _pinching = false;
            _scale = _coercedScale;
        }

        /// <summary> 
        /// When a new image is opened, set its initial scale. 
        /// </summary> 
        private void Image_Opened(object sender, RoutedEventArgs e)
        {
            Image img = sender as Image;
            if (null != img && null != img.Source)
            {
                _bitmap = (BitmapSource)img.Source;

                // Set scale to the minimum, and then save it.
                _scale = 0;
                CoerceScale(true);
                _scale = _coercedScale;
                ResizeImage(true);
            }
        }
    }
}