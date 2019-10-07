using MinutoSeguro.Entity;
using System.Collections.Generic;

namespace MinutoSeguro.Manager
{
    public interface IFeedAnalizer<T>
    {
        delegate void ArticleFinish(Feed feed, T results);

        event ArticleFinish OnArticleFinish;

        void AnalizeFeed();
    }
}