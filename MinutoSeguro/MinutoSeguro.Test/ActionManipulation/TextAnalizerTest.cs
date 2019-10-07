using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.Linq;
using MinutoSeguro.Entity;
using Microsoft.Extensions.Configuration;
using MinutoSeguro.Manager.ActionManipulation;

namespace MinutoSeguro.Test.ActionManipulation
{
    public class TextAnalizerTest
    {
        [Fact]
        public void TextAnalizer_Analize_Success()
        {

            var configuration = new Moq.Mock<IConfiguration>();

            Dictionary<string, IConfigurationSection> sections = new Dictionary<string, IConfigurationSection>();

            var prepositionSection = new Moq.Mock<IConfigurationSection>();
            prepositionSection.SetupGet(p => p.Value).Returns("para");
            sections.Add("prepositions", prepositionSection.Object);

            var articlesSection = new Moq.Mock<IConfigurationSection>();
            articlesSection.SetupGet(p => p.Value).Returns("um,o");
            sections.Add("articles", articlesSection.Object);

            var setencesBreakersSection = new Moq.Mock<IConfigurationSection>();
            setencesBreakersSection.SetupGet(p => p.Value).Returns("que");
            sections.Add("setences_breakers", setencesBreakersSection.Object);

            var pontuationSection = new Moq.Mock<IConfigurationSection>();
            pontuationSection.SetupGet(p => p.Value).Returns(".");
            sections.Add("pontuation", pontuationSection.Object);

            var verbSection = new Moq.Mock<IConfigurationSection>();
            verbSection.SetupGet(p => p.Value).Returns("é");
            sections.Add("verbs", verbSection.Object);

            configuration
                .Setup(c => c.GetSection(Moq.It.IsAny<string>()))
                .Returns<string>((sectionName) => sections[sectionName]);

            IEnumerable<Word> allWords = null;

            var textAnalizer = new TextAnalizer(configuration.Object);

            textAnalizer.OnExecuteFinished += (words) => allWords = words;
            textAnalizer.Execute("Outubro é um mês extremamente representativo para o mundo");

            Assert.NotNull(allWords);
            Assert.Equal(9, allWords.Count());
            Assert.Equal(1, allWords.Count(w => w.Type == WordType.Verb));
            Assert.Equal(1, allWords.Count(w => w.Type == WordType.Preposition));
            Assert.Equal(2, allWords.Count(w => w.Type == WordType.Article));
            Assert.Equal(5, allWords.Count(w => w.Type == WordType.Default));

        }

        [Fact]
        public void TextAnalizer_Analize_MoreThanOneSentence_Success()
        {
            var configuration = new Moq.Mock<IConfiguration>();

            Dictionary<string, IConfigurationSection> sections = new Dictionary<string, IConfigurationSection>();

            var prepositionSection = new Moq.Mock<IConfigurationSection>();
            prepositionSection.SetupGet(p => p.Value).Returns("para,ao,à");
            sections.Add("prepositions", prepositionSection.Object);

            var articlesSection = new Moq.Mock<IConfigurationSection>();
            articlesSection.SetupGet(p => p.Value).Returns("um,o,a,do,de");
            sections.Add("articles", articlesSection.Object);

            var setencesBreakersSection = new Moq.Mock<IConfigurationSection>();
            setencesBreakersSection.SetupGet(p => p.Value).Returns("que,e");
            sections.Add("setences_breakers", setencesBreakersSection.Object);

            var pontuationSection = new Moq.Mock<IConfigurationSection>();
            pontuationSection.SetupGet(p => p.Value).Returns(".|,");
            sections.Add("pontuation", pontuationSection.Object);

            var verbSection = new Moq.Mock<IConfigurationSection>();
            verbSection.SetupGet(p => p.Value).Returns("é");
            sections.Add("verbs", verbSection.Object);

            configuration
                .Setup(c => c.GetSection(Moq.It.IsAny<string>()))
                .Returns<string>((sectionName) => sections[sectionName]);

            var textAnalizer = new TextAnalizer(configuration.Object);
            IEnumerable<Word> allWords = null;
            textAnalizer.OnExecuteFinished += (words) => allWords = words;
            textAnalizer.Execute("É nele que a campanha do Outubro Rosa, voltada à conscientização e prevenção ao câncer de mama, começa.");
            Assert.NotNull(allWords);
            Assert.Equal(16, allWords.Count());
            Assert.Equal(1, allWords.Count(w => w.Type == WordType.Verb));
            Assert.Equal(2, allWords.Count(w => w.Type == WordType.Preposition));
            Assert.Equal(3, allWords.Count(w => w.Type == WordType.Article));
            Assert.Equal(10, allWords.Count(w => w.Type == WordType.Default));

        }


        //
    }
}
