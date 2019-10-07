using System.Xml.Linq;

namespace MinutoSeguro
{
    public interface IFeedReader
    {
        XDocument GetFeed();
    }
}