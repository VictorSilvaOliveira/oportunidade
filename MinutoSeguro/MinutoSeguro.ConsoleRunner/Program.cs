using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using MinutoSeguro.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using MinutoSeguro.Manager.ActionManipulation;
using MinutoSeguro.Manager;

namespace MinutoSeguro.ConsoleRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json", true, true)
              .Build();
            var webFeedReader = new WebFeedReader("http://www.minutoseguros.com.br/blog/feed/");
            var wordCounter = new WordCounter();
            var removeUselessWords = new RemoveUselessWords(config);
            var textAnalizer = new TextAnalizer(config);
            var feed = new FeedCountImportantWords(textAnalizer, wordCounter);
            removeUselessWords.OnExecuteFinished += wordCounter.Execute;
            textAnalizer.OnExecuteFinished += removeUselessWords.Execute;
            var feedAnalizer = new FeedAnalizer<Dictionary<string, int>>(webFeedReader, feed);
            feedAnalizer.OnArticleFinish += PrintReport;
            feedAnalizer.AnalizeFeed();
        }

        private static void PrintReport(Feed feed, Dictionary<string, int> countedWords)
        {
            Console.WriteLine("Artigo : {0} ", feed.Title);
            foreach (var counted in countedWords.OrderByDescending(wc => wc.Value).Take(10))
            {
                Console.WriteLine("\t Word: {0} show {1} times", counted.Key, counted.Value);
            }
            Console.Write("Press any key to next article");
            Console.ReadLine();
        }
    }
}
