using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using MinutoSeguro.Entity;
using Microsoft.Extensions.Configuration;

namespace MinutoSeguro.Manager.ActionManipulation
{
    public class TextAnalizer : IActionManipulation<string, IEnumerable<Word>>
    {
        private readonly IEnumerable<string> _prepositions;
        private readonly IEnumerable<string> _articles;
        private readonly IEnumerable<string> _sentencesBreakers;
        private readonly IEnumerable<string> _pontuations;
        private readonly IEnumerable<string> _verbs;
        private IEnumerable<Sentence> _sentences;

        public TextAnalizer(IConfiguration config)
        {
            _prepositions = config.GetSection("prepositions").Value.Split(",");
            _articles = config.GetSection("articles").Value.Split(",");
            _sentencesBreakers = config.GetSection("setences_breakers").Value.Split(",");
            _pontuations = config.GetSection("pontuation").Value.Split("|");
            _verbs = config.GetSection("verbs").Value.Split(",");
        }

        public override void Execute(string source)
        {
            _sentences = Sentence.Parse(source, _sentencesBreakers, _pontuations);
            foreach (var sentence in _sentences)
            {
                foreach (Word word in sentence)
                {
                    var matchableWord = ((string)word).ToLowerInvariant();
                    word.Type = WordType.Default;
                    if (Regex.IsMatch(matchableWord, $"^({String.Join("|", _verbs)})$"))
                    {
                        word.Type = WordType.Verb;
                    }
                    else if (Regex.IsMatch(matchableWord, $"^({String.Join("|", _articles)})$"))
                    {
                        word.Type = WordType.Article;
                    }
                    else if (Regex.IsMatch(matchableWord, $"^({String.Join("|", _prepositions)})$"))
                    {
                        word.Type = WordType.Preposition;
                    }
                }
            }

            OnExecuteFinished(_sentences.SelectMany(s => s));
        }
    }
}
