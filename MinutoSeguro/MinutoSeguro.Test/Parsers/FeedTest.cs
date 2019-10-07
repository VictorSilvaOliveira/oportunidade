using MinutoSeguro.Manager.Parser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Linq;
using Xunit;

namespace MinutoSeguro.Test.Parser
{
    public class FeedTest
    {
        [Fact]
        public void Download_Success()
        {
            var rawFile = File.ReadAllText(Util.GetFullPath("Feed.xml"));
            var xmlDocument = XDocument.Parse(rawFile).Root;

            var feedParser = new FeedParser();

            var feed = feedParser.Parse(xmlDocument);

            Assert.NotNull(feed);
            Assert.NotEqual(string.Empty, feed.Title);
            Assert.NotEqual(string.Empty, feed.Description);
            Assert.NotNull(feed.Content);
            Assert.True(feed.Category.Count > 0, "Has not loaded categories");
        }

    }
}