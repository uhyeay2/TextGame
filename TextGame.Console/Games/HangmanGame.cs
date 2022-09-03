using TextGame.Console.Screen;
using TextGame.Domain;
using TextGame.Domain.Hangman.Enums;
using TextGame.Domain.Hangman.Models;

namespace TextGame.Console.Games
{
    internal class HangmanGame
    {
        ScreenPrinter _printer;

        Hangman _hangman = new();

        Answer? _answer;

        bool _keepPlaying;

        public HangmanGame(ScreenPrinter printer)
        {
            _printer = printer;
        }

        internal void Start()
        {
            _keepPlaying = true;

            var input = _printer.PrintMenuGetIndexSelected(Constants.Messages.HangmanIntroduction, Constants.MenuOptions.HangmanGenreOptions);
            
            var answerList = Genres.GetAnswerList((Genre)input);
        
            while (_keepPlaying)
            {
                _hangman = new Hangman();

                _answer = new Answer(answerList.GetNewAnswer());

                while (_hangman.IsAlive && !_answer.IsFullyVisible)
                {
                    var guess = AskForGuess();

                    ProcessGuess(guess);

                    PrintMessageIfGameOver();
                }

                input = _printer.PrintMenuGetIndexSelected(Constants.Messages.HangmanContinuePlaying, Constants.MenuOptions.HangmanContinuePlayingOptions);

                switch (input)
                {
                    case var i when Constants.MenuOptions.HangmanContinuePlayingOptions[i] == Constants.Commands.Continue:
                        break;

                    case var i when Constants.MenuOptions.HangmanContinuePlayingOptions[i] == Constants.Commands.Quit:
                        _keepPlaying = false;
                        break;

                    // If did not choose Continue or Quit, then display Genre Options (Only other choice for this menu)
                    default:
                        input = _printer.PrintMenuGetIndexSelected(Constants.Messages.HangmanChangeGenres, Constants.MenuOptions.HangmanGenreOptions);
                        answerList = Genres.GetAnswerList((Genre)input);
                        break;
                }
            }
        }

        AddGuessResponse AskForGuess()
        {
            var guessesLeft = new[]
                {
                    $"You currently have {_hangman.GuessesLeft} wrong {(_hangman.GuessesLeft == 1 ? "guess" : "guesses")} left.", ""
                };

            var promptForInput = new[]
            {
                    "", "Please enter the next letter you would like to guess.", ""
            };

            var answerSoFar = new[]
            {
                "", _answer!.VisibleAnswer, ""
            };

            _printer.PrintScreen(clearBeforePrint: true, guessesLeft, _answer!.GetGuessesMade(), _hangman.GetDisplay(), answerSoFar, promptForInput);

            return _answer.TryAddGuess(_printer.ReadKey());
        }

        void ProcessGuess(AddGuessResponse guess)
        {
            _hangman.UpdateGuessesMade(guess);

            switch (guess)
            {
                case AddGuessResponse.CorrectGuessAdded:
                    _printer.PrintScreen(Constants.Messages.HangmanCorrectGuess);
                    break;
                case AddGuessResponse.WrongGuessAdded:
                    _printer.PrintScreen(Constants.Messages.HangmanWrongGuess);
                    break;
                case AddGuessResponse.GuessAlreadyMade:
                    _printer.PrintScreen(Constants.Messages.HangmanGuessAlreadyMade);
                    break;
                case AddGuessResponse.InvalidCharacter:
                    _printer.PrintScreen(Constants.Messages.HangmanInvalidCharacterGuessed);
                    break;
            }

            _printer.ReadKey();
        }

        void PrintMessageIfGameOver()
        {
            if (_answer!.IsFullyVisible)
            {
                _printer.PrintScreen(clearBeforePrint: true, Constants.Messages.HangmanGameWon, _hangman.GetDisplay());
            }

            if (!_hangman.IsAlive)
            {
                _printer.PrintScreen(clearBeforePrint: true, Constants.Messages.HangmanGameLost, _hangman.GetDisplay());
            }
        }
    }
}
