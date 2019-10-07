using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace MinutoSeguro.Entity
{
    public class Sentence : List<Word>
    {

        private readonly string _rawSentece;

        public Sentence(string rawSentece)
        {
            _rawSentece = rawSentece.Trim();
            _rawSentece.Split().ToList().ForEach(w => this.Add(w));
        }

        public static List<Sentence> Parse(string text, IEnumerable<string> sentenceBreakers, IEnumerable<string> pointers)
        {
            List<Sentence> sentences = new List<Sentence>();
            string[] phrases = new string[] { text };


            foreach (var pointer in pointers)
            {
                var auxPhrases = new List<string>();
                foreach (var phrase in phrases)
                {
                    auxPhrases.AddRange(phrase.Split(pointer).ToList());
                }
                phrases = auxPhrases.ToArray();
            }

            foreach (var sentenceBreaker in sentenceBreakers)
            {
                var auxPhrases = new List<string>();
                foreach (var phrase in phrases)
                {
                    auxPhrases.AddRange(Regex.Split(phrase, $@"\b{sentenceBreaker}\s*\b", RegexOptions.IgnoreCase).ToList());
                }
                phrases = auxPhrases.ToArray();
            }

            foreach (var phrase in phrases)
            {
                if (!String.IsNullOrEmpty(phrase))
                {
                    sentences.Add(phrase);
                }
            }

            return sentences;
        }

        public static implicit operator Sentence(string phrase)
        {
            return new Sentence(phrase);
        }


        public static implicit operator string(Sentence me)
        {
            return me._rawSentece;
        }

    }
}