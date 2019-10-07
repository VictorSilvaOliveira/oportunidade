using Microsoft.Extensions.Configuration;
using MinutoSeguro.Entity;
using MinutoSeguro.Manager.ActionManipulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace MinutoSeguro.Test.ActionManipulation
{
    public class RemoveUselessWordsTest
    {
        [Fact]
        public void RemoveUselessWords_Success()
        {

            var configuration = new Moq.Mock<IConfiguration>();

            Dictionary<string, IConfigurationSection> sections = new Dictionary<string, IConfigurationSection>();

            var exceptionSection = new Moq.Mock<IConfigurationSection>();
            exceptionSection.SetupGet(p => p.Value).Returns("para");
            sections.Add("exceptions", exceptionSection.Object);

            configuration
                .Setup(c => c.GetSection(Moq.It.IsAny<string>()))
                .Returns<string>((sectionName) => sections[sectionName]);

            var removeUselessWord = new RemoveUselessWords(configuration.Object);

            IEnumerable<Word> wordsToCheck = null;
            removeUselessWord.OnExecuteFinished += (restWord) => wordsToCheck = restWord;
            var defaultWord = new Word("asdasd");
            defaultWord.Type = WordType.Default;

            var prepositionWord = new Word("asdasd");
            prepositionWord.Type = WordType.Preposition;

            var articleWord = new Word("asdasd");
            articleWord.Type = WordType.Article;

            var verbWord = new Word("asdasd");
            verbWord.Type = WordType.Verb;

            List<Word> words = new List<Word>()
            {
                defaultWord,
                prepositionWord,
                articleWord,
                verbWord
            };
            removeUselessWord.Execute(words);

            Assert.NotNull(wordsToCheck);
            Assert.Single(wordsToCheck);

        }

        [Fact]
        public void RemoveUselessWords_Success_RemovingNumber()
        {

            var configuration = new Moq.Mock<IConfiguration>();

            Dictionary<string, IConfigurationSection> sections = new Dictionary<string, IConfigurationSection>();

            var exceptionSection = new Moq.Mock<IConfigurationSection>();
            exceptionSection.SetupGet(p => p.Value).Returns("r\\$,&.*,\\d");
            sections.Add("exceptions", exceptionSection.Object);

            configuration
                .Setup(c => c.GetSection(Moq.It.IsAny<string>()))
                .Returns<string>((sectionName) => sections[sectionName]);

            var removeUselessWord = new RemoveUselessWords(configuration.Object);

            IEnumerable<Word> wordsToCheck = null;
            removeUselessWord.OnExecuteFinished += (restWord) => wordsToCheck = restWord;
            var defaultWord = new Word("00");
            defaultWord.Type = WordType.Default;


            var defaultWord2 = new Word("asdads");
            defaultWord.Type = WordType.Default;
            List<Word> words = new List<Word>()
            {
                defaultWord,
                defaultWord2
            };
            removeUselessWord.Execute(words);

            Assert.NotNull(wordsToCheck);
            Assert.Single(wordsToCheck);

        }
    }
}
