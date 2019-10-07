using MinutoSeguro.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MinutoSeguro.Test.ActionManipulation
{
    public class SentenceTest
    {
        [Fact]
        public void Sentence_Parse_Success()
        {
            var rawText = @"Outubro é um mês extremamente representativo para o mundo. É nele que a campanha do Outubro Rosa, voltada à conscientização e prevenção ao câncer de mama, começa.";

            var setenteces = Sentence.Parse(rawText, new List<string>() { "que", "e" }, new List<string>() { ".", "," });
            Assert.Equal(6, setenteces.Count);
            Assert.Equal("Outubro é um mês extremamente representativo para o mundo", setenteces[0]);
            Assert.Equal("É nele", setenteces[1]);
            Assert.Equal("a campanha do Outubro Rosa", setenteces[2]);
            Assert.Equal("voltada à conscientização", setenteces[3]);
            Assert.Equal("prevenção ao câncer de mama", setenteces[4]);
            Assert.Equal("começa", setenteces[5]);
            Assert.Equal(9, setenteces[0].Count);

        }
    }
}
