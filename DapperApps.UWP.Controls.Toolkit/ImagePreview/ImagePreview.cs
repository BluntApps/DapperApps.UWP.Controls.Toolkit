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
//    using Windows.UI.Xaml.Media;
//    using Windows.UI.Xaml.Media.Animation;
//    using Windows.UI.Xaml.Media.Imaging;

//    // TODO Basic Lens SDK virtualized pivot
//    /// <summary>
//    /// An animated, scrolling, preview of an Image.
//    /// </summary>
//    [TemplateVisualState(Name = DisabledStateName, GroupName = CommonStatesName)]
//    [TemplateVisualState(Name = NormalStateName, GroupName = CommonStatesName)]
//    [TemplatePart(Name = ViewportName, Type = typeof(ViewportControl))]
//    [TemplatePart(Name = ImagePanelName, Type = typeof(Canvas))]
//    [TemplatePart(Name = ImageName, Type = typeof(Image))]
//    [TemplatePart(Name = PreviewTransformName, Type = typeof(TranslateTransform))]
//    public class ImagePreview : ImageControl
//    {
//        private const string CommonStatesName = "CommonStates";
//        private const string DisabledStateName = "Disabled";
//        private const string NormalStateName = "Normal";
//        private const string PreviewStoryboardName = "PreviewStoryboard";
//        private const string PreviewAnimationName = "PreviewAnimation";
//        private const string PreviewTransformName = "PreviewTransform";
//        private const string ViewportName = "Viewport";
//        private const string ImagePanelName = "ImagePanel";
//        private const string ImageName = "Image";
//        private const double pixelsPerSecond = 88.8d;

//        private FrameworkElement _root;
//        private ViewportControl _viewport;
//        private Canvas _imagePanel;
//        private BitmapSource _imageBitmap;
//        private ImageSource _source;
//        private VisualStateGroup _commonStates;
//        private VisualState _disabledState;
//        private VisualState _normalState;
//        private Storyboard _previewStoryboard;
//        private DoubleAnimation _previewAnimation;
//        private TranslateTransform _previewTransform;

//        //TODO wite other used image dprops.
//        #region Source DependencyProperty
//        /// <summary>
//        /// The source dependency property.
//        /// </summary>
//        public static readonly DependencyProperty SourceProperty =
//            DependencyProperty.Register(
//            "Source",
//            typeof(ImageSource),
//            typeof(ImagePreview),
//            new PropertyMetadata(OnSourcePropertyChanged));

//        /// <summary>
//        /// Gets or sets the source of this ImagePreview control.
//        /// </summary>
//        public ImageSource Source
//        {
//            get { return (ImageSource)GetValue(SourceProperty); }
//            set { SetValue(SourceProperty, value); }
//        }

//        /// <summary>
//        /// TODO
//        /// </summary>
//        /// <param name="d"></param>
//        /// <param name="dpcea"></param>
//        private static void OnSourcePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs dpcea)
//        {
//            ImagePreview container = (ImagePreview)d;
//            Image contained = (Image)container.Image;
//            ImageSource source = (ImageSource)dpcea.NewValue;
//            if (null != contained)
//            {
//                contained.Source = container._source = source;
//                container.Image_Opened(contained, null);
//            }
//            else
//            {
//                container._source = source;
//            }
//        }
//        #endregion Source DependencyProperty

//        #region Header Dependency Property
//        /// <summary>
//        /// Gets or sets the content for the header of the control.
//        /// </summary>
//        /// <value>
//        /// The content for the header of the control. The default value is
//        /// null.
//        /// </value>
//        public object Header
//        {
//            get { return GetValue(HeaderProperty); }
//            set { SetValue(HeaderProperty, value); }
//        }

//        /// <summary>
//        /// Identifies the
//        /// <see cref="P:System.Windows.Controls.HeaderedContentControl.Header" />
//        /// dependency property.
//        /// </summary>
//        /// <value>
//        /// The identifier for the
//        /// <see cref="P:System.Windows.Controls.HeaderedContentControl.Header" />
//        /// dependency property.
//        /// </value>
//        public static readonly DependencyProperty HeaderProperty =
//                DependencyProperty.Register(
//                        "Header",
//                        typeof(object),
//                        typeof(ImagePreview),
//                        new PropertyMetadata(null));
//        #endregion

//        #region HeaderTemplate Dependency Property
//        /// <summary>
//        /// Gets or sets the template that is used to display the content of the
//        /// control's header.
//        /// </summary>
//        /// <value>
//        /// The template that is used to display the content of the control's
//        /// header. The default is null.
//        /// </value>
//        public DataTemplate HeaderTemplate
//        {
//            get { return (DataTemplate)GetValue(HeaderTemplateProperty); }
//            set { SetValue(HeaderTemplateProperty, value); }
//        }

//        /// <summary>
//        /// Identifies the
//        /// <see cref="P:System.Windows.Controls.HeaderedContentControl.HeaderTemplate" />
//        /// dependency property.
//        /// </summary>
//        /// <value>
//        /// The identifier for the
//        /// <see cref="P:System.Windows.Controls.HeaderedContentControl.HeaderTemplate" />
//        /// dependency property.
//        /// </value>
//        public static readonly DependencyProperty HeaderTemplateProperty =
//                DependencyProperty.Register(
//                        "HeaderTemplate",
//                        typeof(DataTemplate),
//                        typeof(ImagePreview),
//                        new PropertyMetadata(null));
//        #endregion

//        #region IsFrozen DependencyProperty

//        /// <summary>
//        /// Gets or sets whether to animate this ImagePreview
//        /// </summary>
//        public bool IsFrozen
//        {
//            get { return (bool)GetValue(IsFrozenProperty); }
//            set { SetValue(IsFrozenProperty, value); }
//        }

//        /// <summary>
//        /// Identifies the IsFrozen dependency property.
//        /// </summary>
//        public static readonly DependencyProperty IsFrozenProperty =
//            DependencyProperty.Register("IsFrozen",
//            typeof(bool),
//            typeof(ImagePreview),
//            new PropertyMetadata(false, new PropertyChangedCallback(IsFrozen_Changed)));

//        /// <summary>
//        /// Puts an ImagePreview in the normal or disabled state.
//        /// </summary>
//        /// <param name="obj">The dependency object.</param>
//        /// <param name="e">The event information.</param>
//        private static void IsFrozen_Changed(DependencyObject obj, DependencyPropertyChangedEventArgs e)
//        {
//            ImagePreview imagePreview = obj as ImagePreview;

//            if (null != imagePreview)
//            {
//                imagePreview.UpdateState();
//            }
//        }

//        #endregion IsFrozenDependencyProperty

//        /// <summary>
//        /// Initializes a new instance of the ImagePreview class.
//        /// </summary>
//        public ImagePreview()
//            : base()
//        {
//            this.DefaultStyleKey = typeof(ImagePreview);
//        }

//        /// <summary>
//        /// Initializes a new instance of the ImagePreview class
//        /// with the given ImageSource.
//        /// </summary>
//        public ImagePreview(ImageSource source)
//            : this()
//        {
//            this.Source = source;
//        }

//        /// <summary>
//        /// Initializes a new instance of the ImagePreview class
//        /// with the given a BitmapImage ImageSource with the given uri.
//        /// </summary>
//        /// <param name="uri">The uri source for this ImagePreview.</param>
//        public ImagePreview(Uri uri)
//            : this(new BitmapImage(uri)) { }

//        /// <summary>
//        /// Initializes a new instance of the ImagePreview class
//        /// with a BitmapImage ImageSource with a uri to the given path.
//        /// </summary>
//        /// <param name="path">The uri source path for this ImagePreview.</param>
//        public ImagePreview(string path)
//            : this(new Uri(path, UriKind.RelativeOrAbsolute)) { }

//        /// <summary>
//        /// Attempts to freeze all existing ImagePreviews within root or any of it's subchildren.
//        /// </summary>
//        /// <param name="root">The root element to begin to freeze.</param>
//        public static void FreezePreviews(FrameworkElement root)
//        {
//            TogglePreviews(root, true);
//        }

//        /// <summary>
//        /// Attempts to unfreeze all existing ImagePreviews within root or any of it's subchildren.
//        /// </summary>
//        /// <param name="root">The root element to begin to unfreeze.</param>
//        public static void UnfreezePreviews(FrameworkElement root)
//        {
//            TogglePreviews(root, false);
//        }

//        private static void TogglePreviews(FrameworkElement root, bool value)
//        {
//            // Base case 1
//            if (null != root)
//            {
//                var imagePreviewRoot = root as ImagePreview;
//                var contentControlRoot = root as ContentControl;
//                var itemsControlRoot = root as ItemsControl;
//                var panelRoot = root as Panel;
//                var borderRoot = root as Border;
//                var contentPresenterRoot = root as ContentPresenter;
//                var popupRoot = root as Popup;
//                var viewBoxRoot = root as Viewbox;

//                // Base case 2
//                if (null != imagePreviewRoot)
//                {
//                    imagePreviewRoot.IsFrozen = value;
//                }
//                else
//                {
//                    if (null != contentControlRoot)
//                    {
//                        if (null == contentControlRoot.ContentTemplate)
//                        {
//                            TogglePreviews(contentControlRoot.Content as FrameworkElement, value);
//                        }
//                    }
//                    else if (null != itemsControlRoot)
//                    {
//                        if (null == itemsControlRoot.ItemTemplate)
//                        {
//                            foreach (object item in itemsControlRoot.Items)
//                            {
//                                TogglePreviews(item as FrameworkElement, value);
//                            }
//                        }
//                        else
//                        {
//                            for (int i = 0; i < itemsControlRoot.Items.Count; i++)
//                            {
//                                TogglePreviews(itemsControlRoot.ItemContainerGenerator.ContainerFromIndex(i) as FrameworkElement, value);
//                            }
//                        }
//                    }
//                    else if (null != panelRoot)
//                    {
//                        foreach (UIElement child in panelRoot.Children)
//                        {
//                            TogglePreviews(child as FrameworkElement, value);
//                        }
//                    }
//                    else if (null != borderRoot)
//                    {
//                        TogglePreviews(borderRoot.Child as FrameworkElement, value);
//                    }
//                    else if (null != contentPresenterRoot)
//                    {
//                        TogglePreviews(contentPresenterRoot.Content as FrameworkElement, value);
//                    }
//                    else if (null != popupRoot)
//                    {
//                        TogglePreviews(popupRoot.Child as FrameworkElement, value);
//                    }
//                    else
//                    {
//                        if (null != viewBoxRoot)
//                        {
//                            TogglePreviews(viewBoxRoot.Child as FrameworkElement, value);
//                        }
//                    }
//                }
//            }
//        }

//        /// <summary>
//        /// Scales the ImagePanel such that its height matches the height of the Viewport.
//        /// </summary>
//        private void ScaleImage()
//        {
//            Transform imageTransform = Image.RenderTransform;
//            if (null == imageTransform || !(imageTransform is ScaleTransform))
//            {
//                imageTransform = Image.RenderTransform = new ScaleTransform();
//            }

//            if (null == Image.RenderTransformOrigin)
//            {
//                Image.RenderTransformOrigin = new Point(0.0d, 0.0d);
//            }

//            if (_imageBitmap.PixelHeight > 0 && _imageBitmap.PixelWidth > 0)
//            {
//                double scale = _viewport.ActualHeight / _imageBitmap.PixelHeight;

//                double newWidth = _imagePanel.Width = Math.Round(_imageBitmap.PixelWidth * scale);
//                double newHeight = _imagePanel.Height = Math.Round(_imageBitmap.PixelHeight * scale);

//                ScaleTransform transform = (ScaleTransform)Image.RenderTransform;
//                transform.ScaleX = transform.ScaleY = scale;

//                // Set viewport bounds to the size of the viewport to prevent scrolling.
//                _viewport.Bounds = new Rect(0, 0, _viewport.ActualWidth, _viewport.ActualHeight);
//            }
//        }

//        private void UpdateState()
//        {
//            if (null != _previewStoryboard)
//            {
//                if (IsFrozen)
//                {
//                    _previewStoryboard.Pause();
//                }
//                else
//                {
//                    _previewStoryboard.Resume();
//                }
//            }
//        }

//        /// <summary>
//        /// Calculates the preview animation for this ImagePreview, and transitions to
//        /// its NormalState, running the calculated animation.
//        /// </summary>
//        private void CalculateAndAnimate()
//        {
//            if (null != _imagePanel)
//            {
//                Transform previewTransform = _imagePanel.RenderTransform;
//                if (null == previewTransform || !(previewTransform is TranslateTransform))
//                {
//                    previewTransform = _imagePanel.RenderTransform = new TranslateTransform();
//                }

//                if (null == _imagePanel.RenderTransformOrigin)
//                {
//                    _imagePanel.RenderTransformOrigin = new Point(0.0d, 0.0d);
//                }

//                double imageWidth = _imagePanel.Width;
//                double viewportWidth = _viewport.ActualWidth;
//                // If picture is smaller than viewport, dont animate.
//                if (imageWidth > viewportWidth)
//                {
//                    double seconds = imageWidth / pixelsPerSecond;
//                    _previewAnimation.Duration = new Duration(TimeSpan.FromSeconds(seconds));
//                    _previewAnimation.From = 0.0d;
//                    _previewAnimation.To = -(imageWidth - viewportWidth);
//                    VisualStateManager.GoToState(this, NormalStateName, true);
//                    UpdateState();
//                }
//            }
//        }

//        /// <summary>
//        /// Gets the template parts for this ImagePreview and sets up
//        /// references and target properties for such parts.
//        /// </summary>
//        public override void OnApplyTemplate()
//        {
//            base.OnApplyTemplate();

//            _viewport = this.GetTemplateChild(ViewportName) as ViewportControl;
//            _imagePanel = this.GetTemplateChild(ImagePanelName) as Canvas;
//            Image = this.GetTemplateChild(ImageName) as Image;
//            _root = this.GetTemplateRoot() as FrameworkElement;
//            _commonStates = this.GetTemplateVisualStateGroup(CommonStatesName);
//            _disabledState = this.GetTemplateVisualState(DisabledStateName);
//            _normalState = this.GetTemplateVisualState(NormalStateName);
//            _previewTransform = this.GetTemplateChild(PreviewTransformName) as TranslateTransform;

//            if (null != _normalState)
//            {
//                _previewStoryboard = _normalState.Storyboard;
//            }

//            if (null != _previewStoryboard)
//            {
//                _previewAnimation = _previewStoryboard.Children[0] as DoubleAnimation;
//            }

//            if (null != _viewport)
//            {
//                _viewport.SizeChanged += Viewport_SizeChanged;
//            }

//            if (null != Image)
//            {
//                ImageOpened += Image_Opened;
//                if (null != _source)
//                {
//                    Image.Source = _source;
//                    Image_Opened(Image, null);
//                }
//            }
//        }

//        /// <summary>
//        /// Function called when the image is finished loading.
//        /// </summary>
//        /// <param name="sender"></param>
//        /// <param name="e"></param>
//        private void Image_Opened(object sender, RoutedEventArgs e)
//        {
//            if (null != Image)
//            {
//                _imageBitmap = (BitmapSource)Image.Source;
//                ScaleImage();
//                CalculateAndAnimate();
//            }
//        }

//        /// <summary>
//        /// Function called whenever the size of the ViewportControl changes
//        /// Including when it first gets rendered.
//        /// </summary>
//        /// <param name="sender">The viewport.</param>
//        /// <param name="e">Arguments for this handler.</param>
//        private void Viewport_SizeChanged(object sender, SizeChangedEventArgs e)
//        {
//            // Make sure the image has loaded before attempting to scale it.
//            if (null != Image && null != _imageBitmap)
//            {
//                ScaleImage();
//                CalculateAndAnimate();
//            }
//        }
//    }
//}