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

using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace DapperApps.WinRT.Toolkit.ImageControl
{
    public abstract class ImageControl : Control.Control
    {
        /// <summary>
        /// The source image for this PTZImage.
        /// </summary>
        private Image _image;

        /// <summary>
        /// 
        /// </summary>
        protected Image Image
        {
            get
            {
                return _image;
            }
            set
            {
                if (null == _image)
                {
                    _image = value;
                    _image.ImageOpened += RaiseImageOpened;
                    _image.ImageFailed += RaiseImageFailed;
                }
                _image = value;
            }
        }

        /// <summary>
        /// Occurs when the image source is downloaded and decoded with no failure.
        /// You can use this event to determine the size of an image before rendering it.
        /// </summary>
        public event EventHandler<RoutedEventArgs> ImageOpened;

        /// <summary>
        /// Occurs when there is an error associated with image retrieval or format.
        /// </summary>
        public event EventHandler<ExceptionRoutedEventArgs> ImageFailed;

        /// <summary>
        /// Raises the ImageOpened event.
        /// </summary>
        protected void RaiseImageOpened(object sender, RoutedEventArgs args)
        {
            if (null != ImageOpened)
            {
                ImageOpened(sender, args);
            }
        }

        /// <summary>
        /// Raises the ImageFailed event.
        /// </summary>
        protected void RaiseImageFailed(object sender, ExceptionRoutedEventArgs args)
        {
            if (null != ImageFailed)
            {
                ImageFailed(sender, args);
            }
        }
    }
}
