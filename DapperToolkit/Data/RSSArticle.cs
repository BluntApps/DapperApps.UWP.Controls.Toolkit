// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Public License (Ms-PL).
// Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.
// All other rights reserved.

using System;

namespace DapperToolkit.Data
{
    public class RSSArticle
    {
        public string Title { get; set; }
        public DateTime PublishTime { get; set; }
        public string Author { get; set; }
        public string Summary { get; set; }
    }
}
