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

using System;
using System.Collections.Generic;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using DapperToolkitSamples.Data;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace DapperToolkitSamples.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        /// <summary>
        /// Collection of articles being populated on the UI.
        /// </summary>
        private readonly RssArticleCollection _articles;

        /// <summary>
        /// The max number of items to load to the list to prevent an out of memory exception.
        /// </summary>
        private const int MaxItems = 400;

        /// <summary>
        /// Number of articles to load when the user reaches the bottom.
        /// </summary>
        public static readonly int NumberToLoad = 20;

        /// <summary>
        /// Delay to wait before loading articles (for realism).
        /// </summary>
        private const double LoadingDelay = 0.0;

        /// <summary>
        /// Bool check to prevent repeated loading and show loading progress; binded to ProgressBar and Loading text.
        /// </summary>
        public bool BottomReached { get; set; }

        public MainPage()
        {
            InitializeComponent();

            NavigationCacheMode = NavigationCacheMode.Required;

            ScrollEventListBox.Loaded += ScrollEventListBox_Loaded;

            _articles = new RssArticleCollection();
            for (var i = 0; i < NumberToLoad; ++i)
            {
                _articles.Add(new RssArticle
                {
                    Author = "SomeAuthor",
                    PublishTime = DateTime.Today.ToString(),
                    Summary = "SomeSummary",
                    Title = "SomeTitle"
                });
            }
        }

        private void ScrollEventListBox_Loaded(object sender, RoutedEventArgs e)
        {
            ScrollEventListBox.ItemsSource = _articles;
        }

        private void OnBottomReached(object sender, EventArgs e)
        {
            if (BottomReached || ((_articles.Count) > MaxItems)) return;
            // Delay for realism
            var dt = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(LoadingDelay) };
            dt.Tick += (s, a) =>
            {
                _articles.Add(new RssArticle
                {
                    Author = "SomeAuthor",
                    PublishTime = DateTime.Today.ToString(),
                    Summary = "SomeSummary",
                    Title = "SomeTitle"
                });
                dt.Stop();
                BottomReached = false;
            };
            dt.Start();
            BottomReached = true;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
        }
    }
}
