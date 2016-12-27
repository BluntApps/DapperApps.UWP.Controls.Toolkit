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

//namespace DapperApps.Media.Imaging
//{
//    using System.Security;
//    using Windows.UI.Xaml;
//    using Windows.UI.Xaml.Media;
//    using Windows.UI.Xaml.Media.Imaging;

//    /// <summary>
//    /// A wrapper class around a <see cref="System.Windows.Media.Imaging.WriteableBitmap"/> that adds extra, useful, functionality.
//    /// </summary>
//    public class DapperWriteableBitmap : DapperBitmapSource
//    {
//        private WriteableBitmap _bitmap;

//        /// <summary>
//        /// Initializes a new instance of the System.Windows.Media.Imaging.WriteableBitmap class using the provided System.Windows.Media.Imaging.BitmapSource.
//        /// </summary>
//        /// <param name="source">The System.Windows.Media.Imaging.BitmapSource to use for initialization.</param>
//        [SecuritySafeCritical]
//        public DapperWriteableBitmap(BitmapSource source)
//        {
//            _bitmap = new WriteableBitmap(source);
//        }

//        /// <summary>
//        /// Initializes a new instance of the System.Windows.Media.Imaging.WriteableBitmap
//        /// class using the provided dimensions.
//        /// </summary>
//        /// <param name="pixelWidth">The width of the bitmap.</param>
//        /// <param name="pixelHeight">The height of the bitmap.</param>
//        /// <exception cref="System.ArgumentOutOfRangedException">pixelWidth or pixelHeight is zero or less.</exception>
//        [SecuritySafeCritical]
//        public DapperWriteableBitmap(int pixelWidth, int pixelHeight)
//        {
//            _bitmap = new WriteableBitmap(pixelWidth, pixelHeight);
//        }

//        /// <summary>
//        /// Initializes a new instance of the System.Windows.Media.Imaging.WriteableBitmap class using the provided element and transform.
//        /// </summary>
//        /// <param name="element">The desired element to be rendered within the bitmap.</param>
//        /// <param name="transform">The transform the user wants to apply to the element as the last step before
//        /// drawing into the bitmap. This is particularly interesting for you if you
//        /// want the bitmap to respect its transform. This value can be null.</param>
//        /// <exception cref="System.ArgumentNullException">element is null.</exception>
//        /// <exception cref="System.ArgumentException">The element size is invalid. This happens when the pixel width or pixel height
//        /// is not greater than zero.</exception>
//        [SecuritySafeCritical]
//        public DapperWriteableBitmap(UIElement element, Transform transform)
//        {
//            _bitmap = new WriteableBitmap(element, transform);
//        }

//        /// <summary>
//        /// Gets an array representing the 2-D texture of the bitmap.
//        /// </summary>
//        /// <returns>An array of integers representing the 2-D texture of the bitmap.</returns>
//        /// <exception cref="System.Security.SecurityException">The System.Windows.Media.Imaging.WriteableBitmap is created from protected
//        /// content. The System.Windows.Media.Imaging.WriteableBitmap.Pixels array is
//        /// inaccessible in this case.</exception>
//        public int[] Pixels { get { return _bitmap.Pixels; } }

//        /// <summary>
//        /// Requests a draw or redraw of the entire bitmap.
//        /// </summary>
//        public void Invalidate()
//        {
//            _bitmap.Invalidate();
//        }

//        /// <summary>
//        /// Renders an element within the bitmap.
//        /// </summary>
//        /// <param name="element">The element to be rendered within the bitmap.</param>
//        /// <param name="transform">The transform to apply to the element before drawing into the bitmap. If
//        /// an empty transform is supplied, the bits representing the element show up
//        /// at the same offset as if they were placed within their parent.</param>
//        /// <exception cref="System.ArgumentNullException">element is null.</exception>
//        public void Render(UIElement element, Transform transform)
//        {
//            _bitmap.Render(element, transform);
//        }
//    }
//}
