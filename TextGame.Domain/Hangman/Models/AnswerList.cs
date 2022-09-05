namespace TextGame.Domain.Hangman.Models
{
    public class AnswerList
    {
        private readonly IEnumerable<string> _possibleAnswers;

        private readonly List<string> _usedAnswers = new();

        private readonly Random _rng  = new();

        public AnswerList(IEnumerable<string> possibleAnswers)
        {
            _possibleAnswers = possibleAnswers;
        }

        public string GetNewAnswer()
        {
            ClearUsedAnswersIfAllPossibleAnswersAreUsed();

            var nextAnswer = GetUnusedAnswer();

            _usedAnswers.Add(nextAnswer);

            return nextAnswer;
        }

        private string GetUnusedAnswer()
        {
            var unusedAnswers = _possibleAnswers.Where(p => !_usedAnswers.Contains(p));

            var indexOfNextAnswer = _rng.Next(0, unusedAnswers.Count() - 1);

            return unusedAnswers.ElementAt(indexOfNextAnswer);
        }

        private void ClearUsedAnswersIfAllPossibleAnswersAreUsed()
        {
            if (_usedAnswers.Count == _possibleAnswers.Count())
            {
                _usedAnswers.Clear();
            }
        }
    }
}
