using Microsoft.Extensions.Configuration;
using MinutoSeguro.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MinutoSeguro.Manager.ActionManipulation
{
    public class RemoveUselessWords : IActionManipulation<IEnumerable<Word>, IEnumerable<Word>>
    {
        private readonly string _exceptions;

        public RemoveUselessWords(IConfiguration config)
        {
            _exceptions = config.GetSection("exceptions").Value;
        }

        public override void Execute(IEnumerable<Word> words)
        {
            var stringWords = words.Where(word =>
                  word.Type == WordType.Default &&
                  !String.IsNullOrEmpty(word) &&
                  !Regex.IsMatch(
                      word,
                  $"^({string.Join("|", _exceptions.Split(",").ToList())})$",
                  RegexOptions.IgnoreCase));

            OnExecuteFinished(stringWords);
        }
    }
}
