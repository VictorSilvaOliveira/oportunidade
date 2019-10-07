using MinutoSeguro.Entity;
using System.Collections.Generic;

namespace MinutoSeguro.Manager.ActionManipulation
{
    public class FeedCountImportantWords : IActionManipulation<Feed, Dictionary<string, int>>
    {
        private IActionManipulation<string, IEnumerable<Word>> _startManipulationAction;
        private IActionManipulation<IEnumerable<Word>, Dictionary<string, int>> _endManipulationAction;

        public FeedCountImportantWords(IActionManipulation<string, IEnumerable<Word>> startManipulationAction, IActionManipulation<IEnumerable<Word>, Dictionary<string, int>> endManipulationAction)
        {
            _startManipulationAction = startManipulationAction;
            _endManipulationAction = endManipulationAction;
            _endManipulationAction.OnExecuteFinished += (result) => this.OnExecuteFinished(result);
        }

        public override void Execute(Feed source)
        {
            _startManipulationAction.Execute(source.Content);
        }
    }
}