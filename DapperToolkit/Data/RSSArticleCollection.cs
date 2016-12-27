// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Public License (Ms-PL).
// Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.
// All other rights reserved.

using System;
using System.Collections.ObjectModel;

namespace DapperToolkit.Data
{
    public class RSSArticleCollection : ObservableCollection<RSSArticle>
    {
        public RSSArticleCollection()
        {
            LoadMoreArticles(20);
        }

        public void LoadMoreArticles(int numArticles)
        {
            for (int i = 0; i < numArticles; i++)
            {
                Add(new RSSArticle
                {
                    Title = LoremIpsum.GetParagraph(1),
                    Author = LoremIpsum.GetWords(2, LoremIpsum.Capitalization.FirstWord),
                    PublishTime = DateTime.Now,
                    Summary = LoremIpsum.GetParagraph(3)
                });
            }
        }
    }
}
