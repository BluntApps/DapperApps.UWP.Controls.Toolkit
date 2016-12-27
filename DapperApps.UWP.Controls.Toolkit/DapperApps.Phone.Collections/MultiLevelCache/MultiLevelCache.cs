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

//namespace DapperApps.Phone.Collections
//{
//    using System;
//    using System.Collections;
//    using System.Collections.Generic;
//    using System.IO;
//    using Windows.Storage;

//    /// <summary>
//    /// Represents a table of objects that are cached in memory and
//    /// other levels of storage.
//    /// E.g.
//    ///     Caching to main memory and disk.
//    ///     Caching to Main memory, disk, and a remote direcotry.
//    ///     ...
//    /// </summary>
//    /// <typeparam name="T"></typeparam>
//    public class MultiLevelCache<T> : IDictionary<string, T>, ICollection<KeyValuePair<string, T>>, IReadOnlyDictionary<string, T>, IReadOnlyCollection<KeyValuePair<string, T>>, IEnumerable<KeyValuePair<string, T>>, IEnumerable
//    {
//        /// <summary>
//        /// The default option for creating the cache.
//        /// </summary>
//        public const CacheInitOption DEFAULT_CACHE_CREATION_OPTION = CacheInitOption.OpenIfExists;

//        /// <summary>
//        /// The default type of caching performed by MultiLevelCaches.
//        /// </summary>
//        public const CacheType DEFAULT_CACHING_TYPE = CacheType.ImmediateCaching;

//        /// <summary>
//        /// The default on-disk directory where objects are cached.
//        /// </summary>
//        public const string DEFAULT_DISK_CACHE_DIR = @"DataFolder\MultiLevelCacheStorage";

//        private StorageFolder _cacheRoot;
//        private Dictionary<string, CacheItem<T>> _items;
//        private Dictionary<string, T> _dictionary;

//        /// <summary>
//        /// Initializes a new instance of the <see cref="DapperApps.Phone.Collections.MultiLevelCache<T>"/>
//        /// class that is empty, has the default initial capacity, and uses the default
//        /// equality comparer for the key type.
//        /// </summary>
//        /// <param name="cacheDir">The on-disk directory where objects are cached.</param>
//        /// <param name="cachingType">How/when items are cached/uncached.</param>
//        public MultiLevelCache(
//            string cacheDir = DEFAULT_DISK_CACHE_DIR,
//            CacheInitOption cacheCreationOption = DEFAULT_CACHE_CREATION_OPTION,
//            CacheType cachingType = DEFAULT_CACHING_TYPE)
//            : this(0, null, cacheDir, cacheCreationOption, cachingType)
//        {
//        }

//        /// <summary>
//        /// Initializes a new instance of the <see cref="DapperApps.Phone.Collections.MultiLevelCache<T>"/>
//        /// class that contains elements copied from the specified <see cref="System.Collections.Generic.IDictionary<string, T>"/>
//        /// and uses the default equality comparer for the key type.
//        /// </summary>
//        /// <param name="dictionary">The <see cref="System.Collections.Generic.IDictionary<string, T>"/> whose elements are
//        /// copied to the new <see cref="DapperApps.Phone.Collections.MultiLevelCache<T>"/>.</param>
//        /// <param name="cacheDir">The on-disk directory where objects are cached.</param>
//        /// <param name="cachingType">How/when items are cached/uncached.</param>
//        /// <exception cref="System.ArgumentNullException">dictionary is null.</exception>
//        /// <exception cref="System.ArgumentException">dictionary contains one or more duplicate keys.</exception>
//        public MultiLevelCache(IDictionary<string, T> dictionary,
//            string cacheDir = DEFAULT_DISK_CACHE_DIR,
//            CacheInitOption cacheCreationOption = DEFAULT_CACHE_CREATION_OPTION,
//            CacheType cachingType = DEFAULT_CACHING_TYPE)
//            : this(dictionary, null, cacheDir, cacheCreationOption, cachingType)
//        {
//        }

//        /// <summary>
//        /// Initializes a new instance of the <see cref="DapperApps.Phone.Collections.MultiLevelCache<T>"/>
//        /// class that is empty, has the default initial capacity, and uses the specified
//        /// <see cref="System.Collections.Generic.IEqualityComparer<string>"/>.
//        /// </summary>
//        /// <param name="comparer">The <see cref="System.Collections.Generic.IEqualityComparer<string>"/> implementation to use
//        /// when comparing keys, or null to use the default <see cref="System.Collections.Generic.EqualityComparer<string>"/>
//        /// for the type of the key.</param>
//        /// <param name="cacheDir">The on-disk directory where objects are cached.</param>
//        /// <param name="cachingType">How/when items are cached/uncached.</param>
//        public MultiLevelCache(
//            IEqualityComparer<string> comparer,
//            string cacheDir = DEFAULT_DISK_CACHE_DIR,
//            CacheInitOption cacheCreationOption = DEFAULT_CACHE_CREATION_OPTION,
//            CacheType cachingType = DEFAULT_CACHING_TYPE)
//            : this(0, comparer, cacheDir, cacheCreationOption, cachingType)
//        {
//        }

//        /// <summary>
//        /// Initializes a new instance of the <see cref="DapperApps.Phone.Collections.MultiLevelCache<T>"/>
//        /// class that is empty, has the specified initial capacity, and uses the default
//        /// equality comparer for the key type.
//        /// </summary>
//        /// <param name="capacity">The initial number of elements that the <see cref="DapperApps.Phone.Collections.MultiLevelCache<T>"/>
//        /// can contain.</param>
//        /// <param name="cacheDir">The on-disk directory where objects are cached.</param>
//        /// <param name="cachingType">How/when items are cached/uncached.</param>
//        public MultiLevelCache(
//            int capacity,
//            string cacheDir = DEFAULT_DISK_CACHE_DIR,
//            CacheInitOption cacheCreationOption = DEFAULT_CACHE_CREATION_OPTION,
//            CacheType cachingType = DEFAULT_CACHING_TYPE)
//            : this(capacity, null, cacheDir, cacheCreationOption, cachingType)
//        {
//        }

//        /// <summary>
//        /// Initializes a new instance of the <see cref="DapperApps.Phone.Collections.MultiLevelCache<T>"/>
//        /// class that contains elements copied from the specified System.Collections.Generic.IDictionary<TKey,TValue>
//        /// and uses the specified System.Collections.Generic.IEqualityComparer<T>.
//        /// </summary>
//        /// <param name="dictionary">The <see cref="System.Collections.Generic.IDictionary<string, T>"/> whose elements are
//        /// copied to the new <see cref="DapperApps.Phone.Collections.MultiLevelCache<T>"/>.</param>
//        /// <param name="comparer">The <see cref="System.Collections.Generic.IEqualityComparer<string>"/> implementation to use
//        /// when comparing keys, or null to use the default <see cref="System.Collections.Generic.EqualityComparer<string>"/>
//        /// for the type of the key.</param>
//        /// <param name="cacheDir">The on-disk directory where objects are cached.</param>
//        /// <param name="cachingType">How/when items are cached/uncached.</param>
//        /// <exception cref="System.ArgumentNullException">dictionary is null.</exception>
//        /// <exception cref="System.ArgumentException">dictionary contains one or more duplicate keys.</exception>
//        public MultiLevelCache(
//            IDictionary<string, T> dictionary,
//            IEqualityComparer<string> comparer,
//            string cacheDir = DEFAULT_DISK_CACHE_DIR,
//            CacheInitOption cacheCreationOption = DEFAULT_CACHE_CREATION_OPTION,
//            CacheType cachingType = DEFAULT_CACHING_TYPE)
//            : this((dictionary != null) ? dictionary.Count : 0, comparer, cacheDir, cacheCreationOption, cachingType)
//        {
//        }

//        /// <summary>
//        /// Initializes a new instance of the <see cref="DapperApps.Phone.Collections.MultiLevelCache<T>"/>
//        /// class that is empty, has the specified initial capacity, and uses the specified
//        /// <see cref="System.Collections.Generic.IEqualityComparer<string>"/>.
//        /// </summary>
//        /// <param name="capacity">The initial number of elements that the <see cref="DapperApps.Phone.Collections.MultiLevelCache<T>"/>
//        /// can contain.</param>
//        /// <param name="comparer">The <see cref="System.Collections.Generic.IEqualityComparer<string>"/> implementation to use
//        /// when comparing keys, or null to use the default <see cref="System.Collections.Generic.EqualityComparer<string>"/>
//        /// for the type of the key.</param>
//        /// <param name="cacheDir">The on-disk directory where objects are cached.</param>
//        /// <param name="cachingType">How/when items are cached/uncached.</param>
//        /// <exception cref="System.ArgumentOutOfRangeException">capacity is less than 0.</exception>
//        public MultiLevelCache(
//            int capacity,
//            IEqualityComparer<string> comparer,
//            string cacheDir = DEFAULT_DISK_CACHE_DIR,
//            CacheInitOption cacheCreationOption = DEFAULT_CACHE_CREATION_OPTION,
//            CacheType cachingType = DEFAULT_CACHING_TYPE)
//        {
//            try
//            {
//                _dictionary = new Dictionary<string, T>(capacity, comparer);
//                _items = new Dictionary<string, CacheItem<T>>(capacity, comparer);

//                CachingType = cachingType;

//                //TODO exceptions
//                Storage_Init(cacheDir, cacheCreationOption);
//            }
//            catch
//            {
//                throw;
//            }
//        }

//        /// <summary>
//        /// Sets up the storage cache directories. For reading and writing.
//        /// </summary>
//        /// <param name="path">A path where to initializer the directories.</param>
//        /// <param name="createOps">The create options of </param>
//        private async void Storage_Init(string path, CacheInitOption createOps)
//        {
//            try
//            {
//                var rootDir = ApplicationData.Current.LocalFolder;
//                var fldr = StorageFolder.GetFolderFromPathAsync(path);
//                _cacheRoot = await rootDir.CreateFolderAsync(
//                    path,
//                    createOps == CacheInitOption.ReplaceExisting
//                    ? CreationCollisionOption.ReplaceExisting
//                    : CreationCollisionOption.OpenIfExists);

//                /// Reads the values in the storage cache.
//                /// If deferred loading is activated, only the names
//                /// of the items in storage are read, otherwise,
//                /// the items are deserialized.
//                bool immediate = (CachingType == CacheType.ImmediateCaching);
//                var files = await _cacheRoot.GetFilesAsync();
//                foreach (var file in files)
//                {
//                    var keyName = file.Name;
//                    var item = new CacheItem<T> { KeyName = keyName, InStorage = true, InMemory = immediate };
//                    if (immediate)
//                    {
//                        // Read entire file
//                        Storage_Read(item);
//                    }
//                    else
//                    {
//                        // Just put names and default values so we know they exist.
//                        _dictionary.Add(keyName, item.Value);
//                        _items.Add(keyName, item);
//                    }
//                }
//            }
//            catch
//            {
//                throw;
//            }
//        }

//        /// <summary>
//        /// Reads and populates the value of the provided item from storage,
//        /// adding such value to the dictionary.
//        /// </summary>
//        /// <param name="item">The <see cref="DapperApps.Phone.Collections.CacheItem"/> to read.</param>
//        private async void Storage_Read(CacheItem<T> item)
//        {
//            try
//            {
//                var keyName = item.KeyName;
//                var file = await _cacheRoot.GetFileAsync(keyName);
//                using (var stream = (await file.OpenReadAsync()).AsStreamForRead())
//                {
//                    item.Value = (T)(new BinarySerializer(typeof(T)).Deserialize(stream));
//                }
//                _dictionary.Add(keyName, item.Value);
//                _items.Add(keyName, item);
//                item.InStorage = item.InMemory = true;
//            }
//            catch
//            {
//                throw;
//            }
//        }

//        /// <summary>
//        /// Writes the given item to storage.
//        /// </summary>
//        /// <param name="item">The <see cref="DapperApps.Phone.Collections.CacheItem"/> to write to storage.</param>
//        private async void Storage_Write(CacheItem<T> item)
//        {
//            try
//            {
//                var file = await _cacheRoot.CreateFileAsync(item.KeyName, CreationCollisionOption.FailIfExists);
//                using (var stream = (await file.OpenAsync(FileAccessMode.ReadWrite)).AsStream())
//                {
//                    new BinarySerializer(typeof(T)).Serialize(stream, item.Value);
//                }
//                item.InStorage = item.InMemory = true;
//            }
//            catch
//            {
//                throw;
//            }
//        }

//        /// <summary>
//        /// Deletes the provided item from storage.
//        /// </summary>
//        /// <param name="item">The <see cref="DapperApps.Phone.Collections.CacheItem"/> to delete from storage.</param>
//        private async void Storage_Delete(CacheItem<T> item)
//        {
//            try
//            {
//                var keyName = item.KeyName;
//                var file = await _cacheRoot.GetFileAsync(keyName);
//                await file.DeleteAsync(StorageDeleteOption.PermanentDelete);
//            }
//            catch
//            {
//                throw;
//            }
//        }

//        /// <summary>
//        /// How/when items are cached/uncached.
//        /// </summary>
//        public CacheType CachingType { get; set; }

//        /// <summary>
//        /// Gets the number of key/value pairs contained in the <see cref="DapperApps.Phone.Collections.MultiLevelCache<T>"/>.
//        /// </summary>
//        /// <returns>
//        /// The number of key/value pairs contained in the <see cref="DapperApps.Phone.Collections.MultiLevelCache<T>"/>.
//        /// </returns>
//        public int Count
//        {
//            get { return _dictionary.Count; }
//        }

//        /// <summary>
//        /// Gets an <see cref="System.Collections.Generic.ICollection<string>"/> containing the keys of
//        /// the <see cref="DapperApps.Phone.Collections.MultiLevelCache<T>"/>.
//        /// </summary>
//        /// <returns>
//        /// An System.Collections.Generic.ICollection<T> containing the keys of the object
//        /// that implements System.Collections.Generic.IDictionary<TKey,TValue>.
//        /// </returns>
//        public ICollection<string> Keys
//        {
//            get { return _dictionary.Keys; }
//        }

//        /// <summary>
//        /// Gets an <see cref="System.Collections.Generic.ICollection<T>"/> containing the values in
//        /// the <see cref="DapperApps.Phone.Collections.MultiLevelCache<T>"/>.
//        /// </summary>
//        /// <returns>
//        /// An <see cref="System.Collections.Generic.ICollection<T>"/> containing the values in the
//        /// object that implements <see cref="DapperApps.Phone.Collections.MultiLevelCache<T>"/>.
//        /// </returns>
//        public ICollection<T> Values
//        {
//            get { return _dictionary.Values; }
//        }

//        /// <summary>
//        /// Gets or sets the value associated with the specified key.
//        /// </summary>
//        /// <param name="key">The key of the value to get or set.</param>
//        /// <returns>
//        /// The value associated with the specified key. If the specified key is not
//        /// found, a get operation throws a <see cref="System.Collections.Generic.KeyNotFoundException"/>,
//        /// and a set operation creates a new element with the specified key.
//        /// </returns>
//        /// <exception cref="System.ArgumentNullException">key is null.</exception>
//        /// <exception cref="System.Collections.Generic.KeyNotFoundException">The property is retrieved and key does not exist in the collection.</exception>
//        public T this[string key]
//        {
//            get
//            {
//                try
//                {
//                    var item = _items[key];
//                    if (!item.InMemory)
//                    {
//                        Storage_Read(item);
//                    }
//                    return _dictionary[key];
//                }
//                catch
//                {
//                    throw;
//                }
//            }
//            set
//            {
//                try
//                {
//                    _dictionary[key] = value;
//                    var immediate = (CachingType == CacheType.ImmediateCaching);
//                    var item = new CacheItem<T>
//                    {
//                        KeyName = key,
//                        Value = value,
//                        InMemory = true,
//                        InStorage = immediate
//                    };
//                    if (immediate)
//                    {
//                        Storage_Write(item);
//                    }
//                    _items.Add(key, item);
//                }
//                catch
//                {
//                    throw;
//                }
//            }
//        }

//        /// <summary>
//        /// Adds the specified key and value to the Cache.
//        /// </summary>
//        /// <param name="key">The key of the element to add.</param>
//        /// <param name="value">The value of the element to add. The value can be null for reference types.</param>
//        /// <exception cref="System.ArgumentNullException">key is null.</exception>
//        /// <exception cref="System.ArgumentException">An element with the same key already exists in the <see cref="DapperApps.Phone.Collections.MultiLevelCache<T>"/>.</exception>
//        public void Add(string key, T value)
//        {
//            try
//            {
//                var keyFile = new FileInfo(key);
//                var keyName = keyFile.Name;

//                bool immediate = (CachingType == CacheType.ImmediateCaching);
//                var item = new CacheItem<T>
//                {
//                    KeyName = keyName,
//                    Value = value,
//                    InMemory = true,
//                    InStorage = immediate,
//                };

//                _dictionary.Add(keyName, value);

//                if (immediate)
//                {
//                    Storage_Write(item);
//                }

//                _items.Add(keyName, item);
//            }
//            catch (ArgumentNullException)
//            {
//                throw new ArgumentNullException("key");
//            }
//            catch (ArgumentException ae)
//            {
//                throw new ArgumentException("The provided key is empty, contains only white spaces, or contains invalid characters.", ae);
//            }
//            catch (NotSupportedException nse)
//            {
//                throw new NotSupportedException("Key contains a colon (:) in the middle of the string.", nse);
//            }
//            catch
//            {
//                throw;
//            }
//        }

//        /// <summary>
//        /// Removes all keys and values from the <see cref="DapperApps.Phone.Collections.MultiLevelCache<T>"/>.
//        /// </summary>
//        public void Clear()
//        {
//            _dictionary.Clear();
//            foreach (var item in _items.Values)
//            {
//                Storage_Delete(item);
//            }
//            _items.Clear();
//        }

//        /// <summary>
//        /// Determines whether the <see cref="DapperApps.Phone.Collections.MultiLevelCache<T>"/>
//        /// contains the specified key.
//        /// </summary>
//        /// <param name="key">The key to locate in the System.Collections.Generic.Dictionary<TKey,TValue>.</param>
//        /// <returns>true if the System.Collections.Generic.Dictionary<TKey,TValue> contains an
//        /// element with the specified key; otherwise, false.</returns>
//        /// <exception cref="System.ArgumentNullException">key is null.</exception>
//        public bool ContainsKey(string key)
//        {
//            try
//            {
//                return _items.ContainsKey(key);
//            }
//            catch
//            {
//                throw;
//            }
//        }

//        /// <summary>
//        /// Determines whether the <see cref="DapperApps.Phone.Collections.MultiLevelCache<T>"/>
//        /// contains a specific value.
//        /// </summary>
//        /// <param name="value">The value to locate in the <see cref="DapperApps.Phone.Collections.MultiLevelCache<T>"/>.
//        /// The value can be null for reference types.</param>
//        /// <returns>true if the <see cref="DapperApps.Phone.Collections.MultiLevelCache<T>"/> contains an
//        /// element with the specified value; otherwise, false.</returns>
//        public bool ContainsValue(T value)
//        {
//            return _dictionary.ContainsValue(value);
//        }

//        /// <summary>
//        /// Returns an enumerator that iterates through the <see cref="DapperApps.Phone.Collections.MultiLevelCache<T>"/>.
//        /// </summary>
//        /// <returns>A <see cref="System.Collections.Generic.Dictionary<string,T>.Enumerator"/>.Enumerator structure
//        /// for the <see cref="DapperApps.Phone.Collections.MultiLevelCache<T>"/>.</returns>
//        IEnumerator<KeyValuePair<string, T>> GetEnumerator()
//        {
//            return _dictionary.GetEnumerator();
//        }

//        /// <summary>
//        /// Removes the value with the specified key from the <see cref="DapperApps.Phone.Collections.MultiLevelCache<T>"/>.
//        /// </summary>
//        /// <param name="key">The key of the element to remove.</param>
//        /// <returns>true if the element is successfully found and removed; otherwise, false.
//        /// This method returns false if key is not found in the <see cref="DapperApps.Phone.Collections.MultiLevelCache<T>"/>.</returns>
//        /// <exception cref="System.ArgumentNullException">key is null.</exception>
//        public bool Remove(string key)
//        {
//            try
//            {
//                CacheItem<T> item;
//                if (_items.TryGetValue(key, out item))
//                {
//                    Storage_Delete(item);
//                    _items.Remove(key);
//                    _dictionary.Remove(key);
//                    return true;
//                }
//                return false;
//            }
//            catch
//            {
//                throw;
//            }
//        }

//        /// <summary>
//        /// Gets the value associated with the specified key.
//        /// </summary>
//        /// <param name="key">The key of the value to get.</param>
//        /// <param name="value">When this method returns, contains the value associated with the specified
//        /// key, if the key is found; otherwise, the default value for the type of the
//        /// value parameter. This parameter is passed uninitialized.</param>
//        /// <returns>true if the <see cref="DapperApps.Phone.Collections.MultiLevelCache<T>"/> contains an
//        /// element with the specified key; otherwise, false.</returns>
//        /// <exception cref="System.ArgumentNullException">key is null.</exception>
//        public bool TryGetValue(string key, out T value)
//        {
//            try
//            {
//                return _dictionary.TryGetValue(key, out value);
//            }
//            catch
//            {
//                throw;
//            }
//        }

//        void ICollection<KeyValuePair<string, T>>.Add(KeyValuePair<string, T> item)
//        {
//            ((ICollection<KeyValuePair<string, T>>)_dictionary).Add(item);
//            var key = item.Key;
//            var immediate = (CachingType == CacheType.ImmediateCaching);
//            var value = new CacheItem<T> { KeyName = key, Value = item.Value, InMemory = true, InStorage = immediate };
//            ((ICollection<KeyValuePair<string, CacheItem<T>>>)_items).Add(new KeyValuePair<string, CacheItem<T>>(key, value));
//            if (immediate)
//            {
//                Storage_Write(value);
//            }
//        }

//        bool ICollection<KeyValuePair<string, T>>.Contains(KeyValuePair<string, T> item)
//        {
//            return ((ICollection<KeyValuePair<string, T>>)_dictionary).Contains(item);
//        }

//        void ICollection<KeyValuePair<string, T>>.CopyTo(KeyValuePair<string, T>[] array, int arrayIndex)
//        {
//            ((ICollection<KeyValuePair<string, T>>)_dictionary).CopyTo(array, arrayIndex);
//        }

//        bool ICollection<KeyValuePair<string, T>>.IsReadOnly
//        {
//            get { return ((ICollection<KeyValuePair<string, T>>)_dictionary).IsReadOnly; }
//        }

//        bool ICollection<KeyValuePair<string, T>>.Remove(KeyValuePair<string, T> item)
//        {
//            var remove = ((ICollection<KeyValuePair<string, T>>)_dictionary).Remove(item);
//            if (remove)
//            {
//                ((ICollection<KeyValuePair<string, CacheItem<T>>>)_items).Remove(
//                    new KeyValuePair<string, CacheItem<T>>(
//                        item.Key,
//                        new CacheItem<T> { KeyName = item.Key, Value = item.Value, InMemory = true }
//                        ));
//            }
//            return ((ICollection<KeyValuePair<string, T>>)_dictionary).Remove(item);
//        }

//        IEnumerator<KeyValuePair<string, T>> IEnumerable<KeyValuePair<string, T>>.GetEnumerator()
//        {
//            return ((IEnumerable<KeyValuePair<string, T>>)_dictionary).GetEnumerator();
//        }

//        IEnumerator IEnumerable.GetEnumerator()
//        {
//            return ((IEnumerable)_dictionary).GetEnumerator();
//        }

//        IEnumerable<string> IReadOnlyDictionary<string, T>.Keys
//        {
//            get { return ((IReadOnlyDictionary<string, T>)_dictionary).Keys; }
//        }

//        IEnumerable<T> IReadOnlyDictionary<string, T>.Values
//        {
//            get { return ((IReadOnlyDictionary<string, T>)_dictionary).Values; }
//        }
//    }

//    /// <summary>
//    /// Possible options for when creating the storage cache.
//    /// </summary>
//    public enum CacheInitOption
//    {
//        /// <summary>
//        /// Create the new file or folder with the desired name, and replaces any file or folder that already exists with that name. 
//        /// </summary>
//        ReplaceExisting = 1,

//        /// <summary>
//        /// Create the new file or folder with the desired name, or returns an existing item if a file or folder already exists with that name.
//        /// </summary>
//        OpenIfExists = 3
//    };

//    /// <summary>
//    /// Defines an enumerable of options for how/when items are cached/uncached.
//    /// </summary>
//    public enum CacheType
//    {
//        /// <summary>
//        /// Caches/Uncaches items as they are added and removed.
//        /// </summary>
//        ImmediateCaching,

//        /// <summary>
//        /// Caches/Uncaches items as needed.
//        /// </summary>
//        DeferredCaching
//    };
//}