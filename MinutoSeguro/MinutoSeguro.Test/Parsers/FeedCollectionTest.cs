using MinutoSeguro.Manager.Parser;
using System;
using System.IO;
using System.Reflection;
using System.Xml.Linq;
using Xunit;

namespace MinutoSeguro.Test.Parser
{
    public class FeedCollectionTest
    {
        [Fact]
        public void Download_Success()
        {
            var rawFile = File.ReadAllText(Util.GetFullPath("FeedCollection.xml"));
            var xmlDocument = XDocument.Parse(rawFile);

            var xmlMockFeedReader = new Moq.Mock<IFeedReader>();

            xmlMockFeedReader
                .Setup(x => x.GetFeed())
                .Returns(xmlDocument);

            var feedCollection = new FeedCollection(xmlMockFeedReader.Object);

            Assert.True(feedCollection != null);
            Assert.Equal(10, feedCollection.Count);
            xmlMockFeedReader.Verify(x => x.GetFeed(), Moq.Times.Once);
        }



    }
}
