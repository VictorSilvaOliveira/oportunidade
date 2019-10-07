using System;
using System.Net;
using System.Xml.Linq;

namespace MinutoSeguro
{
    public class WebFeedReader : IFeedReader
    {
        private readonly Uri _uri;

        public WebFeedReader(string url)
        {
            _uri = new Uri(url);
        }

        public XDocument GetFeed()
        {
            var client = new WebClient();
            var rawValue = client.DownloadString(_uri);
            var xDocument = XDocument.Parse(rawValue);

            return xDocument;
        }
    }
}
