using MinutoSeguro.Entity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace MinutoSeguro.Manager.Parser
{
    public class FeedParser
    {
        public Feed Parse(XElement xmlElement)
        {
            var feed = new Feed();
            feed.Title = GetValue(xmlElement, "title");
            feed.Description = GetValue(xmlElement, "description");
            feed.Content = GetValue(xmlElement, "encoded", "content");
            var categories = new List<string>();
            foreach (var category in xmlElement.Descendants("category"))
            {
                categories.Add(category.Value);
            }
            feed.Category = new ReadOnlyCollection<string>(categories);

            return feed;
        }

        private static string GetValue(XElement xmlElement, string name, string nameSpace = null, bool removeTags = true)
        {
            string rawValue;
            XName xName = String.IsNullOrEmpty(nameSpace) ?
                XName.Get(name) :
                XName.Get(name, xmlElement.GetNamespaceOfPrefix(nameSpace).NamespaceName);

            rawValue = xmlElement.Element(xName).Value;
            if (removeTags)
            {
                rawValue = Regex.Replace(rawValue, "<.*?>", String.Empty);
            }

            return rawValue;
        }

    }
}
