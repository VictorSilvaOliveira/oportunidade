using Microsoft.Extensions.Configuration;
using MinutoSeguro.Entity;
using MinutoSeguro.Manager.ActionManipulation;
using MinutoSeguro.Manager.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MinutoSeguro.Manager
{
    public class FeedAnalizer<T> : IFeedAnalizer<T>
    {
        private readonly IFeedReader _feedReader;
        private readonly IActionManipulation<Feed, T> _action;

        public FeedAnalizer(IFeedReader feedReader, IActionManipulation<Feed, T> action)
        {
            _feedReader = feedReader;
            _action = action;
        }

        public event IFeedAnalizer<T>.ArticleFinish OnArticleFinish;

        public void AnalizeFeed()
        {
            var feedCollection = new FeedCollection(_feedReader);
            foreach (var feed in feedCollection)
            {
                _action.OnExecuteFinished = (T results) => OnArticleFinish(feed, results);
                _action.Execute(feed);
            }
        }
    }
}
