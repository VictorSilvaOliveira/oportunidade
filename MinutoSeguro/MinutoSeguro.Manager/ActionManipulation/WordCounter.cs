using MinutoSeguro.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MinutoSeguro.Manager.ActionManipulation
{
    public class WordCounter : IActionManipulation<IEnumerable<Word>, Dictionary<string, int>>
    {
        public override void Execute(IEnumerable<Word> words)
        {
            var countedWords = new Dictionary<string, int>();
            
            foreach (var word in words)
            {
                var rawWord = ((string)word).ToLowerInvariant();
                if (!countedWords.ContainsKey(rawWord))
                {
                    countedWords[rawWord] = 0;
                }
                countedWords[rawWord]++;
            }

            OnExecuteFinished(countedWords);
        }
    }
}
