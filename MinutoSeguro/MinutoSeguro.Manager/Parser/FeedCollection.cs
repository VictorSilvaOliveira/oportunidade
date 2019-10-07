using MinutoSeguro.Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;

namespace MinutoSeguro.Manager.Parser
{
    public class FeedCollection : List<Feed>
    {
        public FeedCollection(IFeedReader xmlFeedReader)
        {
            XDocument xmlDocument = xmlFeedReader.GetFeed();

            var channelElement = xmlDocument.Root.Element("channel");
            var feedParser = new FeedParser();
            foreach (var feed in channelElement.Descendants("item"))
            {
                this.Add(feedParser.Parse(feed));
            }

        }

    }
}