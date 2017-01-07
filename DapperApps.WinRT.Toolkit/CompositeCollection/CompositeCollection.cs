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
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DapperApps.WinRT.Toolkit.CompositeCollection
{
    /// <summary>
    /// TODO
    /// </summary>
    public class CompositeCollection : ObservableCollection<object>
    {
        Collection<IEnumerable> _collections;

        public CompositeCollection()
            : base()
        {
            _collections = new Collection<IEnumerable>();
        }

        public CompositeCollection(IEnumerable<object> collection) : this()
        {
            if (null == collection)
            {
                throw new ArgumentNullException("collection");
            }
            foreach (object obj in collection)
            {
                base.Add(obj);
            }
        }

        public CompositeCollection(List<object> list) : this()
        {
            if(null == list)
            {
                throw new ArgumentNullException("list");
            }
            foreach(object obj in list)
            {
                base.Add(obj);
            }
        }

        protected override void ClearItems()
        {
            base.Clear();
            _collections.Clear();
        }

        protected override void InsertItem(int index, object item)
        {
            CompositeContainer container = item as CompositeContainer;
            if (null != container && null != container.Collection)
            {
                InsertContainer(index, container);
            }
            else
            {
                base.InsertItem(index, item);
            }
        }

        private void InsertContainer(int index, CompositeContainer container)
        {
            IEnumerable collection = _collections[index] = container.Collection;
            foreach (object obj in collection)
            {
                base.InsertItem(index++, obj);
            }
        }

        protected override void RemoveItem(int index)
        {
            IEnumerable collection = _collections[index];
            if (null != collection)
            {
                RemoveContainer(index, collection);
            }
            else
            {
                base.RemoveItem(index);
            }
        }

        private void RemoveContainer(int index, IEnumerable collection)
        {
            foreach (object obj in collection)
            {
                base.RemoveItem(index++);
            }
            _collections.RemoveAt(index);
        }

        protected override void SetItem(int index, object item)
        {
            RemoveItem(index);
            InsertItem(index, item);
        }
    }
}