// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Public License (Ms-PL).
// Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.
// All other rights reserved.

using System;
using System.Windows.Threading;
using Microsoft.Phone.Controls;
using DapperToolkit.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace DapperToolkit.Samples
{
    public partial class ScrollEventSample : PhoneApplicationPage
    {
        /// <summary>
        /// Collection of articles being populated on the UI.
        /// </summary>
        private RSSArticleCollection _articles;

        /// <summary>
        /// The max number of items to load to the list to prevent an out of memory exception.
        /// </summary>
        private static readonly int MaxItems = 400;

        /// <summary>
        /// Number of articles to load when the user reaches the bottom.
        /// </summary>
        private static readonly int NumberToLoad = 40;

        /// <summary>
        /// Delay to wait before loading articles (for realism).
        /// </summary>
        private static readonly double LoadingDelay = 1.5;

        /// <summary>
        /// Bool check to prevent repeated loading and show loading progress; binded to ProgressBar and Loading text.
        /// </summary>
        public bool BottomReached { get; set; }

        public ScrollEventSample()
        {
            InitializeComponent();

            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, EventArgs e)
        {
            _articles = LayoutRoot.Resources["Articles"] as RSSArticleCollection;
        }

        private void OnBottomReached(object sender, EventArgs e)
        {
            if (!BottomReached && ((_articles.Count) <= MaxItems))
            {
                // Delay for realism
                DispatcherTimer dt = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(LoadingDelay) };
                dt.Tick += (s, a) =>
                {
                    _articles.LoadMoreArticles(NumberToLoad);
                    dt.Stop();
                    BottomReached = false;
                };
                dt.Start();
                BottomReached = true;
            }
        }
    }
}