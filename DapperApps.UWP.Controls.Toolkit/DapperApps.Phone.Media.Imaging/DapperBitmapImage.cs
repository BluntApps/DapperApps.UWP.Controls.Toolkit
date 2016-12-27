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

namespace DapperApps.Media.Imaging
{
    using System;
    using System.IO;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Media.Imaging;

    /// <summary>
    /// A wrapper class around a <see cref="System.Windows.Media.Imaging.BitmapImage"/> that adds extra, useful, functionality.
    /// </summary>
    public class DapperBitmapImage : DapperBitmapSource
    {
        private BitmapImage _bitmap;

        /// <summary>
        /// Occurs when a significant change has occurred in the download progress of the System.Windows.Media.Imaging.BitmapImage content.
        /// </summary>
        public event EventHandler<DownloadProgressEventArgs> DownloadProgress;

        /// <summary>
        /// Occurs when there is an error associated with image retrieval or format.
        /// </summary>
        public event EventHandler<ExceptionRoutedEventArgs> ImageFailed;

        /// <summary>
        /// Occurs when the image source is downloaded and decoded with no failure. You can use this event to determine the size of an image before rendering it.
        /// </summary>
        public event EventHandler<RoutedEventArgs> ImageOpened;

        /// <summary>
        /// Identifies the CreateOptions DependencyProperty.
        /// </summary>
        public static readonly DependencyProperty CreateOptionsProperty =
            DependencyProperty.Register(
                "CreateOptions",
                typeof(BitmapCreateOptions),
                typeof(DapperBitmapImage),
                new PropertyMetadata(null));

        /// <summary>
        /// Identifies the UriSource DependencyProperty.
        /// </summary>
        public static readonly DependencyProperty UriSourceProperty =
            DependencyProperty.Register(
                "UriSource",
                typeof(Uri),
                typeof(DapperBitmapImage),
                new PropertyMetadata(null));

        /// <summary>
        /// Initializes a new instance of the System.Windows.Media.Imaging.BitmapImage class.
        /// </summary>
        public DapperBitmapImage()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the System.Windows.Media.Imaging.BitmapImage class, using the supplied URI.
        /// </summary>
        /// <param name="uriSource">The URI that references the source graphics file for the image.</param>
        public DapperBitmapImage(Uri uriSource)
            : this()
        {
            _bitmap = new BitmapImage(uriSource);
            Initialize(uriSource);
        }

        /// <summary>
        /// Initializes this DapperBitmapImage.
        /// </summary>
        /// <param name="uriSource">The uri being initialized to.</param>
        private void Initialize(Uri uriSource)
        {
            base.Name = Path.GetFileName(uriSource.LocalPath);
            _bitmap.DownloadProgress += (s, e) => { OnDownloadProgress(e); };
            _bitmap.ImageOpened += (s, e) => { OnImageOpened(e); };
            _bitmap.ImageFailed += (s, e) => { OnImageFailed(e); };
        }

        /// <summary>
        /// Get and set the CreateOptions property of this DapperBitmapImage.
        /// </summary>
        /// <returns>The System.Windows.Media.Imaging.BitmapCreateOptions used for this System.Windows.Media.Imaging.BitmapImage.
        /// The default is System.Windows.Media.Imaging.BitmapCreateOptions.DelayCreation.</returns>
        public BitmapCreateOptions CreateOptions
        {
            get
            {
                return (BitmapCreateOptions)_bitmap.GetValue(CreateOptionsProperty);
            }
            set
            {
                _bitmap.SetValue(CreateOptionsProperty, (Enum)value);
            }
        }

        /// <summary>
        /// Gets or sets the URI of the graphics source file that generated this System.Windows.Media.Imaging.BitmapImage.
        /// </summary>
        /// <returns>The URI of the graphics source file that generated this System.Windows.Media.Imaging.BitmapImage.</returns>
        public Uri UriSource
        {
            get { return (Uri)_bitmap.GetValue(UriSourceProperty); }
            set
            {
                Initialize(value);
                _bitmap.SetValue(UriSourceProperty, value);
            }
        }

        /// <summary>
        /// Raises the DownloadProgress event.
        /// </summary>
        /// <param name="args">The arguments for the raised event.</param>
        private void OnDownloadProgress(DownloadProgressEventArgs args)
        {
            if (null != DownloadProgress)
            {
                DownloadProgress(this, args);
            }
        }

        /// <summary>
        /// Raises the ImageFailed event.
        /// </summary>
        /// <param name="args">The arguments for the raised event.</param>
        private void OnImageFailed(ExceptionRoutedEventArgs args)
        {
            if (null != ImageFailed)
            {
                ImageFailed(this, args);
            }
        }

        /// <summary>
        /// Raises the ImageOpened event.
        /// </summary>
        /// <param name="args">The arguments for the raised event.</param>
        private void OnImageOpened(RoutedEventArgs args)
        {
            if (null != ImageOpened)
            {
                ImageOpened(this, args);
            }
        }
    }
}