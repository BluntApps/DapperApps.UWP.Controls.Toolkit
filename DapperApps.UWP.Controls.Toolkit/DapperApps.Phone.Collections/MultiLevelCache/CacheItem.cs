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

namespace DapperApps.Phone.Collections
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// A container for cache items that keeps track of metadata on such item.
    /// </summary>
    public class CacheItem<T>
    {
        /// <summary>
        /// The name of the key for this cache item.
        /// </summary>
        public string KeyName { get; set; }

        /// <summary>
        /// Whether the value of this cache item is available at the memory level.
        /// </summary>
        public bool InMemory { get; set; }

        /// <summary>
        /// Whether the value of this cache item is available at the storage level.
        /// </summary>
        public bool InStorage { get; set; }

        /// <summary>
        /// The value of this cache item.
        /// </summary>
        public T Value { get; set; }
    }
}
