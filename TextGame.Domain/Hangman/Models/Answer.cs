using TextGame.Domain.Hangman.Enums;

namespace TextGame.Domain.Hangman.Models
{
    public class Answer
    {
        private readonly string _value;

        private readonly List<char> GuessesMade = new();

        public Answer(string value)
        {
            _value = value;
        }

        public bool HasAlreadyBeenGuessed(char c) => GuessesMade.Any(g => g == c);

        public string[] GetGuessesMade() => GuessesMade.Any() ? 
            new string[] { "Guesses Made:", GuessesMade.OrderBy(c => c).Select(x => x.ToString().ToUpper()).Aggregate((a,b) => $"{a}, {b}"), "" } :
            new string[] { "Guesses Made:", "None.", "" };          

        public bool IsFullyVisible => _value.Where(v => char.IsLetter(v)).All(c => GuessesMade.Contains(char.ToLower(c)));

        public string VisibleAnswer => new(_value.Select(c => DisplayIfGuessed(c)).ToArray());

        public AddGuessResponse TryAddGuess(char guess) => guess switch
        {
            var c when HasAlreadyBeenGuessed(char.ToLower(c)) => AddGuessResponse.GuessAlreadyMade,
            var c when !char.IsLetter(c) => AddGuessResponse.InvalidCharacter,
            _ => AddGuess(guess)
        };

        #region Private Helper Methods

        private AddGuessResponse AddGuess(char c)
        {
            GuessesMade.Add(char.ToLower(c));
            return _value.ToLower().Contains(char.ToLower(c)) ? AddGuessResponse.CorrectGuessAdded : AddGuessResponse.WrongGuessAdded;
        }

        private char DisplayIfGuessed(char c) => GuessesMade.Contains(char.ToLower(c)) || char.IsWhiteSpace(c) ? c : '_';

        #endregion
    }
}
