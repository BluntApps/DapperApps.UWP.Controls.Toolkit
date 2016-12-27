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

//namespace DapperApps.Phone.Extensions
//{
//    using Microsoft.Xna.Framework.Media;

//    /// <summary>
//    /// Extended functionality for the PictureCollection class.
//    /// </summary>
//    public static class PictureCollectionEx
//    {
//        /// <summary>
//        /// Whether this colleciton contains the provided picture.
//        /// </summary>
//        /// <param name="collection">This collection.</param>
//        /// <param name="picture">The picture to search for.</param>
//        /// <returns>True if this collection contains the provided picture, false otherwise.</returns>
//        public static bool Contains(this PictureCollection collection, Picture picture)
//        {
//            // If saved picture exists, don't save it to the MediaLibrary.
//            foreach (Picture pic in collection)
//            {
//                if (pic == picture)
//                {
//                    return true;
//                }
//            }
//            return false;
//        }

//        /// <summary>
//        /// Attempts to get a Picture with the provided name from this collection.
//        /// </summary>
//        /// <param name="collection">The PictureCollection to attempt to get the picture from.</param>
//        /// <param name="name">The name of the picture to get.</param>
//        /// <returns>The picture with provided name, or null if not found.</returns>
//        public static Picture Get(this PictureCollection collection, string name)
//        {
//            // If saved picture exists, don't save it to the MediaLibrary.
//            foreach (Picture pic in collection)
//            {
//                if (pic.Name == name)
//                {
//                    return pic;
//                }
//            }
//            return null;
//        }
//    }
//}
