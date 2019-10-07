using MinutoSeguro.Entity;
using MinutoSeguro.Manager.ActionManipulation;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MinutoSeguro.Test.ActionManipulation
{
    public class WordCounterTest
    {
        [Fact]
        public void WordCounter_Success()
        {
            var wordCounter = new WordCounter();
            Dictionary<string, int> countedWord = null;
            wordCounter.OnExecuteFinished += (counted) => countedWord = counted;
            wordCounter.Execute(new List<Word>()
            {
                new Word("boa"),
                new Word("ali"),
                new Word("BOa"),
                new Word("Ali"),
                new Word("ALI")
            });

            Assert.Equal(2, countedWord.Count);
            Assert.Equal(2, countedWord["boa"]);
            Assert.Equal(3, countedWord["ali"]);
        }
    }
}
